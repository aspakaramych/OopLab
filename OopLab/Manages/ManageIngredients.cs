using OopLab.Services;

namespace OopLab.Manages;

public static class ManageIngredients
{
    public static void Manage(IngredientService service)
    {
        Console.WriteLine("\n1. Добавить ингредиент");
        Console.WriteLine("2. Редактировать ингредиент");
        Console.WriteLine("3. Удалить ингредиент");
        Console.WriteLine("4. Показать все ингредиенты");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Введите название ингредиента: ");
                string name = Console.ReadLine();
                Console.Write("Введите стоимость ингредиента: ");
                decimal cost = decimal.Parse(Console.ReadLine());
                
                var ingredient = new Ingredient 
                { 
                    Id = Guid.NewGuid(), 
                    Name = name, 
                    Cost = cost 
                };
                
                try 
                { 
                    service.Add(ingredient); 
                    Console.WriteLine($"Ингредиент '{name}' добавлен"); 
                } 
                catch (Exception ex) 
                { 
                    Console.WriteLine($"Ошибка: {ex.Message}"); 
                }
                break;

            case "2":
                Console.Write("Введите ID ингредиента для редактирования: ");
                var id = Guid.Parse(Console.ReadLine());
                var existing = service.GetById(id);
                if (existing != null)
                {
                    Console.Write($"Новое название [{existing.Name}]: ");
                    string newName = Console.ReadLine();
                    Console.Write($"Новая стоимость [{existing.Cost}]: ");
                    string costStr = Console.ReadLine();
                    
                    if (!string.IsNullOrEmpty(newName)) existing.Name = newName;
                    if (!string.IsNullOrEmpty(costStr)) existing.Cost = decimal.Parse(costStr);
                    
                    try 
                    { 
                        service.Update(existing); 
                        Console.WriteLine("Ингредиент обновлен"); 
                    } 
                    catch (Exception ex) 
                    { 
                        Console.WriteLine($"Ошибка: {ex.Message}"); 
                    }
                }
                else
                {
                    Console.WriteLine("Ингредиент не найден");
                }
                break;

            case "3":
                Console.Write("Введите ID ингредиента для удаления: ");
                id = Guid.Parse(Console.ReadLine());
                try 
                { 
                    service.Delete(id); 
                    Console.WriteLine("Ингредиент удален"); 
                } 
                catch (Exception ex) 
                { 
                    Console.WriteLine($"Ошибка: {ex.Message}"); 
                }
                break;

            case "4":
                Console.WriteLine("Список ингредиентов:");
                foreach (var item in service.GetAll())
                    Console.WriteLine($"{item.Id}: {item.Name} - {item.Cost}");
                break;
        }
    }

}