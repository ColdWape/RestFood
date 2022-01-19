using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestFood.Controllers
{
    public class LogController : Controller
    {

        private readonly DataBaseContext _dataBaseContext;


        public LogController(DataBaseContext dBContext)
        {

            _dataBaseContext = dBContext;
        }



        [HttpGet]
        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewUser(RegModel model)
        {
            if (ModelState.IsValid)
            {
                ClientModel client = await _dataBaseContext.Clients.FirstOrDefaultAsync(u => u.Login == model.Login);
                if (client == null)
                {
                    // добавляем пользователя в бд
                    client = new ClientModel { Login = model.Login, Password = model.Password, Name = model.Name};

                    Role userRole = await _dataBaseContext.Roles.FirstOrDefaultAsync(r => r.Name == "client");

                    if (userRole != null)
                        client.Role = userRole;

                    _dataBaseContext.Clients.Add(client);
                    await _dataBaseContext.SaveChangesAsync();

                    await Authenticate(client); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже есть!");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LogModel model)
        {
            if (ModelState.IsValid)
            {
                ClientModel user = await _dataBaseContext.Clients
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Неверный логин и/или пароль");
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(ClientModel person)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),

                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }

   


}
