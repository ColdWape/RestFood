using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFood.Controllers
{
    public class TheOrderController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;

        public TheOrderController(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        [HttpGet]
        public IActionResult Order()
        {

            ViewBag.Orders = _dataBaseContext.Orders.Include(i => i.Client).Include(i => i.Dish);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(int dishId)
        {
            
            ClientModel client = await _dataBaseContext.Clients.FirstOrDefaultAsync(u => u.Login == User.Identity.Name);
            DishModel dish = await _dataBaseContext.Dishes.FirstOrDefaultAsync(u => u.Id == dishId);

            OrderModel order = new OrderModel { Dish = dish, Client = client, Status="Created" };
            _dataBaseContext.Orders.Add(order);

            await _dataBaseContext.SaveChangesAsync();


            return NoContent();



        }

        [HttpPost]
        public async Task<IActionResult> Pay()
        {

            foreach (var item in _dataBaseContext.Orders.Include(i => i.Client))
            {
                if (item.Client.Login == User.Identity.Name)
                {
                    item.Status = "Payeed";
                }
            }

            await _dataBaseContext.SaveChangesAsync();


            return RedirectToAction("Order", "TheOrder");



        }
    }
}
