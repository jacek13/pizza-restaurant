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
        private readonly IRepository<Order> _orderRepo;

        public OrderService(IRepository<Order> orderRepo)
        {
            _orderRepo = orderRepo;
        }
    }
}
