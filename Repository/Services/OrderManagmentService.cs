using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class OrderManagmentService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IDishesRepository _dishesRepo;

        public OrderManagmentService(IOrderRepository orderRepo, IDishesRepository dishesRepo)
        {
            _orderRepo = orderRepo;
            _dishesRepo = dishesRepo;
        }

        public async Task<(Order, List<Dishes>)> AddNewOrderAndDishes(string deliveryAddress, 
            string city, string name, string surname, string phoneNumber, int restaurant, 
            int? clientId, List<(Pizza, int)> dishes, string additionalInfo = "")
        {
            List<Dishes> newDishes = new List<Dishes>();
            string newDeliveryAddress = city;
            newDeliveryAddress += " " + deliveryAddress;

            var order = new Order()
            {
                PhoneNumber = phoneNumber,
                AdditionalInformation = additionalInfo,
                Name = name,
                Surname = surname,
                DeliveryAdress = newDeliveryAddress,
                Date = DateTime.Now,
                Status = "P",
                ClientIdClient = clientId,
                RestaurantIdRestaurant = restaurant
            };
            await _orderRepo.Insert(order);

            foreach(var dish in dishes)
            {
                var newDish = new Dishes()
                {
                    OrderIdOrder = order.IdOrder,
                    PizzaIdPizza = dish.Item1.IdPizza,
                    HistoricalCost = dish.Item1.Cost,
                    HistoricalPrice = dish.Item1.Price,
                    Amount = dish.Item2
                };
                newDishes.Add(newDish);
                await _dishesRepo.Insert(newDish);
            }
            return (order, newDishes);
        }

        public async Task<List<(Order, List<Dishes>)>> GetAllClientOrdersAndDishes(int clientId)
        {
            List<(Order, List<Dishes>)> historicalOrders = new List<(Order, List<Dishes>)>();

            try
            {
                var clientOrders = await _orderRepo.GetUserOrders(clientId);
                if (clientOrders is null)
                    throw new Exception("Client orders history is NULL");
                foreach (var clientOrder in clientOrders)
                {
                    var tmp = await _dishesRepo.GetOrderDishes(clientOrder.IdOrder);
                    historicalOrders.Add((clientOrder, tmp));
                }
                return historicalOrders;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
