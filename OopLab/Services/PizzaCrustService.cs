using OopLab.Entity;
using OopLab.Repo;

namespace OopLab.Services;

public class PizzaCrustService : BaseService<PizzaCrust>
{
    private readonly PizzaService _pizzaService;
    public PizzaCrustService(PizzaCrustRepo repo, PizzaService pizzaService) : base(repo)
    {
        _pizzaService = pizzaService;
    }

    public override void Add(PizzaCrust item)
    {
        foreach (var ingredient in item.Ingredients)
        {
            var existing = _pizzaService._ingredientService.GetById(ingredient.Id);
            if (existing == null)
                throw new ArgumentException("Нет ингредиентов под бортики");
        }

        foreach (var pizzaId in item.PizzaIds)
        {
            var pizza = _pizzaService.GetById(pizzaId);
            if (pizza == null)
                throw new ArgumentException("Нельзя использовать бортик с этой пиццой");
        }

        base.Add(item);
    }
}