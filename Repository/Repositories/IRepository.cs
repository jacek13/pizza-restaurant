using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IRepository<T1> where T1 : class
    {
        // Wystawienie interfejsu do TODO:
        // * Dodawania nowego klienta -> modyfikacja tebel: account, client
        // - Dodawania nowego menadżera -> modyfikacja tebel: account, manager, manager assignment
        // * Dodawania nowej pizzy -> modyfikacja tebeli: pizza
        // - Dodawania nowej restauracji
        // * DOdawanie nowej rezerwacji -> modyfikacja tabeli: reservation
        // - Pobierania informacji o kliencie
        // - Pobieranie informacji o menedżerze -> tabele: account, manager, manager assignment, restaurant?
        // * Pobieranie ifnormacji o zamówieniu -> tabele: order, dishes, pizza
        // - Pobieranie ifnormacji o restauracji
        // - 

        Task<List<T1>> GetAll();
        Task<T1> GetById(int id);
        Task<T1> Insert(T1 entity);
        Task<T1> Update(T1 entity); //TODO
        Task Delete(int id);

        // Task Save();
        // Deleted Task Save - because we would have need to pass the context as a param
        // Can simply save it inside Insert/Update etc. methods
    }
}
