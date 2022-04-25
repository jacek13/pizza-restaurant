using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using DataBaseAccess.Models;

namespace Repository.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<char> changeStatus(int orderId, char newStatus)
        {
            char newStatusValdation;
            // Validation
            switch (newStatus)
            {
                case 'P':   // preparing
                    newStatusValdation = 'P';
                    break;
                case 'D':   // delivery
                    newStatusValdation = 'D';
                    break;
                case 'F':   // finished
                    newStatusValdation = 'F';
                    break;
                case 'C':   // canceled
                    newStatusValdation = 'C';
                    break;
                default:    // unknown
                    newStatusValdation = 'U';
                    break;
            }
            await _orderRepo.UpdateOrderStatus(orderId, newStatusValdation);
            return newStatusValdation;
        }
    }
}
