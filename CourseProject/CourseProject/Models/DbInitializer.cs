using System;
using System.Linq;

namespace CourseProject.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationContext db)
        {
            db.Database.EnsureCreated();

            Random randObj = new Random();

            char[] letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".ToCharArray();

            if (!db.Clients.Any())
            {
                int clientId;
                string name = "";
                string repFIO = "";
                int numb;
                string address = "";
                for (int id = 1; id <= 40; id++)
                {
                    clientId = db.Clients.Count() + 1;
                    int rand = randObj.Next(3, 25);
                    for (int i = 1; i <= rand; i++)
                    {
                        name += letters[randObj.Next(33)];
                    }
                    rand = randObj.Next(17, 100);
                    for (int i = 1; i <= rand; i++)
                    {
                        repFIO += letters[randObj.Next(33)];
                    }
                    numb = randObj.Next(1000000, 9999999);
                    rand = randObj.Next(5, 40);
                    for (int i = 1; i <= rand; i++)
                    {
                        address += letters[randObj.Next(33)];
                    }
                    db.Clients.Add(new Client { Id = clientId, Name = name, RepresentativeFIO = repFIO, Number = numb, Address = address});
                }
                db.SaveChanges();
            }

            if (!db.Employees.Any())
            {
                int employeeId;
                string fio = "";
                string position = "";
                string education = "";
                for (int id = 1; id <= 40; id++)
                {
                    employeeId = db.Employees.Count() + 1;
                    int rand = randObj.Next(15, 100);
                    for (int i = 1; i <= rand; i++)
                    {
                        fio += letters[randObj.Next(33)];
                    }
                    rand = randObj.Next(5, 50);
                    for (int i = 1; i <= rand; i++)
                    {
                        position += letters[randObj.Next(33)];
                    }
                    rand = randObj.Next(20, 200);
                    for (int i = 1; i <= rand; i++)
                    {
                        education += letters[randObj.Next(33)];
                    }
                    db.Employees.Add(new Employee { Id = employeeId, FIO = fio, Position = position, Education = education });
                }
                db.SaveChanges();
            }

            if (!db.Furniture.Any())
            {
                int furnitId;
                string name = "";
                string descr = "";
                int count;
                decimal price;
                string material = "";
                for (int id = 1; id <= 40; id++)
                {
                    furnitId = db.Furniture.Count() + 1;
                    int rand = randObj.Next(3, 51);
                    for (int i = 1; i <= rand; i++)
                    {
                        name += letters[randObj.Next(33)];
                    }
                    rand = randObj.Next(17, 200);
                    for (int i = 1; i <= rand; i++)
                    {
                        descr += letters[randObj.Next(33)];
                    }
                    price = (decimal)randObj.NextDouble() * 10;
                    count = randObj.Next(1, 21);
                    rand = randObj.Next(3, 61);
                    for (int i = 1; i <= rand; i++)
                    {
                        material += letters[randObj.Next(33)];
                    }
                    db.Furniture.Add(new Furniture { Id = furnitId, Name = name, Description = descr, Material = material, Price = price, Count = count });
                }
                db.SaveChanges();
            }

            if (!db.Orders.Any())
            {
                int orderId;
                int clientId;
                int furnitId;
                int discount;
                int isComplete;
                int count;
                decimal price;
                int emplId;
                for (int id = 1; id <= 200; id++)
                {
                    orderId = db.Orders.Count() + 1;
                    clientId = randObj.Next(1, 41);
                    furnitId = randObj.Next(1, 41);
                    discount = randObj.Next(0, 50);
                    isComplete = randObj.Next(0, 2);
                    count = randObj.Next(1, 21);
                    price = (decimal)randObj.NextDouble() * 100;
                    emplId = randObj.Next(1, 41);
                    db.Orders.Add(new Order { Id = orderId, ClientId = clientId, FurnitureId = furnitId, DiscountPercent = discount, IsCompleted = isComplete, FurnitureCount = count, Price = price, EmployeeId = emplId });
                }
                db.SaveChanges();
            }

            if (!db.Waybills.Any())
            {
                int waybillId;
                int providerId;
                string providerName = "";
                DateTime date;
                string material = "";
                float weight;
                decimal price;
                int furnitId;
                int emplId;
                for (int id = 1; id <= 200; id++)
                {
                    waybillId = db.Waybills.Count() + 1;
                    providerId = randObj.Next(1, 101);
                    int rand = randObj.Next(7, 100);
                    for (int i = 1; i <= rand; i++)
                    {
                        providerName += letters[randObj.Next(33)];
                    }
                    furnitId = randObj.Next(1, 41);
                    date = DateTime.Now;
                    rand = randObj.Next(3, 61);
                    furnitId = randObj.Next(1, 41);
                    material = db.Furniture.Where(item => item.Id == furnitId).Select(item => item.Material).FirstOrDefault();
                    weight = (float)randObj.NextDouble() * 10;
                    price = (decimal)randObj.NextDouble() * 100;
                    emplId = randObj.Next(1, 41);
                    db.Waybills.Add(new Waybill { Id = waybillId, ProviderId = providerId, ProviderName = providerName, DateOfSupply = date, Material = material, Price = price, Weight = weight, FurnitureId = furnitId, EmployeeId = emplId});
                }
                db.SaveChanges();
            }
        }

    }
}