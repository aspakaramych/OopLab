using OopLab.Enums;

namespace OopLab.Entity;

public class PizzaCrust : BaseEntity
{
    private string _name;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    private List<Ingredient> _ingredients;

    public List<Ingredient> Ingredients
    {
        get => _ingredients;
        set => _ingredients = value;
    }

    private UsageTypes _usageType;

    public UsageTypes UsageType
    {
        get => _usageType;
        set => _usageType = value;
    }

    private List<Guid> _pizzaIds;

    public List<Guid> PizzaIds
    {
        get => _pizzaIds;
        set => _pizzaIds = value;
    }
    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(_name))
        {
            throw new Exception("Имя обязательно");
        }

        if (_ingredients.Count == 0 || _ingredients == null)
        {
            throw new Exception("Должны быть ингредиенты");
        }
    }
}