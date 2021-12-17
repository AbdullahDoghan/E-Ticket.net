using E_Ticket.net.Data.Services.MovieService;
using E_Ticket.net.Data.Services.Orders;
using E_Ticket.net.Data.Services.ShoppingCart;
using E_Ticket.net.Data.Vm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Ticket.net.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        public OrdersController(IMovieService movieService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }
        public async Task<IActionResult> AllOrders()
        {
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            string userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders =await _orderService.GetOrdersByUserIdAsync(userId,userRole);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVm()
            {
                shoppingCart = _shoppingCart,
                shoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<RedirectToActionResult> AddItemToShoppingCart(int Id)
        {
            var Item = await _movieService.GetMovieById(Id);
            if (Item != null)
            {
                _shoppingCart.AddItemToCart(Item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int Id)
        {
            var Item = await _movieService.GetMovieById(Id);
            if (Item != null)
            {
                _shoppingCart.RemoveItemFromCatr(Item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string UserEmailAddress = User.FindFirstValue(ClaimTypes.Email);

           await _orderService.StoreOrderAsync(items , userId , UserEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        
        }
    }
}
