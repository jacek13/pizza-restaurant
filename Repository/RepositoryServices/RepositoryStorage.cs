using DataBaseAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryServices
{
    public class RepositoryStorage
    {
        private readonly pizza_restaurant_ver_3Context _Restaurant_Ver3Context = null;
        private ClientService _ClientService = null;
        private AccountService _AccountService = null;
        public RepositoryStorage(pizza_restaurant_ver_3Context Restaurant_Ver3Context)
        {
            this._Restaurant_Ver3Context = Restaurant_Ver3Context;
            _ClientService = new ClientService(_Restaurant_Ver3Context);
            _AccountService = new AccountService(_Restaurant_Ver3Context);
        }

        public async Task AddNewClient(String e_mail, String login, String password, String name, String surname, DateTime accountCreationDate, String PhoneNumber,
                                        int points = 0, String address = "Unknown")
        {
            var accountCount = _AccountService.GetAll().Result.Count;
            var clientCount = _ClientService.GetAll().Result.Count;

            // TODO trzeba sprawdzić czy poprawne dane, Rozwiążemy to sprawdzając dane już w formularzu. Tutaj będą dodatkowe zabezpieczenia na wypadek ominięcia tamtych
            var newAccount = new Account() 
            { 
                IdAccount = accountCount, 
                EMail = e_mail, 
                Login = login, 
                Password = password,
                Name = name, 
                Surname = surname, 
                AccountCreationDate = accountCreationDate, 
                PhoneNumber = PhoneNumber 
            };

            var newClient = new Client
            {
                IdClient = clientCount,
                Points = points,
                Address = address,
                AccountIdAccount = accountCount
            };

            await this._AccountService.Insert(newAccount);
            await this._AccountService.Save();
            await this._ClientService.Insert(newClient);
            await this._ClientService.Save();


        }
    }
}
