using CourseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseProject.Controllers
{
    public class ClientController : Controller
    {
        // Объект контекста данных
        private readonly ApplicationContext db;
        private IMemoryCache cache;

        public ClientController(ApplicationContext applicationContext, IMemoryCache cache)
        {
            db = applicationContext;
            this.cache = cache;
        }

        // Метод получения страницы клиентов.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult Index(int page = 1, string filterRepresFIO = "Все", string address = null, string type = null)
        {
            int pageSize = 20;
            List<Client> clients;
            if (!cache.TryGetValue("Clients", out clients))
            {
                clients = db.Clients.ToList();
                cache.Set("Clients", db.Clients.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }
            List<int> Ids = clients.Select(item => item.Id).ToList();
            var repFios = clients.Select(item => item.RepresentativeFIO).ToList();
            repFios.Add("Все");

            if (filterRepresFIO != "Все")
            {
                clients = clients.Where(item => item.RepresentativeFIO == filterRepresFIO).ToList();
            }
            
            if (address != null)
            {
                clients = clients.Where(item => item.Address.Contains(address)).ToList();
            }

            if (type != null)
            {
                clients = type switch
                {
                    "Id" => clients.OrderBy(item => item.Id).ToList(),
                    "name" => clients.OrderBy(item => item.Name).ToList(),
                    "fio" => clients.OrderBy(item => item.RepresentativeFIO).ToList(),
                    "numb" => clients.OrderBy(item => item.Number).ToList(),
                    _ => clients.OrderBy(item => item.Address).ToList(),
                };
            }

            ClientIndexViewModel clientIndexViewModel = new ClientIndexViewModel()
            {
                Clients = clients.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                Ids = Ids,
                PageViewModel = new PageViewModel(clients.Count, page, pageSize),
                FilterRepresFIOs = repFios
            };

            return View(clientIndexViewModel);
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public IActionResult AddClient(ClientIndexViewModel model)
        {
            var names = db.Clients.Select(item => item.Name);
            ViewData["Message"] = "";
            model.Clients = db.Clients.ToList();
            model.Ids = db.Employees.Select(item => item.Id).ToList();
            if (model.Name == null || model.RepresentativeFIO == null || model.Address == null)
            {
                ViewData["Message"] += "Отсутствие значений в строках";
                return View("~/Views/Client/Index.cshtml", model);
            }
            if (names.Contains(model.Name) || model.Name.Length == 0 || model.Name.Length > 25)
            {
                ViewData["Message"] += "Неправильный ввод названия";
                return View("~/Views/Client/Index.cshtml", model);
            }
            else if (model.RepresentativeFIO.Length == 0 || model.RepresentativeFIO.Length > 100)
            {
                ViewData["Message"] += "Неправильный ввод ФИО представителя";
                return View("~/Views/Client/Index.cshtml", model);
            }
            else if (model.Number.ToString().Length < 9 || model.Number.ToString().Length > 13)
            {
                ViewData["Message"] += "Неправильный ввод номера";
                return View("~/Views/Client/Index.cshtml", model);
            }
            else if (model.Address.Length == 0 || model.Address.Length > 40)
            {
                ViewData["Message"] += "Неправильный ввод адреса";
                return View("~/Views/Client/Index.cshtml", model);
            }
            else
            {
                var id = 0;
                if (db.Clients.Count() != 0)
                {
                    id = db.Clients.Select(item => item.Id).Max();
                }
                id++;
                db.Clients.Add(new Client() { Id = id, Name = model.Name, Address = model.Address, Number = model.Number, RepresentativeFIO = model.RepresentativeFIO });
                db.SaveChanges();
                cache.Remove("Clients");
                cache.Set("Clients", db.Clients.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                return RedirectToAction("Index", "Client");
            }
        }

        [Authorize(Roles = "Администратор")]
        public IActionResult DeleteClient(int id)
        {
            ViewData["Message"] = "";
            var client = db.Clients.Where(item => item.Id == id).FirstOrDefault();
            db.Clients.Remove(client);
            db.SaveChanges();
            cache.Remove("Clients");
            cache.Set("Clients", db.Clients.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            return RedirectToAction("Index", "Client");
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public IActionResult UpdateClient(ClientIndexViewModel model, string action = null)
        {
            if (action != null)
            {
                return DeleteClient(model.Id);
            }
            var names = db.Clients.Select(item => item.Name);
            ViewData["Message"] = "";
            model.Clients = db.Clients.ToList();
            model.Ids = db.Clients.Select(item => item.Id).ToList();
            if (model.Name == null || model.RepresentativeFIO == null || model.Address == null)
            {
                ViewData["Message"] += "Отсутствие значений в строках";
                return View("~/Views/Client/Index.cshtml", model);
            }
            if (names.Contains(model.Name) || model.Name.Length == 0 || model.Name.Length > 25)
            {
                ViewData["Message"] += "Неправильный ввод названия";
                return View("~/Views/Client/Index.cshtml", model);
            }
            else if (model.RepresentativeFIO.Length == 0 || model.RepresentativeFIO.Length > 100)
            {
                ViewData["Message"] += "Неправильный ввод ФИО представителя";
                return View("~/Views/Client/Index.cshtml", model);
            }
            else if (model.Number.ToString().Length < 9 || model.Number.ToString().Length > 13)
            {
                ViewData["Message"] += "Неправильный ввод номера";
                return View("~/Views/Client/Index.cshtml", model);
            }
            else if (model.Address.Length == 0 || model.Address.Length > 40)
            {
                ViewData["Message"] += "Неправильный ввод адреса";
                return View("~/Views/Client/Index.cshtml", model);
            }
            else
            {
                var client = db.Clients.Where(item => item.Id == model.Id).FirstOrDefault();
                client.Name = model.Name;
                client.RepresentativeFIO = model.RepresentativeFIO;
                client.Number = model.Number;
                client.Address = model.Address;
                db.SaveChanges();
                cache.Remove("Clients");
                cache.Set("Clients", db.Clients.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                return RedirectToAction("Index", "Client");
            }
        }
    }
}
