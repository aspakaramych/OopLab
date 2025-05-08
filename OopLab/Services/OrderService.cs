using OopLab.Entity;
using OopLab.Repo;

namespace OopLab.Services;

public class OrderService : BaseService<Order>
{
    private readonly OrderRepo _repo;
    private readonly PizzaService _pizzaService;

    public OrderService(OrderRepo repo, PizzaService pizzaService) : base(repo)
    {
        _repo = repo;
        _pizzaService = pizzaService;
    }

    public override void Add(Order item)
    {
        foreach (var orderPizza in item.Pizzas)
        {
            ValidateOrderPizza(orderPizza);
        }

        base.Add(item);
    }

    private void ValidateOrderPizza(OrderPizza orderPizza)
    {
        if (orderPizza.IsCustom)
        {
            foreach (var ingredient in orderPizza.Pizza.Ingredients)
            {
                var existing = _pizzaService._ingredientService.GetById(ingredient.Id);
                if (existing == null)
                    throw new ArgumentException("Нет такого ингредиента");
            }

            var baseExisting = _pizzaService._baseService.GetById(orderPizza.Pizza.Base.Id);
            if (baseExisting == null)
                throw new ArgumentException("Такой основы нет");
        }
        else
        {
            var existingPizza = _pizzaService.GetById(orderPizza.Pizza.Id);
            if (existingPizza == null)
                throw new ArgumentException("Такой пиццы нет");
        }

        if (orderPizza.HalfPizzaA != null && orderPizza.HalfPizzaB != null)
        {
            var existingA = _pizzaService.GetById(orderPizza.HalfPizzaA.Id);
            var existingB = _pizzaService.GetById(orderPizza.HalfPizzaB.Id);
            if (existingA == null || existingB == null)
                throw new ArgumentException("Пицца не найдена");
        }

        if (orderPizza.Crust != null)
        {
            var existingCrust = _pizzaService._crustService.GetById(orderPizza.Crust.Id);
            if (existingCrust == null)
                throw new ArgumentException("Бортик не найден");
        }
    }
}