using CourseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseProject.Controllers
{
    public class EmployeeController : Controller
    {
        // Объект контекста данных
        private readonly ApplicationContext db;
        private IMemoryCache cache;

        public EmployeeController(ApplicationContext applicationContext, IMemoryCache cache)
        {
            db = applicationContext;
            this.cache = cache;
        }

        // Метод получения страницы работников.
        // Данная страница кэшируется на 286 секунд.
        [ResponseCache(CacheProfileName = "TablesCaching")]
        public IActionResult Index(int page = 1, string position = "Все", string education = null)
        {
            int pageSize = 20;
            List<Employee> employees;
            if (!cache.TryGetValue("Employees", out employees))
            {
                employees = db.Employees.ToList();
                cache.Set("Employees", db.Employees.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }
            List<int> Ids = employees.Select(item => item.Id).ToList();
            List<string> positions = employees.Select(item => item.Position).ToList();
            positions.Add("Все");

            if (position != "Все")
            {
                employees = employees.Where(item => item.Position == position).ToList();
            }

            if (education != null)
            {
                employees = employees.Where(item => item.Education.Contains(education)).ToList();
            }


            EmployeeIndexViewModel employeeIndexViewModel = new EmployeeIndexViewModel()
            {
                Employees = employees.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                Ids = Ids,
                PageViewModel = new PageViewModel(employees.Count, page, pageSize),
                FilterPositions = positions
            };

            return View(employeeIndexViewModel);
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public IActionResult AddEmployee(EmployeeIndexViewModel model)
        {
            var fios = db.Employees.Select(item => item.FIO);
            ViewData["Message"] = "";
            model.Employees = db.Employees.ToList();
            model.Ids = db.Employees.Select(item => item.Id).ToList();
            if (model.FIO == null || model.Education == null || model.Position == null)
            {
                ViewData["Message"] += "Отсутствие значений в строках";
                return View("~/Views/Employee/Index.cshtml", model);
            }
            if (fios.Contains(model.FIO) || model.FIO.Length == 0 || model.FIO.Length > 100)
            {
                ViewData["Message"] += "Неправильный ввод ФИО";
                return View("~/Views/Employee/Index.cshtml", model);
            }
            else if (model.Position.Length == 0 || model.Position.Length > 50)
            {
                ViewData["Message"] += "Неправильный ввод должности";
                return View("~/Views/Employee/Index.cshtml", model);
            }
            else if (model.Education.Length < 9 || model.Education.Length > 200)
            {
                ViewData["Message"] += "Неправильный ввод образования";
                return View("~/Views/Employee/Index.cshtml", model);
            }
            else
            {
                var id = 0;
                if (db.Employees.Count() != 0)
                {
                    id = db.Employees.Select(item => item.Id).Max();
                }
                id++;
                db.Employees.Add(new Employee() { Id = id, FIO = model.FIO, Position = model.Position, Education = model.Education });
                db.SaveChanges();
                cache.Remove("Employees");
                cache.Set("Employees", db.Employees.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                return RedirectToAction("Index", "Employee");
            }
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public IActionResult DeleteEmployee(EmployeeIndexViewModel model)
        {
            ViewData["Message"] = "";
            var employee = db.Employees.Where(item => item.Id == model.Id).FirstOrDefault();
            db.Employees.Remove(employee);
            db.SaveChanges();
            cache.Remove("Employees");
            cache.Set("Employees", db.Employees.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            return RedirectToAction("Index", "Employee");
        }

        [Authorize(Roles = "Администратор")]
        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeIndexViewModel model)
        {
            var fios = db.Employees.Select(item => item.FIO);
            ViewData["Message"] = "";
            model.Employees = db.Employees.ToList();
            model.Ids = db.Employees.Select(item => item.Id).ToList();
            if (model.FIO == null || model.Education == null || model.Position == null)
            {
                ViewData["Message"] += "Отсутствие значений в строках";
                return View("~/Views/Employee/Index.cshtml", model);
            }
            if (fios.Contains(model.FIO) || model.FIO.Length == 0 || model.FIO.Length > 100)
            {
                ViewData["Message"] += "Неправильный ввод ФИО";
                return View("~/Views/Employee/Index.cshtml", model);
            }
            else if (model.Position.Length == 0 || model.Position.Length > 50)
            {
                ViewData["Message"] += "Неправильный ввод должности";
                return View("~/Views/Employee/Index.cshtml", model);
            }
            else if (model.Education.Length < 0 || model.Education.Length > 200)
            {
                ViewData["Message"] += "Неправильный ввод образования";
                return View("~/Views/Employee/Index.cshtml", model);
            }
            else
            {
                var employee = db.Employees.Where(item => item.Id == model.Id).FirstOrDefault();
                employee.FIO = model.FIO;
                employee.Position = model.Position;
                employee.Education = model.Education;
                db.SaveChanges();
                cache.Remove("Employees");
                cache.Set("Employees", db.Employees.ToList(), new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
                return RedirectToAction("Index", "Employee");
            }
        }
    }
}
