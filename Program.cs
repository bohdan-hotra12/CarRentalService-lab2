using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalService
{
    class Program
    {
        static List<Car> cars = new List<Car>();
        static List<Client> clients = new List<Client>();

        static void Main(string[] args)
        {
            InitializeData();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== Сервiс прокату авто ===\n");
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. Перегляд доступних авто");
                Console.WriteLine("2. Оренда авто");
                Console.WriteLine("3. Повернення авто");
                Console.WriteLine("4. Клiєнти");
                Console.WriteLine("5. Звiти");
                Console.WriteLine("6. Вихiд");
                Console.Write("\nВиберiть пункт меню: ");
                Console.ResetColor();

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAvailableCarsMenu();
                        break;
                    case "2":
                        RentCar();
                        break;
                    case "3":
                        ReturnCar();
                        break;
                    case "4":
                        ManageClients();
                        break;
                    case "5":
                        ShowReports();
                        break;
                    case "6":
                        exit = true;
                        Console.WriteLine("Дякуємо за користування сервiсом!");
                        break;
                    default:
                        Console.WriteLine("Невiрний вибiр. Спробуйте ще раз.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНатиснiть будь-яку клавiшу для повернення в меню...");
                    Console.ReadKey();
                }
            }
        }

        static void InitializeData()
        {
            // Легкові авто
            Console.ForegroundColor = ConsoleColor.Yellow;
            cars.Add(new Car("Toyota Corolla", "Легковий", 500, true));
            cars.Add(new Car("BMW 5 Series", "Легковий", 700, true));
            cars.Add(new Car("Tesla Model 3", "Легковий", 1000, true));
            cars.Add(new Car("Audi A4", "Легковий", 650, true));
            cars.Add(new Car("Honda Civic", "Легковий", 550, true));
            Console.WriteLine("=== Легкові авто ===");
            foreach (var car in cars.Where(c => c.Type == "Легковий"))
                Console.WriteLine(car);
            Console.ResetColor();
            Console.WriteLine();

            // Позашляховики
            Console.ForegroundColor = ConsoleColor.Blue;
            cars.Add(new Car("Toyota RAV4 \"Позашляховик\"", "Позашляховик", 800, true));
            cars.Add(new Car("BMW X5 \"Позашляховик\"", "Позашляховик", 1000, true));
            cars.Add(new Car("Mercedes GLE \"Позашляховик\"", "Позашляховик", 1200, true));
            cars.Add(new Car("Audi Q7 \"Позашляховик\"", "Позашляховик", 1100, true));
            cars.Add(new Car("Jeep Grand Cherokee \"Позашляховик\"", "Позашляховик", 950, true));
            Console.WriteLine("=== Позашляховики ===");
            foreach (var car in cars.Where(c => c.Type == "Позашляховик"))
                Console.WriteLine(car);
            Console.ResetColor();
            Console.WriteLine();

            // Мікроавтобуси
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            cars.Add(new Car("Ford Transit", "Мiкроавтобус", 800, true));
            cars.Add(new Car("Mercedes Sprinter", "Мiкроавтобус", 1200, true));
            cars.Add(new Car("Renault Trafic", "Мiкроавтобус", 900, true));
            cars.Add(new Car("Volkswagen Crafter", "Мiкроавтобус", 1000, true));
            cars.Add(new Car("Opel Vivaro", "Мiкроавтобус", 950, true));
            Console.WriteLine("=== Мікроавтобуси ===");
            foreach (var car in cars.Where(c => c.Type == "Мiкроавтобус"))
                Console.WriteLine(car);
            Console.ResetColor();
            Console.WriteLine();

            // Клієнти
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            clients.Add(new Client("Iван Iванов"));
            clients.Add(new Client("Марiя Петренко"));
            clients.Add(new Client("Андрiй Ковальчук"));
            clients.Add(new Client("Марина Соколова"));
            clients.Add(new Client("Дмитро Пасернев"));
            clients.Add(new Client("Сергiй Говрик"));
            Console.WriteLine("=== Клієнти ===");
            foreach (var client in clients)
                Console.WriteLine(client.Name);
            Console.ResetColor();
            Console.WriteLine();
        }



        static void ShowAvailableCarsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== Доступнi авто ===\n");
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1. Переглянути всi авто");
                Console.WriteLine("2. Фiльтрувати за типом");
                Console.WriteLine("3. Фiльтрувати за цiною/днем оренди");
                Console.WriteLine("4. Пошук за маркою");
                Console.WriteLine("5. Повернутися в головне меню");
                Console.Write("\nВиберiть опцiю: ");
                Console.ResetColor();

                string subChoice = Console.ReadLine();
                switch (subChoice)
                {
                    case "1":
                        ShowAllCars();
                        break;
                    case "2":
                        FilterCarsByType();
                        break;
                    case "3":
                        FilterCarsByPrice();
                        break;
                    case "4":
                        SearchCarByModel();
                        break;
                    case "5":
                        return; // вихід у головне меню
                    default:
                        Console.WriteLine("Невiрний вибiр. Спробуйте ще раз.");
                        break;
                }

                Console.WriteLine("\nНатиснiть будь-яку клавiшу для продовження...");
                Console.ReadKey();
            }
        }

        static void ShowAllCars()
        {
            var availableCars = cars.Where(c => c.IsAvailable).ToList();
            if (!availableCars.Any())
            {
                Console.WriteLine("Немає доступних авто.");
                return;
            }

            // Групуємо по типу авто
            foreach (var group in availableCars.GroupBy(c => c.Type))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n--- {group.Key} ---\n");
                Console.ResetColor();
                foreach (var car in group)
                    Console.WriteLine(car);
            }
        }

        static void FilterCarsByType()
        {
            Console.Write("Введіть тип авто (Легковий, Позашляховик, Мiкроавтобус): ");
            string type = Console.ReadLine();
            var filtered = cars.Where(c => c.IsAvailable && c.Type.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filtered.Any())
            {
                Console.WriteLine("Авто такого типу недоступнi.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n--- {type} ---\n");
            Console.ResetColor();
            foreach (var car in filtered)
                Console.WriteLine(car);
        }

        static void FilterCarsByPrice()
        {
            Console.Write("Введiть максимальну цiну за день: ");
            if (int.TryParse(Console.ReadLine(), out int maxPrice))
            {
                var filtered = cars.Where(c => c.IsAvailable && c.PricePerDay <= maxPrice).ToList();
                if (!filtered.Any())
                {
                    Console.WriteLine("Немає авто за цiєю цiною.");
                    return;
                }

                foreach (var group in filtered.GroupBy(c => c.Type))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n--- {group.Key} ---\n");
                    Console.ResetColor();
                    foreach (var car in group)
                        Console.WriteLine(car);
                }
            }
            else
            {
                Console.WriteLine("Невiрне число.");
            }
        }

        static void SearchCarByModel()
        {
            Console.Write("Введiть марку авто: ");
            string model = Console.ReadLine();
            var found = cars.Where(c => c.IsAvailable && c.Model.IndexOf(model, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            if (!found.Any())
            {
                Console.WriteLine("Авто з такою маркою не знайдено.");
                return;
            }

            foreach (var group in found.GroupBy(c => c.Type))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n--- {group.Key} ---\n");
                Console.ResetColor();
                foreach (var car in group)
                    Console.WriteLine(car);
            }
        }

        static void RentCar()
        {
            Console.Clear();
            var availableCars = cars.Where(c => c.IsAvailable).ToList();
            if (!availableCars.Any())
            {
                Console.WriteLine("Немає доступних авто для оренди.");
                return;
            }

            Console.WriteLine("Доступнi авто:");
            for (int i = 0; i < availableCars.Count; i++)
                Console.WriteLine($"{i + 1}. {availableCars[i]}");

            Console.Write("Виберiть номер авто для оренди: ");
            if (!int.TryParse(Console.ReadLine(), out int carIndex) || carIndex < 1 || carIndex > availableCars.Count)
            {
                Console.WriteLine("Невiрний вибiр авто.");
                return;
            }

            Car selectedCar = availableCars[carIndex - 1];

            Console.WriteLine("Список клiєнтiв:");
            for (int i = 0; i < clients.Count; i++)
                Console.WriteLine($"{i + 1}. {clients[i].Name}");
            Console.Write("Виберiть номер клiєнта або введiть 0 для нового: ");

            if (!int.TryParse(Console.ReadLine(), out int clientChoice))
            {
                Console.WriteLine("Невiрний вибiр клiєнта.");
                return;
            }

            Client client;
            if (clientChoice == 0)
            {
                Console.Write("Введiть iм'я нового клiєнта: ");
                string name = Console.ReadLine();
                client = new Client(name);
                clients.Add(client);
            }
            else if (clientChoice >= 1 && clientChoice <= clients.Count)
            {
                client = clients[clientChoice - 1];
            }
            else
            {
                Console.WriteLine("Невiрний вибiр клiєнта.");
                return;
            }

            selectedCar.IsAvailable = false;
            selectedCar.RentedBy = client;
            // Введення кількості днів оренди
            Console.Write("Введiть кiлькiсть днiв оренди: ");
            if (!int.TryParse(Console.ReadLine(), out int days) || days <= 0)
            {
                Console.WriteLine("Невірна кількість днів.");
                return;
            }

            // Розрахунок ціни
            double totalPrice = selectedCar.PricePerDay * days;

            // Простий приклад знижки: якщо більше 3 днів – 10% знижка
            double discount = days > 3 ? 0.1 : 0;
            double finalPrice = totalPrice - (totalPrice * discount);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Чек оренди авто ===\n");
            Console.ResetColor();
            Console.WriteLine($"Клiєнт: {client.Name}");
            Console.WriteLine($"Авто: {selectedCar.Model} ({selectedCar.Type})");
            Console.WriteLine($"Цiна за день: {selectedCar.PricePerDay} грн");
            Console.WriteLine($"Кiлькiсть днiв: {days}");
            Console.WriteLine($"Сума без знижки: {totalPrice} грн");
            Console.WriteLine($"Знижка: {discount * 100}%");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Остаточна цiна до оплати: {finalPrice} грн");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Авто {selectedCar.Model} успiшно орендовано клiєнтом {client.Name}.");
            Console.ResetColor();




        }

        static void ReturnCar()
        {
            Console.Clear();
            var rentedCars = cars.Where(c => !c.IsAvailable).ToList();
            if (!rentedCars.Any())
            {
                Console.WriteLine("Немає авто, що знаходяться в орендi.");
                return;
            }

            Console.WriteLine("Орендованi авто:");
            for (int i = 0; i < rentedCars.Count; i++)
                Console.WriteLine($"{i + 1}. {rentedCars[i]} (Орендар: {rentedCars[i].RentedBy.Name})");

            Console.Write("Виберiть номер авто для повернення: ");
            if (!int.TryParse(Console.ReadLine(), out int carIndex) || carIndex < 1 || carIndex > rentedCars.Count)
            {
                Console.WriteLine("Невiрний вибiр.");
                return;
            }

            Car car = rentedCars[carIndex - 1];
            car.IsAvailable = true;
            car.RentedBy = null;
            Console.WriteLine($"Авто {car.Model} повернуто.");
        }

        static void ManageClients()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== Клiєнти ===\n");
                Console.ResetColor();

                for (int i = 0; i < clients.Count; i++)
                    Console.WriteLine($"{i + 1}. {clients[i].Name}");

                Console.WriteLine("\n7. Додати клiєнта");
                Console.WriteLine("8. Видалити клiєнта");
                Console.WriteLine("9. Повернутися в головне меню");
                Console.Write("\nВиберiть опцiю: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "7":
                        Console.Write("Введiть iм'я нового клiєнта: ");
                        string name = Console.ReadLine();
                        clients.Add(new Client(name));
                        Console.WriteLine("Клiєнта додано.");
                        break;
                    case "8":
                        Console.Write("Введiть номер клiєнта для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= clients.Count)
                        {
                            clients.RemoveAt(index - 1);
                            Console.WriteLine("Клiєнта видалено.");
                        }
                        else
                        {
                            Console.WriteLine("Невiрний номер клiєнта.");
                        }
                        break;
                    case "9":
                        return; // повернення у головне меню
                    default:
                        Console.WriteLine("Невiрний вибiр.");
                        break;
                }

                Console.WriteLine("\nНатиснiть будь-яку клавiшу для продовження...");
                Console.ReadKey();
            }
        }

        static void ShowReports()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Звiти ===\n");
            Console.ResetColor();

            var rentedCars = cars.Where(c => !c.IsAvailable).ToList();
            Console.WriteLine("Орендованi авто:");
            if (!rentedCars.Any())
                Console.WriteLine("Немає орендованих авто.");
            else
            {
                foreach (var group in rentedCars.GroupBy(c => c.Type))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n--- {group.Key} ---\n");
                    Console.ResetColor();
                    foreach (var car in group)
                        Console.WriteLine($"{car.Model} - Орендар: {car.RentedBy.Name}");
                }
            }

            var availableCars = cars.Where(c => c.IsAvailable).ToList();
            Console.WriteLine("\nДоступнi авто:");
            if (!availableCars.Any())
                Console.WriteLine("Немає доступних авто.");
            else
            {
                foreach (var group in availableCars.GroupBy(c => c.Type))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n--- {group.Key} ---\n");
                    Console.ResetColor();
                    foreach (var car in group)
                        Console.WriteLine(car);
                }
            }
        }
    }

    class Car
    {
        public string Model { get; set; }
        public string Type { get; set; }
        public int PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public Client RentedBy { get; set; }

        public Car(string model, string type, int pricePerDay, bool isAvailable)
        {
            Model = model;
            Type = type;
            PricePerDay = pricePerDay;
            IsAvailable = isAvailable;
            RentedBy = null;
        }

        public override string ToString()
        {
            return $"{Model} ({Type}) - {PricePerDay} грн/день - {(IsAvailable ? "Доступне" : "Зайняте")}";
        }
    }

    class Client
    {
        public string Name { get; set; }

        public Client(string name)
        {
            Name = name;
        }
    }
}