using DataBaseAccess.Models;
using Repository.Repositories;
using Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories
{
    public class AccountManagmentServiceShould
    {
        private readonly RestaurantDBContext _context;
        private IDbContextFactory<RestaurantDBContext> _factory;
        private DbContextOptions<RestaurantDBContext> _options;
        private string _dataBaseName = "In memory database - Account Managment";

        public class Factory : IDbContextFactory<RestaurantDBContext>
        {
            public RestaurantDBContext _dbContext;
            private string _dataBaseName = "In memory database - Account Managment";
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

        public AccountManagmentServiceShould()
        {
            _options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseInMemoryDatabase(_dataBaseName)
                .Options;
            _context = new RestaurantDBContext(_options);
            _factory = new Factory(_context, _dataBaseName);
        }

        [Fact(DisplayName = "Service is able to add a user to the database")]
        public async Task AddUser()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var ar = new AccountRepository(_factory);
                var or = new ClientRepository(_factory);
                var ams = new AccountManagmentService(ar, or);

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

                var client = new Client()
                {
                    AccountIdAccount = account.IdAccount,
                    Address = "Unknown street",
                    Points = 0,
                };

                // Act
                var result = await ams.AddNewClient(account.EMail,
                                                    account.Login,
                                                    account.Password,
                                                    account.Name,
                                                    account.Surname,
                                                    account.AccountCreationDate.GetValueOrDefault(),
                                                    account.PhoneNumber,
                                                    client.Points,
                                                    client.Address,
                                                    account.Role
                                                    );

                // Assert
                var accounts = await context.Account.ToListAsync();
                var clients = await context.Client.ToListAsync();

                Assert.Equal(accounts.FirstOrDefault(a => a.Name == "Frodo")!.Name, result.Item1.Name);
                Assert.Equal(clients.FirstOrDefault(c => c.Address == "Unknown street")!.Points, result.Item2.Points);

                // Clean
                ams.DeleteClient(client.IdClient);
            }
        }

        [Fact(DisplayName = "Service is able to delete a user from the database")]
        public async Task DeleteUser()
        {
            using (var context = _factory.CreateDbContext())
            {
                // Arrange
                var ar = new AccountRepository(_factory);
                var or = new ClientRepository(_factory);
                var ams = new AccountManagmentService(ar, or);

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

                var client = new Client()
                {
                    AccountIdAccount = account.IdAccount,
                    Address = "Unknown street Prim",
                    Points = 13,
                };

                context.Account.Add(account);
                context.SaveChanges();
                context.Client.Add(client);
                context.SaveChanges();

                // Act
                var usersBeforeDelete = await context.Account.ToListAsync();
                ams.DeleteClient(client.IdClient);
                Thread.Sleep(500); // Wait fo Database
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
                var or = new ClientRepository(_factory);
                var ams = new AccountManagmentService(ar, or);

                var account = new Account()
                {
                    Name = "Gandalf",
                    Surname = "White",
                    Password = "magic1234",
                    PhoneNumber = "192837465",
                    Login = "Gandalf",
                    EMail = "GandalfWhite@gmail.com",
                    AccountCreationDate = DateTime.Now,
                    Role = "Client"
                };

                var client = new Client()
                {
                    AccountIdAccount = account.IdAccount,
                    Address = "Unknown street Prim",
                    Points = 13,
                };

                context.Account.Add(account);
                context.SaveChanges();
                context.Client.Add(client);
                context.SaveChanges();

                // Act
                account.Role = "Manager";
                client.Points = 26;

                var result = await ams.UpdateAccountAndClient(  account.IdAccount,
                                                                account.EMail,
                                                                account.Login,
                                                                account.Password,
                                                                account.Name,
                                                                account.Surname,
                                                                account.AccountCreationDate.GetValueOrDefault(),
                                                                account.PhoneNumber,
                                                                client.Points,
                                                                client.Address,
                                                                account.Role,
                                                                client.IdClient
                                                                );


                var users = await context.Account.ToListAsync();
                var accounts = await context.Account.ToListAsync();
                var clients = await context.Client.ToListAsync();

                // Assert
                Assert.Equal(accounts.FirstOrDefault(a => a.Name == "Gandalf")!.Name, result.Item1.Name);
                Assert.Equal(clients.FirstOrDefault(c => c.Address == "Unknown street Prim")!.Points, result.Item2.Points);

                // Clean
                ams.DeleteClient(client.IdClient);
            }
        }
    }
}
