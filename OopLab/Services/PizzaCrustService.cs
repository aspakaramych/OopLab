using OopLab.Entity;
using OopLab.Enums;
using OopLab.Repo;

namespace OopLab.Services;

public class PizzaCrustService : BaseService<PizzaCrust>
{
    private readonly PizzaService _pizzaService;
    public PizzaCrustService(PizzaCrustRepo repo, PizzaService pizzaService) : base(repo)
    {
        _pizzaService = pizzaService;
    }

    public void Add(string name, List<Ingredient> ingredients, UsageTypes usageType, List<Guid> pizzaIds)
    {
        var item = new PizzaCrust
        {
            Id = Guid.NewGuid(),
            Name = name,
            Ingredients = ingredients,
            UsageType = usageType,
            PizzaIds = pizzaIds
        };
        
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