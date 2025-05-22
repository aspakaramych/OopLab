namespace OopLab.Entity;

public class Pizza : BaseEntity
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

    private PizzaBase _base;

    public PizzaBase Base
    {
        get => _base;
        set => _base = value;
    }

    private PizzaCrust? _crust;

    public PizzaCrust? Crust
    {
        get => _crust;
        set => _crust = value;
    }

    public decimal Cost
    {
        get
        {
            decimal cost = Base.Cost;
            cost += _ingredients.Sum(i => i.Cost);
            cost += _crust != null ? _crust.Ingredients.Sum(i => i.Cost) : 0;
            return cost;
        }
    }
    
    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new Exception("Имя обязательно");
        }

        if (Base == null)
        {
            throw new Exception("Основа обязательна");
        }
    }
}