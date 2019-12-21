using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristAgency.Domain.Models.EfModels;

namespace TouristAgency.Domain.ViewModels
{
    public class UsersOrders
    {
        List<Order> orders;

        public UsersOrders(List<Order> orders)
        {
            this.orders = orders;
        }

        public List<Order> GetOrdersWithRealPrice()
        {
            List<Order> ordersWithRealPrice = new List<Order>();
            foreach (var order in orders)
            {
                if (order.Discount == 0 || order.Discount == null)
                {
                    ordersWithRealPrice.Add(order);
                }
                else
                {
                    double? prise = order.Journey.Tour.Price - ((order.Journey.Tour.Price * order.Discount) / 100);
                    order.Journey.Tour.Price = (int?)prise;
                    ordersWithRealPrice.Add(order);
                }
            }
            return ordersWithRealPrice;
        }
    }
}
