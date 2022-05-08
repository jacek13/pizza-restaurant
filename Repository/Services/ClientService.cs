using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepo;

        public ClientService(IClientRepository accountRepo)
        {
            _clientRepo = accountRepo;
        }

        public Task<List<Client>> GetClients()
        {
            return _clientRepo.GetAll();
        }

        public async Task<Client> GetClientByAccountId(int accountId)
        {
            var clients = await _clientRepo.GetAll();
            var clientTmp = new Client();
            bool found = false;

            foreach (var client in clients)
                if (client.AccountIdAccount == accountId)
                {
                    clientTmp = client;
                    found = true;
                    break;
                }
            if (found)
                return clientTmp;
            else throw new Exception("Client not found!");
        }

        public async void UpdateClientPoints(int id, int points)
        {
            var tmp = await _clientRepo.GetAll();
            var primaryKeys = new List<int>();

            foreach (var item in tmp)
                primaryKeys.Add(item.IdClient);

            if (primaryKeys.Contains(id))
            {
                Client modifiedClient = await _clientRepo.UpdatePoints(id, points);
            }
        }


    }
}
