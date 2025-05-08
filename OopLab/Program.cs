using OopLab.Manages;
using OopLab.Repo;
using OopLab.Services;
using OopLab.Utils;

namespace OopLab ;


class Program
{
    static void Main(string[] args)
    {
        var ingredientRepo = new IngredientRepo();
        var baseRepo = new PizzaBaseRepo();
        var crustRepo = new PizzaCrustRepo();
        var pizzaRepo = new PizzaRepo();
        var orderRepo = new OrderRepo();

        var ingredientService = new IngredientService(ingredientRepo);
        var baseService = new PizzaBaseService(baseRepo);
        var crustService = new PizzaCrustService(crustRepo, new PizzaService(pizzaRepo, baseService, ingredientService, null));
        crustService = new PizzaCrustService(crustRepo, new PizzaService(pizzaRepo, baseService, ingredientService, crustService));
        var pizzaService = new PizzaService(pizzaRepo, baseService, ingredientService, crustService);
        var orderService = new OrderService(orderRepo, pizzaService);

        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Управление ингредиентами");
            Console.WriteLine("2. Управление основами");
            Console.WriteLine("3. Управление пиццами");
            Console.WriteLine("4. Управление бортиками");
            Console.WriteLine("5. Управление заказами");
            Console.WriteLine("6. Вывести списки с фильтрацией");
            Console.WriteLine("0. Выход");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ManageIngredients.Manage(ingredientService);
                    break;
                case "2":
                    ManageBases.Manage(baseService);
                    break;
                case "3":
                    ManagePizzas.Manage(pizzaService, ingredientService, baseService, crustService);
                    break;
                case "4":
                    ManageCrusts.Manage(crustService, pizzaService, ingredientService);
                    break;
                case "5":
                    ManageOrder.Manage(orderService, pizzaService);
                    break;
                case "6":
                    Filter.FilterLists(ingredientService, baseService, pizzaService, crustService, orderService);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }

    }
}