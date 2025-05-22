using OopLab.Entity;
using OopLab.Services;

namespace OopLab.Manages;

public static class ManageBases
{
    public static void Manage(PizzaBaseService service)
    {
        Console.WriteLine("\n1. Добавить основу");
        Console.WriteLine("2. Редактировать основу");
        Console.WriteLine("3. Удалить основу");
        Console.WriteLine("4. Показать все основы");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Введите название основы: ");
                string name = Console.ReadLine();
                Console.Write("Введите стоимость основы: ");
                decimal cost = decimal.Parse(Console.ReadLine());
                Console.Write("Это классическая основа? (y/n): ");
                bool isClassic = Console.ReadLine().ToLower() == "y";
                try 
                { 
                    service.Add(name, cost, isClassic); 
                    Console.WriteLine($"Основа '{name}' добавлена"); 
                } 
                catch (Exception ex) 
                { 
                    Console.WriteLine($"Ошибка: {ex.Message}"); 
                }
                break;

            case "2":
                Console.Write("Введите ID основы для редактирования: ");
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
                        Console.WriteLine("Основа обновлена"); 
                    } 
                    catch (Exception ex) 
                    { 
                        Console.WriteLine($"Ошибка: {ex.Message}"); 
                    }
                }
                else
                {
                    Console.WriteLine("Основа не найдена");
                }
                break;

            case "3":
                Console.Write("Введите ID основы для удаления: ");
                id = Guid.Parse(Console.ReadLine());
                try 
                { 
                    service.Delete(id); 
                    Console.WriteLine("Основа удалена"); 
                } 
                catch (Exception ex) 
                { 
                    Console.WriteLine($"Ошибка: {ex.Message}"); 
                }
                break;

            case "4":
                Console.WriteLine("Список основ:");
                foreach (var item in service.GetAll())
                    Console.WriteLine($"{item.Id}: {item.Name} - {item.Cost} {(item.IsClassic ? "(классическая)" : "")}");
                break;
        }
    }

}