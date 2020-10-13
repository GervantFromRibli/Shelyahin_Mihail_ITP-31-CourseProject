using ContextLibrary;
using ContextLibrary.Domain_Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
    // Класс бизнес-логики приложения
    public class BLL
    {
        // Метод получения списка клиентов (Метод 1)
        public List<Client> GetClients(DbContextOptions<ApplicationContext> options)
        {
            List<Client> clients;
            using (ApplicationContext context = new ApplicationContext(options))
            {
                clients = context.Clients.ToList();
            }
            return clients;
        }
        // Метод получения списка клиентов по полю "Представитель" (Метод 2)
        public List<Client> GetClientByRepresentative(DbContextOptions<ApplicationContext> options, string FIO)
        {
            List<Client> clients;
            using (ApplicationContext context = new ApplicationContext(options))
            {
                clients = context.Clients.Select(elem => elem).Where(elem => elem.RepresentativeFIO == FIO).ToList();
            }
            return clients;
        }
        // Метод получения списка дат и сум накладных на каждую дату (Метод 3)
        public Dictionary<DateTime, decimal> GetSumInDates(DbContextOptions<ApplicationContext> options)
        {
            Dictionary<DateTime, decimal> sumInDates = new Dictionary<DateTime, decimal>();
            decimal sum;
            using (ApplicationContext context = new ApplicationContext(options))
            {
                var dateSums = context.Waybills.ToList().GroupBy(item => item.DateOfSupply).Select(item => item);
                foreach (var elem in dateSums)
                {
                    sum = elem.Select(item => item.Price).Sum();
                    sumInDates.Add(elem.Key, sum);
                }
            }
            return sumInDates;
        }
        // Метод получения списка дат и списков работников, работавших в каждую дату (Метод 4)
        public Dictionary<DateTime, List<string>> GetEmployeesInDates(DbContextOptions<ApplicationContext> options)
        {
            Dictionary<DateTime, List<string>> employeesInDates = new Dictionary<DateTime, List<string>>();
            List<int> employeesId;
            using (ApplicationContext context = new ApplicationContext(options))
            {
                var dateSums = context.Waybills.ToList().GroupBy(item => item.DateOfSupply).Select(item => item);
                foreach (var elem in dateSums)
                {
                    employeesId = elem.Select(item => item.EmployeeId).Distinct().ToList();
                    employeesInDates.Add(elem.Key, new List<string>());
                    foreach (var elemId in employeesId)
                    {
                        employeesInDates[elem.Key].Add(context.Employees.Where(item => item.Id == elemId).Select(item => item.FIO).First());
                    }
                }
            }
            return employeesInDates;
        }
        // Метод получения списка дат и списков работников, работавших в каждую дату с заданным поставщиком (Метод 5)
        public Dictionary<DateTime, List<string>> GetEmployeesInDatesWithProvider(DbContextOptions<ApplicationContext> options, string name)
        {
            Dictionary<DateTime, List<string>> employeesInDates = new Dictionary<DateTime, List<string>>();
            List<int> employeesId;
            using (ApplicationContext context = new ApplicationContext(options))
            {
                var dateSums = context.Waybills.Where(item => item.ProviderName == name).ToList().GroupBy(item => item.DateOfSupply).Select(item => item);
                foreach (var elem in dateSums)
                {
                    employeesId = elem.Select(item => item.EmployeeId).Distinct().ToList();
                    employeesInDates.Add(elem.Key, new List<string>());
                    foreach (var elemId in employeesId)
                    {
                        employeesInDates[elem.Key].Add(context.Employees.Where(item => item.Id == elemId).Select(item => item.FIO).First());
                    }
                }
            }
            return employeesInDates;
        }
        // Метод добавления записи в таблицу "Мебель" (Метод 6)
        public void AddFurniture(string name, string description, string material, decimal price, int count, DbContextOptions<ApplicationContext> options)
        {
            using ApplicationContext context = new ApplicationContext(options);
            var names = context.Furniture.Select(item => item.Name);
            var descr = context.Furniture.Select(item => item.Description);
            var id = context.Furniture.Select(item => item.Id).Max();
            // Проверка на наличие идентичной записи и корректности данных
            if (!names.Contains(name) && !descr.Contains(description) && price >= 0 && count > 0)
            {
                context.Furniture.Add(new Furniture(id + 1, name, description, material, price, count));
                context.SaveChanges();
            }
        }
        // Метод добавления записи в таблицу "Накладные" (Метод 7)
        public void AddWaybill(DbContextOptions<ApplicationContext> options, int provId, string provName, DateTime date, string material, decimal price, double weight, string furnitName, string emplFIO)
        {
            using ApplicationContext context = new ApplicationContext(options);
            // Проверка на наличие внешних ключей и корректность данных
            if (context.Furniture.Where(item => item.Name == furnitName).Count() != 0 && context.Employees.Where(item => item.FIO == emplFIO).Count()
                != 0 && price >= 0 && weight > 0)
            {
                var furnitId = context.Furniture.Where(item => item.Name == furnitName).Select(item => item.Id).Single();
                var emplId = context.Employees.Where(item => item.FIO == emplFIO).Select(item => item.Id).Single();
                var id = context.Waybills.Select(item => item.Id).Max();
                context.Waybills.Add(new Waybill(id + 1, provId, provName, date, material, price, weight, furnitId, emplId));
                context.SaveChanges();
            }
        }
        // Метод удаления записи из таблицы "Мебель" по ключу (Метод 8)
        public void DeleteFurniture(int id, DbContextOptions<ApplicationContext> options)
        {
            using ApplicationContext context = new ApplicationContext(options);
            var furniture = context.Furniture.Where(item => item.Id == id).Single();
            context.Furniture.Remove(furniture);
            context.SaveChanges();
        }
        // Метод удаления записи из таблицы "Накладные" по ключу (Метод 9)
        public void DeleteWaybill(int id, DbContextOptions<ApplicationContext> options)
        {
            using ApplicationContext context = new ApplicationContext(options);
            var waybill = context.Waybills.Where(item => item.Id == id).Single();
            context.Waybills.Remove(waybill);
            context.SaveChanges();
        }
        // Метод обновления записи в таблице "Заказы" (Метод 10)
        public void UpdateOrder(int id, string clientName, string furnitName, int furnitCount, decimal price, int percent, int isCompl, string emplFIO, DbContextOptions<ApplicationContext> options)
        {
            using ApplicationContext context = new ApplicationContext(options);
            var order = context.Orders.Where(item => item.Id == id).Single();
            order.ClientId = context.Clients.Where(item => item.Name == clientName).Select(item => item.Id).Single();
            order.FurnitureId = context.Furniture.Where(item => item.Name == furnitName).Select(item => item.Id).Single();
            order.FurnitureCount = furnitCount;
            order.Price = price;
            order.DiscountPercent = percent;
            order.IsCompleted = isCompl;
            order.EmployeeId = context.Employees.Where(item => item.FIO == emplFIO).Select(item => item.Id).Single();
            context.SaveChanges();
        }
    }
}
