using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;


namespace Repository.Services
{
    public class PizzaService
    {
        private readonly IPizzaRepository _pizzaRepo;

        public PizzaService(IPizzaRepository pizzaRepo)
        {
            _pizzaRepo = pizzaRepo;
        }

        public Task<List<Pizza>> GetMenu()
        {
            return _pizzaRepo.GetAll();
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
            var entity = await this._pizzaRepo.Insert(newPizza);
            //await this._AccountService.Save();
            return newPizza;
        }

        public async void ChangePizzaAvailability(int id, bool availability)
        {
            var tmp = await _pizzaRepo.GetAll();
            var primaryKeys = new List<int>();

            foreach (var item in tmp)
                primaryKeys.Add(item.IdPizza);

            if (primaryKeys.Contains(id))
            {
                await _pizzaRepo.UpdatePizzaAvailability(id, availability);
                //await _pizzaRepo.Save();
            }

        }

        public async void DeletePizza(int id)
        {
            var tmp = await _pizzaRepo.GetAll();
            var primaryKeys = new List<int>();

            foreach (var item in tmp)
                primaryKeys.Add(item.IdPizza);

            if (primaryKeys.Contains(id))
            {
                await _pizzaRepo.Delete(id);
                //await _pizzaRepo.Save();
            }
        }
    }
}
