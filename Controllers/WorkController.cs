using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFood.Controllers
{
    public class WorkController : Controller
    {
        private readonly DataBaseContext _dataBaseContext;


        public WorkController(DataBaseContext dBContext)
        {

            _dataBaseContext = dBContext;
        }
        public IActionResult CheckOrders()
        {
            ViewBag.AdminWork = _dataBaseContext.Orders.Include(i => i.Client).Include(i => i.Dish);

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> OrderIsReady(int orderId)
        {
            OrderModel order = await _dataBaseContext.Orders.FirstOrDefaultAsync(u => u.Id == orderId);


            order.Status = "Ready";
             

            await _dataBaseContext.SaveChangesAsync();


            return RedirectToAction("CheckOrders", "Work");



        }
    }
}
