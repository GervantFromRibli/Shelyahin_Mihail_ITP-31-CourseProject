using CourseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseProject.Controllers
{
    public class OrderController : Controller
    {
        // Объект контекста данных
        private readonly ApplicationContext db;
        private IMemoryCache cache;

        public OrderController(ApplicationContext applicationContext, IMemoryCache cache)
        {
            db = applicationContext;
            this.cache = cache;
        }

        // Метод получения страницы клиентов.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult Index(int page = 1, string furniture = "Все", string clientName = "Все", string type = null)
        {
            return View(GetViewModel(page, furniture, clientName, type));
        }

        [Authorize(Roles = "Администратор, Работник фабрики")]
        [HttpPost]
        public IActionResult AddOrder(OrderIndexViewModel model)
        {
            ViewData["Message"] = "";
            if (model.FurnitureCount <= 0)
            {
                ViewData["Message"] += "Неправильное значение количества мебели";
                return View("~/Views/Order/Index.cshtml", GetViewModel());
            }
            else if (model.DiscountPercent < 0 || model.DiscountPercent > 99)
            {
                ViewData["Message"] += "Неправильное значение скидки";
                return View("~/Views/Order/Index.cshtml", GetViewModel());
            }
            else if (model.Price <= 0)
            {
                ViewData["Message"] += "Неправильное значение стоимости";
                return View("~/Views/Order/Index.cshtml", GetViewModel());
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
                cache.Remove("Orders");
                return RedirectToAction("Index", "Order");
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
            cache.Remove("Orders");
            return RedirectToAction("Index", "Order");
        }

        [Authorize(Roles = "Администратор, Работник фабрики")]
        [HttpPost]
        public IActionResult UpdateOrder(OrderIndexViewModel model)
        {
            ViewData["Message"] = "";
            if (model.FurnitureCount <= 0)
            {
                ViewData["Message"] += "Неправильное значение количества мебели";
                return View("~/Views/Order/Index.cshtml", GetViewModel());
            }
            else if (model.DiscountPercent < 0 || model.DiscountPercent > 99)
            {
                ViewData["Message"] += "Неправильное значение скидки";
                return View("~/Views/Order/Index.cshtml", GetViewModel());
            }
            else if (model.Price <= 0)
            {
                ViewData["Message"] += "Неправильное значение стоимости";
                return View("~/Views/Order/Index.cshtml", GetViewModel());
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
                cache.Remove("Orders");
                return RedirectToAction("Index", "Order");
            }
        }

        private OrderIndexViewModel GetViewModel(int page = 1, string furnitureName = "Все", string clientName = "Все", string type = null)
        {
            int pageSize = 20;
            List<Employee> employees = db.Employees.ToList();
            List<Furniture> furniture = db.Furniture.ToList();
            List<Client> clients = db.Clients.ToList();
            List<OrderViewModel> orderViewModels;
            if (!cache.TryGetValue("Orders", out orderViewModels))
            {
                List<Order> orders = db.Orders.ToList();
                orderViewModels = new List<OrderViewModel>();
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
                        IsCompleted = order.IsCompleted != 0,
                        Price = order.Price
                    });
                }
                cache.Set("Orders", db.Orders.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }
            List<int> Ids = orderViewModels.Select(item => item.Id).ToList();
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

            if (type != null)
            {
                orderViewModels = type switch
                {
                    "Id" => orderViewModels.OrderBy(item => item.Id).ToList(),
                    "client" => orderViewModels.OrderBy(item => item.ClientName).ToList(),
                    "furnit" => orderViewModels.OrderBy(item => item.FurnitureName).ToList(),
                    "count" => orderViewModels.OrderBy(item => item.FurnitureCount).ToList(),
                    "price" => orderViewModels.OrderBy(item => item.Price).ToList(),
                    "disc" => orderViewModels.OrderBy(item => item.DiscountPercent).ToList(),
                    "isComp" => orderViewModels.OrderBy(item => item.IsCompleted).ToList(),
                    _ => orderViewModels.OrderBy(item => item.EmployeeFIO).ToList(),
                };
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
