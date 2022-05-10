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
    public class AccountRepositoryShould
    {
        private readonly RestaurantDBContext _context;
        private IDbContextFactory<RestaurantDBContext> _factory;
        private DbContextOptions<RestaurantDBContext> _options;
        private string _dataBaseName = "In memory database - Account";

        public class Factory : IDbContextFactory<RestaurantDBContext>
        {
            public RestaurantDBContext _dbContext;
            private string _dataBaseName = "In memory database - Account";
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

        public AccountRepositoryShould()
        {
            _options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseInMemoryDatabase(_dataBaseName)
                .Options;
            _context = new RestaurantDBContext(_options);
            _factory = new Factory(_context, _dataBaseName);
        }

        [Fact(DisplayName = "System is able to add a user to the database")]
        public async Task AddUser()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var ar = new AccountRepository(_factory);
                var account = new Account()
                {
                    Name = "Frodo",
                    Surname = "Baggins",
                    Password = "Ilov3Shir3",
                    PhoneNumber = "192837465",
                    Login = "FroBag",
                    EMail = "FrodoBaggins13@gmail.com",
                    AccountCreationDate = DateTime.Now,
                    Role = "Client"
                };
                // Act
                var result = await ar.Insert(account);

                // Assert
                var users = await context.Account.ToListAsync();
                Assert.Equal(users.FirstOrDefault(u => u.Name == "Frodo")!.Name, result.Name);

                // Clean
                await ar.Delete(account.IdAccount);
            }
        }

        [Fact(DisplayName = "System is able to delete a user to the database")]
        public async Task DeleteUser()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var ar = new AccountRepository(_factory);
                var account = new Account()
                {
                    Name = "Bilbo",
                    Surname = "Baggins",
                    Password = "Ilov3Shir3",
                    PhoneNumber = "192837465",
                    Login = "BilBag",
                    EMail = "BilboBaggins31@gmail.com",
                    AccountCreationDate = DateTime.Now,
                    Role = "Client"
                };
                await ar.Insert(account);

                // Act
                var usersBeforeDelete = await context.Account.ToListAsync();
                await ar.Delete(account.IdAccount);
                var usersAfterDelete = await context.Account.ToListAsync();

                // Assert
                Assert.NotEqual(usersBeforeDelete.Count, usersAfterDelete.Count);
            }
        }

        [Fact(DisplayName = "System is able to Update user data in the database")]
        public async Task UpdateUser()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var ar = new AccountRepository(_factory);
                var account = new Account()
                {
                    Name = "Saruman",
                    Surname = "White",
                    Password = "Ilov3UrukHai",
                    PhoneNumber = "987654321",
                    Login = "SarumanWhite",
                    EMail = "SarWhi@gmail.com",
                    AccountCreationDate = DateTime.Now,
                    Role = "Client"
                };
                await ar.Insert(account);

                // Act
                account.Role = "Manager";
                var result = await ar.Update(account);
                var users = await context.Account.ToListAsync();

                // Assert
                Assert.Equal(users.FirstOrDefault(u => u.Name == "Saruman")!.Role, result.Role);

                // Clean
                await ar.Delete(account.IdAccount);
            }
        }
    }
}
