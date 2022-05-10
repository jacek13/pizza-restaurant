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
    public class PizzaRepositoryShould
    {
        private readonly RestaurantDBContext _context;
        private IDbContextFactory<RestaurantDBContext> _factory;
        private DbContextOptions<RestaurantDBContext> _options;
        private string _dataBaseName = "In memory database - Pizza";

        public class Factory : IDbContextFactory<RestaurantDBContext>
        {
            public RestaurantDBContext _dbContext;
            private string _dataBaseName = "In memory database - Pizza";
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

        public PizzaRepositoryShould()
        {
            _options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseInMemoryDatabase(_dataBaseName)
                .Options;
            _context = new RestaurantDBContext(_options);
            _factory = new Factory(_context, _dataBaseName);
        }

        [Fact(DisplayName = "Manager is able to add new pizza to the database")]
        public async Task AddPizza()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var pr = new PizzaRepository(_factory);
                var pizza = new Pizza()
                {
                    Type = "Pepperoni",
                    Size = 32,
                    Price = 17,
                    Cost = 7,
                    Points = 1,
                    IsAvailable = true
                };
                // Act
                var result = await pr.Insert(pizza);

                // Assert
                var menu = await context.Pizza.ToListAsync();
                Assert.Equal(menu.FirstOrDefault(p => p.Type == "Pepperoni")!.Type, result.Type);

                // Clean
                await pr.Delete(pizza.IdPizza);
            }
        }

        [Fact(DisplayName = "Manager is able to delete pizza from the database")]
        public async Task DeletePizza()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var pr = new PizzaRepository(_factory);
                var pizza = new Pizza()
                {
                    Type = "Roma",
                    Size = 32,
                    Price = 15,
                    Cost = 6,
                    Points = 0,
                    IsAvailable = true
                };
                await pr.Insert(pizza);

                // Act
                var menuBeforeDelete = await context.Pizza.ToListAsync();
                await pr.Delete(pizza.IdPizza);
                var menuAfterDelete = await context.Pizza.ToListAsync();

                // Assert
                Assert.NotEqual(menuBeforeDelete.Count, menuAfterDelete.Count);
            }
        }

        [Fact(DisplayName = "Manager is able to Update pizza in the database")]
        public async Task UpdatePizza()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var pr = new PizzaRepository(_factory);
                var pizza = new Pizza()
                {
                    Type = "Funghi",
                    Size = 32,
                    Price = 14,
                    Cost = 5,
                    Points = 0,
                    IsAvailable = false
                };
                await pr.Insert(pizza);

                // Act
                pizza.IsAvailable = true;
                var result = await pr.Update(pizza);
                var menu = await context.Pizza.ToListAsync();

                // Assert
                Assert.Equal(menu.FirstOrDefault(p => p.Type == "Funghi")!.IsAvailable, pizza.IsAvailable);

                // Clean
                await pr.Delete(pizza.IdPizza);
            }
        }
    }
}
