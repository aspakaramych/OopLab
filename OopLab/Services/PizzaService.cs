using OopLab.Entity;
using OopLab.Enums;
using OopLab.Repo;

namespace OopLab.Services;

public class PizzaService : BaseService<Pizza>
{
    private readonly PizzaRepo _repo;
    public readonly PizzaBaseService _baseService;
    public readonly IngredientService _ingredientService;
    public readonly PizzaCrustService _crustService;

    public PizzaService(PizzaRepo repo, PizzaBaseService baseService, IngredientService ingredientService, PizzaCrustService crustService) : base(repo)
    {
        _repo = repo;
        _baseService = baseService;
        _ingredientService = ingredientService;
        _crustService = crustService;
    }

    public override void Add(Pizza item)
    {
        var existingBase = _baseService.GetById(item.Base.Id);
        if (existingBase == null)
            throw new ArgumentException("Основа не найдена");

        foreach (var ingredient in item.Ingredients)
        {
            var existingIngredient = _ingredientService.GetById(ingredient.Id);
            if (existingIngredient == null)
                throw new ArgumentException("Нет ингредиента");
        }

        if (item.Crust != null)
        {
            var existingCrust = _crustService.GetById(item.Crust.Id);
            if (existingCrust == null)
                throw new ArgumentException("Бортик не найден");
            if (existingCrust.UsageType == UsageTypes.Allow && !existingCrust.PizzaIds.Contains(item.Id))
                throw new ArgumentException($"Этот бортик {existingCrust.Name} нельзя использовать с этой пиццой");
            if (existingCrust.UsageType == UsageTypes.Block && existingCrust.PizzaIds.Contains(item.Id))
                throw new ArgumentException($"Этот бортик {existingCrust.Name} нельзя использовать с этой пиццой");
        }
            
        

        base.Add(item);
    }
}