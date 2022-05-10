using DataBaseAccess.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories
{
    public class OrderRepositoryShould
    {
        private readonly RestaurantDBContext _context;
        private IDbContextFactory<RestaurantDBContext> _factory;
        private DbContextOptions<RestaurantDBContext> _options;
        private string _dataBaseName = "In memory database - Order";

        public class Factory : IDbContextFactory<RestaurantDBContext>
        {
            public RestaurantDBContext _dbContext;
            private string _dataBaseName = "In memory database - Order";
            public Factory(RestaurantDBContext dBContext, string dataBaseName)
            {
                _dbContext = dBContext;
                _dataBaseName = dataBaseName;
            }
            public RestaurantDBContext CreateDbContext()
            {
                var _options = new DbContextOptionsBuilder<RestaurantDBContext>()
                                    .UseInMemoryDatabase(_dataBaseName)
                                    .Options;
                _dbContext = new RestaurantDBContext(_options);
                return _dbContext;
            }
        }

        public OrderRepositoryShould()
        {
            _options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseInMemoryDatabase(_dataBaseName)
                .Options;
            _context = new RestaurantDBContext(_options);
            _factory = new Factory(_context, _dataBaseName);
        }

        [Fact(DisplayName = "System is able to add new order to the database")]
        public async Task AddOrder()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var or = new OrderRepository(_factory);
                var order = new Order()
                {
                    Date = DateTime.Now,
                    DeliveryAdress = "Fryderyka Chopina 13",
                    Name = "Admirał",
                    Surname = "Jachaś",
                    AdditionalInformation = "Trójkącik jak łono",
                    Status = "P",
                    PhoneNumber = "123456789"
                };
                // Act
                var result = await or.Insert(order);

                // Assert
                var menu = await context.Order.ToListAsync();
                Assert.Equal(menu.FirstOrDefault(o => o.Surname == "Jachaś")!.Name, result.Name);

                // Clean
                await or.Delete(order.IdOrder);
            }
        }

        [Fact(DisplayName = "System is able to delete order from the database")]
        public async Task DeleteOrder()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var or = new OrderRepository(_factory);
                var order = new Order()
                {
                    Date = DateTime.Now,
                    DeliveryAdress = "Akademicka 73",
                    Name = "Tytus",
                    Surname = "Bomba",
                    AdditionalInformation = "Wolę hot dogi",
                    Status = "P",
                    PhoneNumber = "123456789"
                };
                await or.Insert(order);

                // Act
                var ordersBeforeDelete = await context.Order.ToListAsync();
                await or.Delete(order.IdOrder);
                var ordersAfterDelete = await context.Order.ToListAsync();

                // Assert
                Assert.NotEqual(ordersBeforeDelete.Count, ordersAfterDelete.Count);
            }
        }

        [Fact(DisplayName = "System is able to Update order status")]
        public async Task UpdateOrderStatus()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var or = new OrderRepository(_factory);
                var order = new Order()
                {
                    Date = DateTime.Now,
                    DeliveryAdress = "Akademicka 73",
                    Name = "Sułtan",
                    Surname = "Kosmitów",
                    AdditionalInformation = "Sancti Magistrii",
                    Status = "U",
                    PhoneNumber = "987654321"
                };
                await or.Insert(order);

                // Act
                var result_preparing = await or.UpdateOrderStatus(order.IdOrder, 'P');
                var result_delivery = await or.UpdateOrderStatus(order.IdOrder, 'D');
                var result_finished = await or.UpdateOrderStatus(order.IdOrder, 'F');
                var result_canceled = await or.UpdateOrderStatus(order.IdOrder, 'C');
                var orders = await context.Order.ToListAsync();

                // Assert
                Assert.Equal("P", result_preparing.Status);
                Assert.Equal("D", result_delivery.Status);
                Assert.Equal("F", result_finished.Status);
                Assert.Equal(orders.FirstOrDefault(o => o.Name == "Sułtan")!.Status, result_canceled.Status);

                // Clean
                await or.Delete(order.IdOrder);
            }
        }

        [Fact(DisplayName = "System is able return client orders")]
        public async Task GetClientOrders()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var account = new Account()
                {
                    Name = "Piotr",
                    Surname = "Frączewski",
                    Password = "RodzinaZastępcza",
                    PhoneNumber = "192837465",
                    Login = "Karate mistrz",
                    EMail = "PolecamIPozdrawiam@o2.pl",
                    AccountCreationDate = DateTime.Now,
                    Role = "Client"
                };

                var client = new Client()
                {
                    AccountIdAccount = account.IdAccount,
                    Points = 0,
                };

                context.Account.Add(account);
                context.SaveChanges();
                context.Client.Add(client);
                context.SaveChanges();

                var or = new OrderRepository(_factory);
                var orderFirst = new Order()
                {
                    Date = DateTime.Now,
                    DeliveryAdress = "Akademia Pana Kleksa",
                    Name = "Adaś",
                    Surname = "Niezgódka",
                    AdditionalInformation = "Pan kleks jest dziwny",
                    Status = "U",
                    PhoneNumber = "987654321",
                    ClientIdClient = client.IdClient
                };

                var orderSecond = new Order()
                {
                    Date = DateTime.Now,
                    DeliveryAdress = "Akademia Pana Kleksa",
                    Name = "Adaś",
                    Surname = "Niezgódka",
                    AdditionalInformation = "Pan kleks jest dziwny",
                    Status = "U",
                    PhoneNumber = "987654321",
                    ClientIdClient = client.IdClient
                };
                await or.Insert(orderFirst);
                await or.Insert(orderSecond);

                // Act
                var PiotrOrders = await or.GetUserOrders(client.IdClient);

                // Assert
                Assert.Equal(2, PiotrOrders.Count);
                Assert.Equal(PiotrOrders[0].ClientIdClient, PiotrOrders[1].ClientIdClient);

                // Clean
                context.Account.Remove(account);
                context.SaveChanges();
                context.Client.Remove(client);
                context.SaveChanges();
                await or.Delete(orderFirst.IdOrder);
                await or.Delete(orderSecond.IdOrder);
            }
        }

        [Fact(DisplayName = "System is able to return restaurant orders")]
        public async Task GetRestaurantOrders()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var restaurant = new Restaurant()
                { 
                    Address = "Łużycka 7",
                    PhoneNumber = "192837465",
                    EMail = "GalakŁużycka@gmail.com"
                };

                context.Restaurant.Add(restaurant);
                context.SaveChanges();

                var or = new OrderRepository(_factory);
                var orderFirst = new Order()
                {
                    Date = DateTime.Now,
                    DeliveryAdress = "Oleska 13",
                    Name = "Stanisław",
                    Surname = "Jankowski",
                    AdditionalInformation = "Proszę dostarczyć szybko",
                    Status = "P",
                    PhoneNumber = "794123456",
                    RestaurantIdRestaurant = restaurant.IdRestaurant,
                };

                var orderSecond = new Order()
                {
                    Date = DateTime.Now,
                    DeliveryAdress = "Oleska 13",
                    Name = "Stanisław",
                    Surname = "Jankowski",
                    AdditionalInformation = "Bo mamy ultra gastro",
                    Status = "P",
                    PhoneNumber = "794123456",
                    RestaurantIdRestaurant = restaurant.IdRestaurant,
                };
                await or.Insert(orderFirst);
                await or.Insert(orderSecond);

                // Act
                var RestaurantOrders = await or.GetRestaurantOrders(restaurant.IdRestaurant);

                // Assert
                Assert.Equal(2, RestaurantOrders.Count);
                Assert.Equal(RestaurantOrders[0].RestaurantIdRestaurant, RestaurantOrders[1].RestaurantIdRestaurant);

                // Clean
                context.Restaurant.Remove(restaurant);
                context.SaveChanges();
                await or.Delete(orderFirst.IdOrder);
                await or.Delete(orderSecond.IdOrder);
            }
        }
    }
}
