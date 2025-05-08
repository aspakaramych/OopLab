using OopLab.Entity;
using OopLab.Enums;
using OopLab.Services;

namespace OopLab.Manages;

public class ManageOrder
{
    public static void Manage(OrderService service, PizzaService pizzaService)
    {
        Console.WriteLine("\n1. Создать заказ");
        Console.WriteLine("2. Показать все заказы");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderTime = DateTime.Now,
                    Pizzas = new List<OrderPizza>()
                };

                while (true)
                {
                    Console.WriteLine("Тип пиццы в заказе:");
                    Console.WriteLine("1. Стандартная пицца");
                    Console.WriteLine("2. Собранная вручную");
                    Console.WriteLine("3. Комбинированная");
                    Console.WriteLine("4. Завершить добавление пицц");
                    var pizzaType = Console.ReadLine();

                    if (pizzaType == "4") break;

                    var orderPizza = new OrderPizza();

                    switch (pizzaType)
                    {
                        case "1":
                            Console.WriteLine("Доступные пиццы:");
                            foreach (var p in pizzaService.GetAll())
                                Console.WriteLine($"{p.Id}: {p.Name} - {p.Cost}");
                            Console.Write("Выберите ID пиццы: ");
                            var pizzaId = Guid.Parse(Console.ReadLine());
                            orderPizza.Pizza = pizzaService.GetById(pizzaId);
                            orderPizza.IsCustom = false;
                            break;

                        case "2":
                            var customPizza = new Pizza
                            {
                                Id = Guid.NewGuid(),
                                Name = "Пользовательская",
                                Ingredients = new List<Ingredient>(),
                                Base = pizzaService._baseService.GetAll().FirstOrDefault(b => b.IsClassic)
                            };


                            while (true)
                            {
                                Console.WriteLine("Доступные ингредиенты:");
                                foreach (var i in pizzaService._ingredientService.GetAll())
                                    Console.WriteLine($"{i.Id}: {i.Name} - {i.Cost}");
                                Console.Write("Добавить ингредиент (ID или 'q' для выхода): ");
                                var input = Console.ReadLine();
                                if (input.ToLower() == "q") break;

                                var ingredient = pizzaService._ingredientService.GetById(Guid.Parse(input));
                                if (ingredient != null)
                                {
                                    customPizza.Ingredients.Add(ingredient);
                                    Console.WriteLine($"Добавлен ингредиент: {ingredient.Name}");
                                }
                            }

                            orderPizza.Pizza = customPizza;
                            orderPizza.IsCustom = true;
                            break;

                        case "3":
                            Console.WriteLine("Доступные пиццы:");
                            foreach (var p in pizzaService.GetAll())
                                Console.WriteLine($"{p.Id}: {p.Name} - {p.Cost}");

                            Console.Write("Выберите первую половину: ");
                            var pizzaA = pizzaService.GetById(Guid.Parse(Console.ReadLine()));
                            Console.Write("Выберите вторую половину: ");
                            var pizzaB = pizzaService.GetById(Guid.Parse(Console.ReadLine()));

                            orderPizza.HalfPizzaA = pizzaA;
                            orderPizza.HalfPizzaB = pizzaB;
                            break;
                    }

                    Console.WriteLine("Размер:");
                    Console.WriteLine("1. Маленькая");
                    Console.WriteLine("2. Средняя");
                    Console.WriteLine("3. Большая");
                    var size = int.Parse(Console.ReadLine());
                    orderPizza.Size = size == 1 ? Size.Small : size == 2 ? Size.Medium : Size.Large;


                    PizzaCrust pickedCrust = null;
                    Console.WriteLine("Доступные бортики:");
                    foreach (var c in pizzaService._crustService.GetAll())
                        Console.WriteLine($"{c.Id}: {c.Name}");
                    Console.Write("Добавить бортик (ID или 'q' для выхода): ");
                    var newinput = Console.ReadLine();
                    if (newinput.ToLower() == "q") break;

                    var crust = pizzaService._crustService.GetById(Guid.Parse(newinput));
                    pickedCrust = crust;
                    if (crust != null)
                    {
                        orderPizza.Crust = pickedCrust;
                        Console.WriteLine($"Добавлен бортик: {crust.Name}");
                    }


                    order.Pizzas.Add(orderPizza);
                }

                Console.Write("Введите комментарий к заказу: ");
                order.Comment = Console.ReadLine();
                
                Console.Write("Сделать заказ отложенным? (y/n): ");
                if (Console.ReadLine().ToLower() == "y")
                {
                    Console.Write("Введите дату и время (dd.MM.yyyy HH:mm): ");
                    order.DeferredDateTime = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy HH:mm", null);
                    order.IsDeferred = true;
                }

                try
                {
                    service.Add(order);
                    Console.WriteLine($"Заказ {order.Id} создан");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                break;

            case "2":
                Console.WriteLine("Список заказов:");
                foreach (var item in service.GetAll())
                    Console.WriteLine(
                        $"{item.Id}: {item.TotalCost} - {item.OrderTime} {(item.IsDeferred ? $"[Отложен: {item.DeferredDateTime}]" : "")}");
                break;
        }
    }
}