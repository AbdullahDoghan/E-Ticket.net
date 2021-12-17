using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _Db;
        public OrderService(AppDbContext db)
        {
            _Db = db;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId, string userRole)
        {
            var orders =await _Db.Orders.Include(x => x.OrderItems).ThenInclude(x=>x.Moive).Include(z=>z.User).ToListAsync();
            if (userRole != "Admin")
            {
                orders = orders.Where(x => x.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
            };
            await _Db.Orders.AddAsync(order);
            await _Db.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItems()
                {
                    Amout = item.Amount,
                    MovieId = item.Moive.Id,
                    OrderId = order.Id,
                    Price = item.Moive.Price,
                };
                 await _Db.OrderItems.AddAsync(orderItem);
               
            }
            await _Db.SaveChangesAsync();
        }
    }
}
