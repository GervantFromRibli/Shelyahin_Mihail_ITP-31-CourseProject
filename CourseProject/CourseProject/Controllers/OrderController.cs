using CourseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class OrderController : Controller
    {
        // Объект контекста данных
        private readonly ApplicationContext db;

        public OrderController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }

        // Метод получения страницы клиентов.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult Index(int page = 1, string furniture = "Все", string clientName = "Все")
        {
            return View(GetViewModel(page, furniture, clientName));
        }

        [Authorize(Roles = "Администратор, Работник фабрики")]
        [HttpPost]
        public IActionResult AddOrder(OrderIndexViewModel model)
        {
            ViewData["Message"] = "";
            if (model.FurnitureCount <= 0)
            {
                ViewData["Message"] += "Неправильное значение количества мебели";
                return View("~/Views/Order/Index.cshtml", GetViewModel(1));
            }
            else if (model.DiscountPercent < 0 || model.DiscountPercent > 99)
            {
                ViewData["Message"] += "Неправильное значение скидки";
                return View("~/Views/Order/Index.cshtml", GetViewModel(1));
            }
            else if (model.Price <= 0)
            {
                ViewData["Message"] += "Неправильное значение стоимости";
                return View("~/Views/Order/Index.cshtml", GetViewModel(1));
            }
            else
            {
                var id = 0;
                if (db.Orders.Count() != 0)
                {
                    id = db.Orders.Select(item => item.Id).Max();
                }
                id++;
                db.Orders.Add(new Order()
                {
                    Id = id,
                    ClientId = db.Clients.Where(item => item.Name == model.ClientName).First().Id,
                    DiscountPercent = model.DiscountPercent,
                    FurnitureCount = model.FurnitureCount, 
                    Price = model.Price, 
                    IsCompleted = model.IsCompleted ? 1 : 0, 
                    EmployeeId = db.Employees.Where(item => item.FIO == model.EmployeeFIO).First().Id, 
                    FurnitureId = db.Furniture.Where(item => item.Name == model.FurnitureName).First().Id 
                });
                db.SaveChanges();
                return View("~/Views/Order/Index.cshtml", GetViewModel(1));
            }
        }

        [Authorize(Roles = "Администратор, Работник фабрики")]
        [HttpPost]
        public IActionResult DeleteOrder(OrderIndexViewModel model)
        {
            ViewData["Message"] = "";
            var order = db.Orders.Where(item => item.Id == model.Id).FirstOrDefault();
            db.Orders.Remove(order);
            db.SaveChanges();
            return View("~/Views/Order/Index.cshtml", GetViewModel(1));
        }

        [Authorize(Roles = "Администратор, Работник фабрики")]
        [HttpPost]
        public IActionResult UpdateOrder(OrderIndexViewModel model)
        {
            ViewData["Message"] = "";
            if (model.FurnitureCount <= 0)
            {
                ViewData["Message"] += "Неправильное значение количества мебели";
                return View("~/Views/Order/Index.cshtml", GetViewModel(1));
            }
            else if (model.DiscountPercent < 0 || model.DiscountPercent > 99)
            {
                ViewData["Message"] += "Неправильное значение скидки";
                return View("~/Views/Order/Index.cshtml", GetViewModel(1));
            }
            else if (model.Price <= 0)
            {
                ViewData["Message"] += "Неправильное значение стоимости";
                return View("~/Views/Order/Index.cshtml", GetViewModel(1));
            }
            else
            {
                var order = db.Orders.Where(item => item.Id == model.Id).FirstOrDefault();
                order.DiscountPercent = model.DiscountPercent;
                order.ClientId = db.Clients.Where(item => item.Name == model.ClientName).First().Id;
                order.EmployeeId = db.Employees.Where(item => item.FIO == model.EmployeeFIO).First().Id;
                order.Price = model.Price;
                order.FurnitureCount = model.FurnitureCount;
                order.FurnitureId = db.Furniture.Where(item => item.Name == model.FurnitureName).First().Id;
                order.IsCompleted = model.IsCompleted ? 1 : 0;
                db.SaveChanges();
                return View("~/Views/Order/Index.cshtml", GetViewModel(1));
            }
        }

        private OrderIndexViewModel GetViewModel(int page, string furnitureName = "Все", string clientName = "Все")
        {
            int pageSize = 20;
            List<int> Ids = db.Orders.Select(item => item.Id).ToList();
            List<Order> orders = db.Orders.ToList();
            List<Employee> employees = db.Employees.ToList();
            List<Furniture> furniture = db.Furniture.ToList();
            List<Client> clients = db.Clients.ToList();
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                orderViewModels.Add(new OrderViewModel()
                {
                    Id = order.Id,
                    ClientName = clients.Where(item => item.Id == order.ClientId).First().Name,
                    DiscountPercent = order.DiscountPercent,
                    EmployeeFIO = employees.Where(item => item.Id == order.EmployeeId).First().FIO,
                    FurnitureCount = order.FurnitureCount,
                    FurnitureName = furniture.Where(item => item.Id == order.FurnitureId).First().Name,
                    IsCompleted = order.IsCompleted == 0 ? false : true,
                    Price = order.Price
                });
            }

            List<string> furnituresNames = furniture.Select(item => item.Name).ToList();
            List<string> clientNames = clients.Select(item => item.Name).ToList();
            furnituresNames.Add("Все");
            clientNames.Add("Все");

            if (furnitureName != "Все")
            {
                orderViewModels = orderViewModels.Where(item => item.FurnitureName == furnitureName).ToList();
            }

            if (clientName != "Все")
            {
                orderViewModels = orderViewModels.Where(item => item.ClientName == clientName).ToList();
            }

            OrderIndexViewModel orderIndexViewModel = new OrderIndexViewModel()
            {
                OrderViewModels = orderViewModels.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                Ids = Ids,
                ClientNames = clients.Select(item => item.Name).ToList(),
                EmployeesFIOs = employees.Select(item => item.FIO).ToList(),
                FurnitureNames = furniture.Select(item => item.Name).ToList(),
                PageViewModel = new PageViewModel(orderViewModels.Count, page, pageSize),
                FilterClients = clientNames,
                FilterFurnitures = furnituresNames
            };
            return orderIndexViewModel;
        }
    }
}
