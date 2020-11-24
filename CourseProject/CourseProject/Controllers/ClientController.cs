using CourseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CourseProject.Controllers
{
    public class ClientController : Controller
    {
        // Объект контекста данных
        private readonly ApplicationContext db;

        public ClientController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }

        // Метод получения страницы клиентов.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult Index()
        {
            List<Client> clients = db.Clients.ToList();
            List<int> Ids = clients.Select(item => item.Id).ToList();

            ClientIndexViewModel clientIndexViewModel = new ClientIndexViewModel()
            {
                Clients = clients,
                Ids = Ids
            };

            return View(clientIndexViewModel);
        }

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
                model.Clients = db.Clients.ToList();
                model.Ids = db.Clients.Select(item => item.Id).ToList();
                return View("~/Views/Client/Index.cshtml", model);
            }
        }

        [HttpPost]
        public IActionResult DeleteClient(ClientIndexViewModel model)
        {
            ViewData["Message"] = "";
            var client = db.Clients.Where(item => item.Id == model.Id).FirstOrDefault();
            db.Clients.Remove(client);
            db.SaveChanges();
            model.Clients = db.Clients.ToList();
            model.Ids = db.Clients.Select(item => item.Id).ToList();
            return View("~/Views/Client/Index.cshtml", model);
        }

        [HttpPost]
        public IActionResult UpdateClient(ClientIndexViewModel model)
        {
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
                return View("~/Views/Client/Index.cshtml", model);
            }
        }
    }
}
