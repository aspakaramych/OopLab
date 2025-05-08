using OopLab.Enums;
using OopLab.Services;

namespace OopLab.Utils;

public static class Filter
{
    public static void FilterLists(IngredientService ingredientService, PizzaBaseService baseService, PizzaService pizzaService, PizzaCrustService crustService, OrderService orderService)
    {
        Console.WriteLine("\n1. Фильтрация пицц по ингредиенту");
        Console.WriteLine("2. Фильтрация заказов по дате");
        Console.WriteLine("3. Фильтрация заказов по стоимости");
        Console.WriteLine("4. Фильтрация пицц по типу основы");
        Console.WriteLine("5. Фильтрация бортиков по типу использования");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Введите ID ингредиента: ");
                var id = Guid.Parse(Console.ReadLine());
                var pizzas = pizzaService.GetAll().Where(p => p.Ingredients.Any(i => i.Id == id)).ToList();
                Console.WriteLine($"Пиццы с ингредиентом {id}:");
                foreach (var pizza in pizzas)
                    Console.WriteLine($"{pizza.Id}: {pizza.Name} - {pizza.Cost}");
                break;
                
            case "2":
                Console.Write("Введите дату (dd.MM.yyyy): ");
                var date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", null);
                var orders = orderService.GetAll().Where(o => o.OrderTime.Date == date.Date).ToList();
                Console.WriteLine($"Заказы за {date.ToShortDateString()}:");
                foreach (var order in orders)
                    Console.WriteLine($"{order.Id}: {order.TotalCost} - {order.OrderTime}");
                break;
                
            case "3":
                Console.Write("Введите минимальную стоимость: ");
                decimal minCost = decimal.Parse(Console.ReadLine());
                Console.Write("Введите максимальную стоимость: ");
                decimal maxCost = decimal.Parse(Console.ReadLine());
                var filteredOrders = orderService.GetAll().Where(o => o.TotalCost >= minCost && o.TotalCost <= maxCost).ToList();
                Console.WriteLine($"Заказы в диапазоне от {minCost} до {maxCost}:");
                foreach (var order in filteredOrders)
                    Console.WriteLine($"{order.Id}: {order.TotalCost} - {order.OrderTime}");
                break;
                
            case "4":
                Console.Write("Введите ID типа основы: ");
                var baseId = Guid.Parse(Console.ReadLine());
                var filteredPizzas = pizzaService.GetAll().Where(p => p.Base.Id == baseId).ToList();
                Console.WriteLine($"Пиццы с основой {baseId}:");
                foreach (var pizza in filteredPizzas)
                    Console.WriteLine($"{pizza.Id}: {pizza.Name} - {pizza.Cost}");
                break;
                
            case "5":
                Console.WriteLine("Выберите тип использования:");
                Console.WriteLine("1. Разрешенные");
                Console.WriteLine("2. Запрещенные");
                var usageType = int.Parse(Console.ReadLine()) == 1 ? UsageTypes.Allow : UsageTypes.Block;
                var crusts = crustService.GetAll().Where(c => c.UsageType == usageType).ToList();
                Console.WriteLine($"Бортики с типом использования {usageType}:");
                foreach (var crust in crusts)
                    Console.WriteLine($"{crust.Id}: {crust.Name}");
                break;
        }
    }
}