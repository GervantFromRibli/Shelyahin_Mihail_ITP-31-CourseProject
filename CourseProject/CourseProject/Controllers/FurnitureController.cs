using CourseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Controllers
{
    public class FurnitureController : Controller
    {
        // Объект контекста данных
        private readonly ApplicationContext db;

        public FurnitureController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }

        // Метод получения страницы клиентов.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult Index()
        {
            List<Furniture> furniture = db.Furniture.ToList();
            List<int> Ids = furniture.Select(item => item.Id).ToList();

            FurnitureIndexViewModel furnitureIndexViewModel = new FurnitureIndexViewModel()
            {
                Furniture = furniture,
                Ids = Ids
            };

            return View(furnitureIndexViewModel);
        }

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
                model.Furniture = db.Furniture.ToList();
                model.Ids = db.Furniture.Select(item => item.Id).ToList();
                return View("~/Views/Furniture/Index.cshtml", model);
            }
        }

        [HttpPost]
        public IActionResult DeleteFurniture(FurnitureIndexViewModel model)
        {
            ViewData["Message"] = "";
            var furniture = db.Furniture.Where(item => item.Id == model.Id).FirstOrDefault();
            db.Furniture.Remove(furniture);
            db.SaveChanges();
            model.Furniture = db.Furniture.ToList();
            model.Ids = db.Furniture.Select(item => item.Id).ToList();
            return View("~/Views/Furniture/Index.cshtml", model);
        }

        [HttpPost]
        public IActionResult UpdateFurniture(FurnitureIndexViewModel model)
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
                var furniture = db.Furniture.Where(item => item.Id == model.Id).FirstOrDefault();
                furniture.Name = model.Name;
                furniture.Description = model.Description;
                furniture.Material = model.Material;
                furniture.Price = model.Price;
                furniture.Count = model.Count;
                db.SaveChanges();
                return View("~/Views/Furniture/Index.cshtml", model);
            }
        }
    }
}
