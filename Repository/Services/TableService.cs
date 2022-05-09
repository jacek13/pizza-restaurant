using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class TableService
    {
        private readonly ITableRepository _tableRepo;

        public TableService(ITableRepository tableRepo)
        {
            _tableRepo = tableRepo;
        }

        public async Task<Table> addNewTable(Restaurant restaurant, int capacity)
        {
            Table newTable = new Table()
            {
                RestaurantIdRestaurant = restaurant.IdRestaurant,
                Capacity = capacity,
            };

            return await _tableRepo.Insert(newTable);
        }

        public async Task<Table> updateCapacity(Table table, int newCapacity)
        {
            return await _tableRepo.updateCapacity(table, newCapacity);
        }

        public async Task<List<Table>> getTablesIn(Restaurant restaurant)
        {
            return await _tableRepo.fetchAllTablesIn(restaurant);
        }

        public async Task<List<Table>> getTablesWithReservationsIn(Restaurant restaurant)
        {
            return await _tableRepo.fetchAllTablesWithReservationsIn(restaurant);
        }


        public async void deleteTableById(int id)
        {
            await _tableRepo.Delete(id);
        }
    }
}
