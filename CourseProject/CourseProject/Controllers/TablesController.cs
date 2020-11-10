using System.Collections.Generic;
using System.Linq;
using CourseProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.Controllers
{
    // Контроллер представления страниц записей из таблиц
    public class TablesController : Controller
    {
        // Объект контекста данных
        private readonly ApplicationContext db;

        public TablesController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }

        // Метод получения страницы клиентов.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetClients()
        {
            List<Client> clients = db.Clients.ToList();
            return View(clients);
        }

        // Метод получения страницы работников.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetEmployees()
        {
            List<Employee> employees = db.Employees.ToList();
            return View(employees);
        }

        // Метод получения страницы мебели.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetFurniture()
        {
            List<Furniture> furnitures = db.Furniture.ToList();
            return View(furnitures);
        }

        // Метод получения страницы заказов.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetOrders()
        {
            List<Order> orders = db.Orders.ToList();

            // Преобразование данных для удобного представления
            List<OrderViewModel> orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                string clientName = db.Clients.Where(item => item.Id == order.ClientId).Select(item => item.Name).First();
                string furnitName = db.Furniture.Where(item => item.Id == order.FurnitureId).Select(item => item.Name).First();
                string emplFIO = db.Employees.Where(item => item.Id == order.EmployeeId).Select(item => item.FIO).First();
                orderViewModels.Add(new OrderViewModel()
                {
                    Id = order.Id,
                    ClientName = clientName,
                    FurnitureName = furnitName,
                    IsCompleted = order.IsCompleted == 1 ? true : false,
                    DiscountPercent = order.DiscountPercent,
                    FurnitureCount = order.FurnitureCount,
                    Price = order.Price,
                    EmployeeFIO = emplFIO
                });
            }
            return View(orderViewModels);
        }

        // Метод получения страницы накладных.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult GetWaybills()
        {
            List<Waybill> waybills = db.Waybills.ToList();

            // Преобразование данных для удобного представления
            List<WaybillViewModel> waybillViewModels = new List<WaybillViewModel>();
            foreach (var waybill in waybills)
            {
                string furnitName = db.Furniture.Where(item => item.Id == waybill.FurnitureId).Select(item => item.Name).First();
                string emplFIO = db.Employees.Where(item => item.Id == waybill.EmployeeId).Select(item => item.FIO).First();
                waybillViewModels.Add(new WaybillViewModel()
                {
                    Id = waybill.Id,
                    ProviderId = waybill.ProviderId,
                    ProviderName = waybill.ProviderName,
                    DateOfSupply = waybill.DateOfSupply,
                    Material = waybill.Material,
                    Price = waybill.Price,
                    Weight = waybill.Weight,
                    FurnitureName = furnitName,
                    EmployeeFIO = emplFIO
                });
            }
            return View(waybillViewModels);
        }
    }
}
