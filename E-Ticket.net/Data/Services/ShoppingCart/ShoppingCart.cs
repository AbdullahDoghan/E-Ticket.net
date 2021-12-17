using E_Ticket.net.Data.DbContextConfg;
using E_Ticket.net.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Ticket.net.Data.Services.ShoppingCart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId =session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) {ShoppingCartId = cartId};

        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId ==
            ShoppingCartId).Include(n => n.Moive).ToList());
        }
        public double GetShoppingCartTotal()
        {
            var total= _context.ShoppingCartItems.Where(n=>n.ShoppingCartId == ShoppingCartId).Select(n=>
                n.Moive.Price* n.Amount).Sum();
            return total;
        }


        public void AddItemToCart(Moive movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Moive.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Moive = movie,
                    Amount = 1
                };

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCatr(Moive moive)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Moive.Id == moive.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem); 
                }
            }
            _context.SaveChanges();
        }
        public async Task ClearShoppingCartAsync()
        { 
            var Items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(x=>x.Moive).ToListAsync();

            _context.ShoppingCartItems.RemoveRange(Items);
            await _context.SaveChangesAsync();

        }
    }
}
