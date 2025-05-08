using OopLab.Entity;
using OopLab.Services;

namespace OopLab.Manages;

public static class ManagePizzas
{
    public static void Manage(PizzaService service, IngredientService ingredientService, PizzaBaseService baseService,
        PizzaCrustService crustService)
    {
        Console.WriteLine("\n1. Добавить пиццу");
        Console.WriteLine("2. Редактировать пиццу");
        Console.WriteLine("3. Удалить пиццу");
        Console.WriteLine("4. Показать все пиццы");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Введите название пиццы: ");
                string name = Console.ReadLine();
                Console.WriteLine("Доступные основы:");
                foreach (var b in baseService.GetAll())
                    Console.WriteLine($"{b.Id}: {b.Name} - {b.Cost}");
                Console.Write("Выберите ID основы: ");
                var baseId = Guid.Parse(Console.ReadLine());
                var selectedBase = baseService.GetById(baseId);

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

                PizzaCrust pickedCrust = null;
                Console.WriteLine("Доступные бортики:");
                foreach (var c in crustService.GetAll())
                    Console.WriteLine($"{c.Id}: {c.Name}");
                Console.Write("Добавить бортик (ID или 'q' для выхода): ");
                var newinput = Console.ReadLine();
                if (newinput.ToLower() == "q") break;

                var crust = crustService.GetById(Guid.Parse(newinput));
                if (crust != null)
                {
                    pickedCrust = crust;
                    Console.WriteLine($"Добавлен бортик: {crust.Name}");
                }


                var pizza = new Pizza
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Ingredients = ingredients,
                    Base = selectedBase,
                    Crust = pickedCrust
                };

                try
                {
                    service.Add(pizza);
                    Console.WriteLine($"Пицца '{name}' добавлена");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                break;

            case "2":
                Console.Write("Введите ID пиццы для редактирования: ");
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
                        Console.WriteLine("Пицца обновлена");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Пицца не найдена");
                }

                break;

            case "3":
                Console.Write("Введите ID пиццы для удаления: ");
                id = Guid.Parse(Console.ReadLine());
                try
                {
                    service.Delete(id);
                    Console.WriteLine("Пицца удалена");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                break;

            case "4":
                Console.WriteLine("Список пицц:");
                foreach (var item in service.GetAll())
                    Console.WriteLine($"{item.Id}: {item.Name} - {item.Cost}");
                break;
        }
    }
}