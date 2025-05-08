using OopLab.Entity;
using OopLab.Enums;
using OopLab.Services;

namespace OopLab.Manages;

public class ManageCrusts
{
    public static void Manage(PizzaCrustService service, PizzaService pizzaService, IngredientService ingredientService)
    {
        Console.WriteLine("\n1. Добавить бортик");
        Console.WriteLine("2. Редактировать бортик");
        Console.WriteLine("3. Удалить бортик");
        Console.WriteLine("4. Показать все бортики");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Введите название бортика: ");
                string name = Console.ReadLine();
                List<Ingredient> ingredients = new List<Ingredient>();
                while (true)
                {
                    Console.WriteLine("Доступные ингредиенты:");
                    foreach (var i in ingredientService.GetAll())
                        Console.WriteLine($"{i.Id}: {i.Name} - {i.Cost}");
                    Console.Write("Добавить ингредиент (ID или 'q' для выхода): ");
                    var input = Console.ReadLine();
                    if (input.ToLower() == "q") break;

                    var ingredient = ingredientService.GetById(Guid.Parse(input));
                    if (ingredient != null)
                    {
                        ingredients.Add(ingredient);
                        Console.WriteLine($"Добавлен ингредиент: {ingredient.Name}");
                    }
                }

                Console.WriteLine("Тип использования:");
                Console.WriteLine("1. Разрешить только для указанных пицц");
                Console.WriteLine("2. Запретить для указанных пицц");
                var usageType = int.Parse(Console.ReadLine()) == 1 ? UsageTypes.Allow : UsageTypes.Block;

                List<Guid> pizzaIds = new List<Guid>();
                while (true)
                {
                    Console.WriteLine("Доступные пиццы:");
                    foreach (var p in pizzaService.GetAll())
                        Console.WriteLine($"{p.Id}: {p.Name}");
                    Console.Write("Добавить пиццу (ID или 'q' для выхода): ");
                    var input = Console.ReadLine();
                    if (input.ToLower() == "q") break;

                    pizzaIds.Add(Guid.Parse(input));
                }

                var crust = new PizzaCrust
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Ingredients = ingredients,
                    UsageType = usageType,
                    PizzaIds = pizzaIds
                };

                try
                {
                    service.Add(crust);
                    Console.WriteLine($"Бортик '{name}' добавлен");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                break;

            case "2":
                Console.Write("Введите ID бортика для редактирования: ");
                var id = Guid.Parse(Console.ReadLine());
                var existing = service.GetById(id);
                if (existing != null)
                {
                    Console.Write($"Новое название [{existing.Name}]: ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName)) existing.Name = newName;

                    try
                    {
                        service.Update(existing);
                        Console.WriteLine("Бортик обновлен");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Бортик не найден");
                }

                break;

            case "3":
                Console.Write("Введите ID бортика для удаления: ");
                id = Guid.Parse(Console.ReadLine());
                try
                {
                    service.Delete(id);
                    Console.WriteLine("Бортик удален");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                break;

            case "4":
                Console.WriteLine("Список бортиков:");
                foreach (var item in service.GetAll())
                    Console.WriteLine($"{item.Id}: {item.Name} - {item.UsageType}");
                break;
        }
    }
}