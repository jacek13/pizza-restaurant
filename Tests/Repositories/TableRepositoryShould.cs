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
    public class TableRepositoryShould
    {
        private readonly RestaurantDBContext _context;
        private IDbContextFactory<RestaurantDBContext> _factory;
        private DbContextOptions<RestaurantDBContext> _options;
        private string _dataBaseName = "In memory database - Table";

        public class Factory : IDbContextFactory<RestaurantDBContext>
        {
            public RestaurantDBContext _dbContext;
            private string _dataBaseName = "In memory database - Table";
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

        public TableRepositoryShould()
        {
            _options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseInMemoryDatabase(_dataBaseName)
                .Options;
            _context = new RestaurantDBContext(_options);
            _factory = new Factory(_context, _dataBaseName);
        }

        [Fact(DisplayName = "Manager is able to add new Table to the restaurant")]
        public async Task AddTable()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var rr = new RestaurantRepository(_factory);
                var restaurant = new Restaurant()
                {
                    Address = "Kochanowskiego 13",
                    PhoneNumber = "123456789",
                    EMail = "GalakPizzaKochanowski@gmail.com"
                };

                context.Restaurant.Add(restaurant);
                context.SaveChanges();

                var tr = new TableRepository(_factory);
                var table = new Table()
                {
                    Capacity = 5,
                    RestaurantIdRestaurant = restaurant.IdRestaurant
                };

                // Act
                var result = await tr.Insert(table);

                // Assert
                var tables = await context.Table.ToListAsync();
                Assert.Equal(tables.FirstOrDefault(p => p.Capacity == 5)!.RestaurantIdRestaurant, result.RestaurantIdRestaurant);

                // Clean
                await tr.Delete(table.IdTable);
                await rr.Delete(restaurant.IdRestaurant);
            }
        }

        [Fact(DisplayName = "Manager is able to delete Table from the restaurant")]
        public async Task DeleteTable()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var rr = new RestaurantRepository(_factory);
                var restaurant = new Restaurant()
                {
                    Address = "Kochanowskiego 66",
                    PhoneNumber = "123456789",
                    EMail = "GalakPizzaKochanowski@gmail.com"
                };

                context.Restaurant.Add(restaurant);
                context.SaveChanges();

                var tr = new TableRepository(_factory);
                var table = new Table()
                {
                    Capacity = 13,
                    RestaurantIdRestaurant = restaurant.IdRestaurant
                };

                context.Table.Add(table);
                context.SaveChanges();

                // Act
                var tablesBeforeDelete = await context.Table.ToListAsync();
                await tr.Delete(table.IdTable);
                var tablesAfterDelete = await context.Table.ToListAsync();

                // Assert
                Assert.NotEqual(tablesBeforeDelete.Count, tablesAfterDelete.Count);
            }
        }

        [Fact(DisplayName = "Manager is able to Update table capacity")]
        public async Task UpdateTableCapacity()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var rr = new RestaurantRepository(_factory);
                var restaurant = new Restaurant()
                {
                    Address = "Kochanowskiego 66",
                    PhoneNumber = "123456789",
                    EMail = "GalakPizzaKochanowski@gmail.com"
                };

                context.Restaurant.Add(restaurant);
                context.SaveChanges();

                var tr = new TableRepository(_factory);
                var table = new Table()
                {
                    Capacity = 128,
                    RestaurantIdRestaurant = restaurant.IdRestaurant
                };

                // Act
                var result = await tr.updateCapacity(table, 4);
                var tables = await context.Table.ToListAsync();

                // Assert
                Assert.Equal(tables.FirstOrDefault(p => p.Capacity == 4)!.RestaurantIdRestaurant, table.RestaurantIdRestaurant);

                // Clean
                await tr.Delete(table.IdTable);
                await rr.Delete(restaurant.IdRestaurant);
            }
        }
    }
}
