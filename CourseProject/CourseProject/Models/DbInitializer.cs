using System;
using System.Linq;

namespace CourseProject.Models
{
    // Класс, содержащий метод инициализации базы данных
    public static class DbInitializer
    {
        // Метод инициализации базы данных путем заполнения таблиц тестовыми наборами данных
        public static void Initialize(ApplicationContext db)
        {
            // Метод, который проверяет существование базы данных
            db.Database.EnsureCreated();

            // Объекты для генерации случайных чисел и записей
            Random randObj = new Random();

            char[] letters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".ToCharArray();

            // Проверка на наличие записей в таблице Клиенты
            if (!db.Clients.Any())
            {
                int clientId;
                string name = "";
                string repFIO = "";
                int numb;
                string address = "";

                // Создание 40 записей в таблице
                for (int id = 1; id <= 40; id++)
                {
                    // Получение Id
                    clientId = db.Clients.Count() + 1;

                    // Создание названия клиента-организации
                    int rand = randObj.Next(3, 25);
                    for (int i = 1; i <= rand; i++)
                    {
                        name += letters[randObj.Next(33)];
                    }

                    // Создание ФИО представителя
                    rand = randObj.Next(17, 100);
                    for (int i = 1; i <= rand; i++)
                    {
                        repFIO += letters[randObj.Next(33)];
                    }

                    // Создание номера клиента
                    numb = randObj.Next(1000000, 9999999);

                    // Создание адреса
                    rand = randObj.Next(5, 40);
                    for (int i = 1; i <= rand; i++)
                    {
                        address += letters[randObj.Next(33)];
                    }

                    // Добавление записи в таблицу
                    db.Clients.Add(new Client { Id = clientId, Name = name, RepresentativeFIO = repFIO, Number = numb, Address = address});
                }
                db.SaveChanges();
            }

            // Проверка на наличие записей в таблице Работники
            if (!db.Employees.Any())
            {
                int employeeId;
                string fio = "";
                string position = "";
                string education = "";

                // Создание 40 записей
                for (int id = 1; id <= 40; id++)
                {
                    // Получение Id
                    employeeId = db.Employees.Count() + 1;

                    // Создание ФИО работника
                    int rand = randObj.Next(15, 100);
                    for (int i = 1; i <= rand; i++)
                    {
                        fio += letters[randObj.Next(33)];
                    }

                    // Создание должности работника
                    rand = randObj.Next(5, 50);
                    for (int i = 1; i <= rand; i++)
                    {
                        position += letters[randObj.Next(33)];
                    }

                    // Создание образования
                    rand = randObj.Next(20, 200);
                    for (int i = 1; i <= rand; i++)
                    {
                        education += letters[randObj.Next(33)];
                    }

                    // Добавление записи в таблицу
                    db.Employees.Add(new Employee { Id = employeeId, FIO = fio, Position = position, Education = education });
                }
                db.SaveChanges();
            }

            // Проверка на наличие записей в таблице Мебель
            if (!db.Furniture.Any())
            {
                int furnitId;
                string name = "";
                string descr = "";
                int count;
                decimal price;
                string material = "";

                // Создание 40 записей
                for (int id = 1; id <= 40; id++)
                {
                    // Получение Id
                    furnitId = db.Furniture.Count() + 1;

                    // Создание названия мебели
                    int rand = randObj.Next(3, 51);
                    for (int i = 1; i <= rand; i++)
                    {
                        name += letters[randObj.Next(33)];
                    }

                    // Создание описания мебели
                    rand = randObj.Next(17, 200);
                    for (int i = 1; i <= rand; i++)
                    {
                        descr += letters[randObj.Next(33)];
                    }

                    // Создание цены на мебель
                    price = (decimal)randObj.NextDouble() * 10;

                    // Создание кол-ва мебели
                    count = randObj.Next(1, 21);

                    // Создание материала мебели
                    rand = randObj.Next(3, 61);
                    for (int i = 1; i <= rand; i++)
                    {
                        material += letters[randObj.Next(33)];
                    }

                    // Добавление записи в таблицу
                    db.Furniture.Add(new Furniture { Id = furnitId, Name = name, Description = descr, Material = material, Price = price, Count = count });
                }
                db.SaveChanges();
            }

            // Проверка на наличие записей в таблице Заказы
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

                // Создание 200 записей
                for (int id = 1; id <= 200; id++)
                {
                    // Получение Id
                    orderId = db.Orders.Count() + 1;

                    // Получение Id клиента
                    clientId = randObj.Next(1, 41);

                    // Получение Id мебели
                    furnitId = randObj.Next(1, 41);

                    // Создание скидки
                    discount = randObj.Next(0, 50);

                    // Создание состояния выполнения
                    isComplete = randObj.Next(0, 2);

                    // Создание кол-ва мебели
                    count = randObj.Next(1, 21);

                    // Создание стоимости заказа
                    price = (decimal)randObj.NextDouble() * 100;

                    // Получение Id работника
                    emplId = randObj.Next(1, 41);

                    // Добавление записи в таблицу
                    db.Orders.Add(new Order { Id = orderId, ClientId = clientId, FurnitureId = furnitId, DiscountPercent = discount, IsCompleted = isComplete, FurnitureCount = count, Price = price, EmployeeId = emplId });
                }
                db.SaveChanges();
            }

            // Проверка на наличие записей в таблице Накладные
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

                // Создание 200 записей
                for (int id = 1; id <= 200; id++)
                {
                    // Получение Id накладной
                    waybillId = db.Waybills.Count() + 1;

                    // Создание номера поставщика
                    providerId = randObj.Next(1, 101);

                    // Создание названия поставщика
                    int rand = randObj.Next(7, 100);
                    for (int i = 1; i <= rand; i++)
                    {
                        providerName += letters[randObj.Next(33)];
                    }

                    // Получение Id мебели
                    furnitId = randObj.Next(1, 41);

                    // Получение времени
                    date = DateTime.Now;

                    // Получение материала мебели
                    material = db.Furniture.Where(item => item.Id == furnitId).Select(item => item.Material).FirstOrDefault();

                    // Создание веса поставки
                    weight = (float)randObj.NextDouble() * 10;

                    // Создание стоимости поставки
                    price = (decimal)randObj.NextDouble() * 100;

                    // Получение Id работника
                    emplId = randObj.Next(1, 41);

                    // Добавление записи в таблицу
                    db.Waybills.Add(new Waybill { Id = waybillId, ProviderId = providerId, ProviderName = providerName, DateOfSupply = date, Material = material, Price = price, Weight = weight, FurnitureId = furnitId, EmployeeId = emplId});
                }
                db.SaveChanges();
            }
        }
    }
}