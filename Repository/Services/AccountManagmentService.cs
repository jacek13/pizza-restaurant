using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class AccountManagmentService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IClientRepository _clientRepo;

        public AccountManagmentService(IAccountRepository accountRepo, IClientRepository clientRepo)
        {
            _accountRepo = accountRepo;
            _clientRepo = clientRepo;
        }

        public async Task<(Account,Client)> AddNewClient(String e_mail, String login, String password, String name, String surname, DateTime accountCreationDate, String PhoneNumber,
                                        int points, String address, String role)
        {
            //var accountCount = _accountRepo.lastId;
            //var clientCount = _clientRepo.lastId;

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

            var newClient = new Client()
            {
                //IdClient = clientCount,
                Points = points,
                Address = address,
                AccountIdAccount = newAccount.IdAccount
            };

            // Account creation <- tabela
            var entity = await _accountRepo.Insert(newAccount);
            //await this._AccountService.Save();

            // Client creation <- tabela
            newClient.AccountIdAccount = entity.IdAccount;
            //newClient.Account = newAccount;
            await _clientRepo.Insert(newClient);
            //await this._ClientService.Save();

            return (newAccount,newClient);
        }

        public async void DeleteClient(int id)
        {
            var tmp = await _clientRepo.GetAll();
            var primaryKeys = new List<int>();

            foreach (var item in tmp)
                primaryKeys.Add(item.IdClient);

            if (primaryKeys.Contains(id))
            {
                await _accountRepo.Delete(id);
                //await this._AccountService.Save();
                await _clientRepo.Delete(id);
                //await this._ClientService.Save();
            }
        }

        //Update client&account
        public async Task<(Account, Client)> UpdateAccountAndClient(int idAccount, String e_mail, String login, 
            String password, String name, String surname, DateTime? accountCreationDate, String PhoneNumber,
                                        int points, String address, String role, int idClient)
        {
            var updatedAccount = new Account()
            {
                IdAccount = idAccount,
                EMail = e_mail,
                Login = login,
                Password = password,
                Name = name,
                Surname = surname,
                AccountCreationDate = accountCreationDate,
                PhoneNumber = PhoneNumber,
                Role = role
            };

            var updatedClient = new Client()
            {
                IdClient = idClient,
                Points = points,
                Address = address,
                AccountIdAccount = updatedAccount.IdAccount
            };

            var entity = await _accountRepo.Update(updatedAccount);
          
            await _clientRepo.Update(updatedClient);
            
            return (updatedAccount, updatedClient);

        }

    }
}
