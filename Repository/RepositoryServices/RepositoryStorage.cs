using DataBaseAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    public class RepositoryStorage
    {
        private readonly pizza_restaurant_ver_6Context _RestaurantContext = null;
        private ClientService _ClientService = null;
        private AccountService _AccountService = null;
        private DishesService _DishesService = null;
        private ManagerAssignmentService _ManagerAssignmentService = null;
        private ManagerService _ManagerService = null;
        private OrderService _OrderService = null;
        private PizzaService _PizzaService = null;
        private ReservationService _ReservationService = null;
        private RestaurantService _RestaurantService = null;
        private TableService _TableService = null;

        public RepositoryStorage(pizza_restaurant_ver_6Context Restaurant_Ver3Context)
        {
            this._RestaurantContext = Restaurant_Ver3Context;
            _ClientService = new ClientService(_RestaurantContext);
            _AccountService = new AccountService(_RestaurantContext);
            _DishesService = new DishesService(_RestaurantContext);
            _ManagerAssignmentService = new ManagerAssignmentService(_RestaurantContext);
            _ManagerService = new ManagerService(_RestaurantContext);
            _OrderService = new OrderService(_RestaurantContext);
            _PizzaService= new PizzaService(_RestaurantContext);
            _ReservationService = new ReservationService(_RestaurantContext);
            _RestaurantService= new RestaurantService(_RestaurantContext);
            _TableService = new TableService(_RestaurantContext);
        }
        //---------- RESTAURANT ----------//
        public Task<List<Restaurant>> GetRestaurants()
        {
            return _RestaurantService.GetAll();
        }


        //---------- ACCOUNT i CLIENT ----------//

        public Task<List<Account>> GetAccounts()
        {
            return _AccountService.GetAll();
        }

        public Task<List<Client>> GetClients()
        {
            return _ClientService.GetAll();
        }

        public async Task<Account> GetAccountByLoginAndPassword(string login, string password)
        {
            var accounts = await _AccountService.GetAll();
            var accountTmp = new Account();
            bool found = false;
        
            foreach (var account in accounts)
                if ((account.Login == login || account.EMail == login) && account.Password == password)
                {
                    accountTmp = account;
                    found = true;
                    break;
                }
            if (found)
                return accountTmp;
            else throw new Exception("Incorrect login or password");
        }

        public async void UpdateClientPoints(int id, int points)
        {
            var tmp = await _ClientService.GetAll();
            var primaryKeys = new List<int>();

            foreach (var item in tmp)
                primaryKeys.Add(item.IdClient);

            if (primaryKeys.Contains(id))
            {
                await _ClientService.UpdatePoints(id, points);
                await _ClientService.Save();
            }
        }

        public async void AddNewClient(String e_mail, String login, String password, String name, String surname, DateTime accountCreationDate, String PhoneNumber,
                                        int points, String address, String role)
        {
            var accountCount = _AccountService.lastId;
            var clientCount = _ClientService.lastId;

            // TODO trzeba sprawdzić czy poprawne dane, Rozwiążemy to sprawdzając dane już w formularzu. Tutaj będą dodatkowe zabezpieczenia na wypadek ominięcia tamtych
            var newAccount = new Account() 
            { 
                //IdAccount = accountCount, // Tutej był błąd związany z tym, że w tabeli było ustawione automatyczne wstawianie klucza
                EMail = e_mail, 
                Login = login, 
                Password = password,
                Name = name, 
                Surname = surname, 
                AccountCreationDate = accountCreationDate, 
                PhoneNumber = PhoneNumber,
                Role = role
            };

            var newClient = new Client
            {
                //IdClient = clientCount,
                Points = points,
                Address = address,
                AccountIdAccount = accountCount
            };

            // Account creation <- tabela
            var entity = await this._AccountService.Insert(newAccount);
            await this._AccountService.Save();

            // Client creation <- tabela
            newClient.AccountIdAccount = entity.IdAccount;
            await this._ClientService.Insert(newClient);
            await this._ClientService.Save();
        }

        public async void DeleteClient(int id)
        {
            var tmp = await _ClientService.GetAll();
            var primaryKeys = new List<int>();

            foreach (var item in tmp)
                primaryKeys.Add(item.IdClient);

            if (primaryKeys.Contains(id))
            {
                await this._AccountService.Delete(id);
                await this._AccountService.Save();
                await this._ClientService.Delete(id);
                await this._ClientService.Save();
            }
        }

        //---------- Pizza ----------// change availability
        public Task<List<Pizza>> GetMenu()
        {
            return _PizzaService.GetAll();
        }

        public async Task<Pizza> AddNewPizza(int price, int cost, bool isAvailable, string name, int size, int points)
        {

            // TODO trzeba sprawdzić czy poprawne dane, Rozwiążemy to sprawdzając dane już w formularzu. Tutaj będą dodatkowe zabezpieczenia na wypadek ominięcia tamtych
            var newPizza = new Pizza()
            {
                Price = price,
                Cost = cost,
                IsAvailable = isAvailable,
                Type = name,
                Size = size,
                Points = points
            };

            // Pizza creation <- tabela
            var entity = await this._PizzaService.Insert(newPizza);
            await this._AccountService.Save();
            return newPizza;
        }

        public async void ChangePizzaAvailability(int id, bool availability)
        {
            var tmp = await _PizzaService.GetAll();
            var primaryKeys = new List<int>();

            foreach (var item in tmp)
                primaryKeys.Add(item.IdPizza);

            if (primaryKeys.Contains(id))
            {
                await _PizzaService.UpdatePizzaAvailability(id, availability);
                await _PizzaService.Save();
            }

        }

        public async void DeletePizza(int id)
        {
            var tmp = await _PizzaService.GetAll();
            var primaryKeys = new List<int>();

            foreach (var item in tmp)
                primaryKeys.Add(item.IdPizza);

            if (primaryKeys.Contains(id))
            {
                await _PizzaService.Delete(id);
                await _PizzaService.Save();
            }
        }

    }
}
