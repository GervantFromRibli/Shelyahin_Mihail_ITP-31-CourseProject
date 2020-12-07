using CourseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseProject.Controllers
{
    public class FurnitureController : Controller
    {
        // Объект контекста данных
        private readonly ApplicationContext db;
        private IMemoryCache cache;

        public FurnitureController(ApplicationContext applicationContext, IMemoryCache cache)
        {
            db = applicationContext;
            this.cache = cache;
        }

        // Метод получения страницы клиентов.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult Index(int page = 1, string material = "Все", decimal price = 0, string type = null)
        {
            int pageSize = 20;
            List<Furniture> furniture;
            if (!cache.TryGetValue("Furniture", out furniture))
            {
                furniture = db.Furniture.ToList();
                cache.Set("Furniture", db.Furniture.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }
            List<int> Ids = furniture.Select(item => item.Id).ToList();
            List<string> materials = furniture.Select(item => item.Material).ToList();

            materials.Add("Все");

            if (material != "Все")
            {
                furniture = furniture.Where(item => item.Material == material).ToList();
            }

            if (price != 0)
            {
                furniture = furniture.Where(item => item.Price == price).ToList();
            }

            if (type != null)
            {
                furniture = type switch
                {
                    "Id" => furniture.OrderBy(item => item.Id).ToList(),
                    "name" => furniture.OrderBy(item => item.Name).ToList(),
                    "descr" => furniture.OrderBy(item => item.Description).ToList(),
                    "material" => furniture.OrderBy(item => item.Material).ToList(),
                    "price" => furniture.OrderBy(item => item.Price).ToList(),
                    _ => furniture.OrderBy(item => item.Count).ToList(),
                };
            }

            FurnitureIndexViewModel furnitureIndexViewModel = new FurnitureIndexViewModel()
            {
                Furniture = furniture.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                Ids = Ids,
                PageViewModel = new PageViewModel(furniture.Count, page, pageSize),
                FilterMaterials = materials
            };

            return View(furnitureIndexViewModel);
        }

        [Authorize(Roles = "Администратор, Работник фабрики")]
        [HttpPost]
        public IActionResult AddFurniture(FurnitureIndexViewModel model)
        {
            var names = db.Furniture.Select(item => item.Name);
            ViewData["Message"] = "";
            model.Furniture = db.Furniture.ToList();
            model.Ids = db.Furniture.Select(item => item.Id).ToList();
            if (model.Name == null || model.Description == null || model.Material == null)
            {
                ViewData["Message"] += "Отсутствие значений в строках";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            if (names.Contains(model.Name) || model.Name.Length == 0 || model.Name.Length > 50)
            {
                ViewData["Message"] += "Неправильный ввод названия";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else if (model.Description.Length == 0 || model.Description.Length > 200)
            {
                ViewData["Message"] += "Неправильный ввод описания";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else if (model.Material.Length == 0 || model.Material.Length > 60)
            {
                ViewData["Message"] += "Неправильный ввод материала";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else if (model.Price <= 0)
            {
                ViewData["Message"] += "Неправильное значение стоимости";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else if (model.Count <= 0)
            {
                ViewData["Message"] += "Неправильное значение количества";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else
            {
                var id = 0;
                if (db.Furniture.Count() != 0)
                {
                    id = db.Furniture.Select(item => item.Id).Max();
                }
                id++;
                db.Furniture.Add(new Furniture() { Id = id, Name = model.Name, Description = model.Description, Material = model.Material, Price = model.Price, Count = model.Count });
                db.SaveChanges();
                cache.Remove("Furniture");
                cache.Set("Furniture", db.Furniture.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                return RedirectToAction("Index", "Furniture");
            }
        }

        [Authorize(Roles = "Администратор, Работник фабрики")]
        public IActionResult DeleteFurniture(int id)
        {
            ViewData["Message"] = "";
            var furniture = db.Furniture.Where(item => item.Id == id).FirstOrDefault();
            db.Furniture.Remove(furniture);
            db.SaveChanges();
            cache.Remove("Furniture");
            cache.Set("Furniture", db.Furniture.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            return RedirectToAction("Index", "Furniture");
        }

        [Authorize(Roles = "Администратор, Работник фабрики")]
        [HttpPost]
        public IActionResult UpdateFurniture(FurnitureIndexViewModel model, string action = null)
        {
            if (action != null)
            {
                return DeleteFurniture(model.Id);
            }
            var names = db.Furniture.Select(item => item.Name);
            ViewData["Message"] = "";
            model.Furniture = db.Furniture.ToList();
            model.Ids = db.Furniture.Select(item => item.Id).ToList();
            if (model.Name == null || model.Description == null || model.Material == null)
            {
                ViewData["Message"] += "Отсутствие значений в строках";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            if (names.Contains(model.Name) || model.Name.Length == 0 || model.Name.Length > 50)
            {
                ViewData["Message"] += "Неправильный ввод названия";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else if (model.Description.Length == 0 || model.Description.Length > 200)
            {
                ViewData["Message"] += "Неправильный ввод описания";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else if (model.Material.Length == 0 || model.Material.Length > 60)
            {
                ViewData["Message"] += "Неправильный ввод материала";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else if (model.Price <= 0)
            {
                ViewData["Message"] += "Неправильное значение стоимости";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else if (model.Count <= 0)
            {
                ViewData["Message"] += "Неправильное значение количества";
                return View("~/Views/Furniture/Index.cshtml", model);
            }
            else
            {
                var furniture = db.Furniture.Where(item => item.Id == model.Id).FirstOrDefault();
                furniture.Name = model.Name;
                furniture.Description = model.Description;
                furniture.Material = model.Material;
                furniture.Price = model.Price;
                furniture.Count = model.Count;
                db.SaveChanges();
                cache.Remove("Furniture");
                cache.Set("Furniture", db.Furniture.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                return RedirectToAction("Index", "Furniture");
            }
        }
    }
}
