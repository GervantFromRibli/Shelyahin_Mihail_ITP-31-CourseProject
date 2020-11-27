using CourseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseProject.Controllers
{
    public class WaybillController : Controller
    {
        // Объект контекста данных
        private readonly ApplicationContext db;
        private IMemoryCache cache;

        public WaybillController(ApplicationContext applicationContext, IMemoryCache cache)
        {
            db = applicationContext;
            this.cache = cache;
        }

        // Метод получения страницы клиентов.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult Index(int page = 1, string providerName = null, string furnitName = "Все")
        {
            return View(GetViewModel(page, providerName, furnitName));
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public IActionResult AddWaybill(WaybillIndexViewModel model)
        {
            ViewData["Message"] = "";
            if (model.ProviderName == null || model.Material == null || model.DateOfSupply == null)
            {
                ViewData["Message"] += "Отсутствие значений в строках";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.ProviderId <= 0)
            {
                ViewData["Message"] += "Неправильное значение номера поставщика";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.Weight <= 0)
            {
                ViewData["Message"] += "Неправильное значение веса";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.Price <= 0)
            {
                ViewData["Message"] += "Неправильное значение стоимости";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.ProviderName.Length > 100)
            {
                ViewData["Message"] += "Неправильный ввод названия поставщика";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.Material.Length > 60)
            {
                ViewData["Message"] += "Неправильный ввод материала";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else
            {
                var id = 0;
                if (db.Waybills.Count() != 0)
                {
                    id = db.Waybills.Select(item => item.Id).Max();
                }
                id++;   
                db.Waybills.Add(new Waybill()
                {
                    Id = id,
                    ProviderId = model.ProviderId,
                    ProviderName = model.ProviderName,
                    Price = model.Price,
                    DateOfSupply = model.DateOfSupply,
                    EmployeeId = db.Employees.Where(item => item.FIO == model.EmployeeFIO).First().Id,
                    FurnitureId = db.Furniture.Where(item => item.Name == model.FurnitureName).First().Id,
                    Material = model.Material,
                    Weight = model.Weight
                });
                db.SaveChanges();
                cache.Remove("Waybills");
                return RedirectToAction("Index", "Waybill");
            }
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public IActionResult DeleteWaybill(WaybillIndexViewModel model)
        {
            ViewData["Message"] = "";
            var waybill = db.Waybills.Where(item => item.Id == model.Id).FirstOrDefault();
            db.Waybills.Remove(waybill);
            db.SaveChanges();
            cache.Remove("Waybills");
            return RedirectToAction("Index", "Waybill");
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public IActionResult UpdateWaybill(WaybillIndexViewModel model)
        {
            ViewData["Message"] = "";
            if (model.ProviderName == null || model.Material == null || model.DateOfSupply == null)
            {
                ViewData["Message"] += "Отсутствие значений в строках";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.ProviderId <= 0)
            {
                ViewData["Message"] += "Неправильное значение номера поставщика";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.Weight <= 0)
            {
                ViewData["Message"] += "Неправильное значение веса";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.Price <= 0)
            {
                ViewData["Message"] += "Неправильное значение стоимости";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.ProviderName.Length > 100)
            {
                ViewData["Message"] += "Неправильный ввод названия поставщика";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else if (model.Material.Length > 60)
            {
                ViewData["Message"] += "Неправильный ввод материала";
                return View("~/Views/Waybill/Index.cshtml", GetViewModel());
            }
            else
            {
                var waybill = db.Waybills.Where(item => item.Id == model.Id).FirstOrDefault();
                waybill.ProviderId = model.ProviderId;
                waybill.ProviderName = model.ProviderName;
                waybill.Price = model.Price;
                waybill.DateOfSupply = model.DateOfSupply;
                waybill.EmployeeId = db.Employees.Where(item => item.FIO == model.EmployeeFIO).First().Id;
                waybill.FurnitureId = db.Furniture.Where(item => item.Name == model.FurnitureName).First().Id;
                waybill.Material = model.Material;
                waybill.Weight = model.Weight;
                db.SaveChanges();
                cache.Remove("Waybills");
                return RedirectToAction("Index", "Waybill");
            }
        }

        private WaybillIndexViewModel GetViewModel(int page = 1, string providerName = null, string furnitName = "Все")
        {
            int pageSize = 20;
            List<Employee> employees = db.Employees.ToList();
            List<Furniture> furniture = db.Furniture.ToList();
            List<WaybillViewModel> waybillViewModels;
            if (!cache.TryGetValue("Waybills", out waybillViewModels))
            {
                List<Waybill> waybills = db.Waybills.ToList();
                waybillViewModels = new List<WaybillViewModel>();
                foreach (var waybill in waybills)
                {
                    waybillViewModels.Add(new WaybillViewModel()
                    {
                        Id = waybill.Id,
                        ProviderId = waybill.ProviderId,
                        ProviderName = waybill.ProviderName,
                        EmployeeFIO = employees.Where(item => item.Id == waybill.EmployeeId).First().FIO,
                        DateOfSupply = waybill.DateOfSupply,
                        FurnitureName = furniture.Where(item => item.Id == waybill.FurnitureId).First().Name,
                        Material = waybill.Material,
                        Price = waybill.Price,
                        Weight = waybill.Weight
                    });
                }
                cache.Set("Waybills", db.Waybills.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }
            List<int> Ids = waybillViewModels.Select(item => item.Id).ToList();
            List<string> furnitNames = furniture.Select(item => item.Name).ToList();
            furnitNames.Add("Все");

            if (providerName != null)
            {
                waybillViewModels = waybillViewModels.Where(item => item.ProviderName.Contains(providerName)).ToList();
            }
            if (furnitName != "Все")
            {
                waybillViewModels = waybillViewModels.Where(item => item.FurnitureName == furnitName).ToList();
            }

            WaybillIndexViewModel waybillIndexViewModel = new WaybillIndexViewModel()
            {
                WaybillViewModels = waybillViewModels.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                Ids = Ids,
                EmployeesFIOs = employees.Select(item => item.FIO).ToList(),
                FurnitureNames = furniture.Select(item => item.Name).ToList(),
                PageViewModel = new PageViewModel(waybillViewModels.Count, page, pageSize),
                FilterFurnitureNames = furnitNames
            };
            return waybillIndexViewModel;
        }
    }
}
