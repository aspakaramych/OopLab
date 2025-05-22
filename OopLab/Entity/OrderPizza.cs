using OopLab.Enums;

namespace OopLab.Entity;

public class OrderPizza
{
    private Pizza? _pizza;

    public Pizza? Pizza
    {
        get => _pizza;
        set => _pizza = value;
    }

    private Size _size;

    public Size Size
    {
        get => _size;
        set => _size = value;
    }
    public Dictionary<Ingredient, int> ExtraIngredients { get; set; } = new Dictionary<Ingredient, int>();
    private bool _isCustom;

    public bool IsCustom
    {
        get => _isCustom;
        set => _isCustom = value;
    }
    public Pizza? HalfPizzaA { get; set; }
    public Pizza? HalfPizzaB { get; set; }
    
    public PizzaCrust? Crust { get; set; }

    public decimal Cost
    {
        get
        {
            decimal cost = Pizza == null ? 0 : Pizza.Cost;
            cost += HalfPizzaA == null ? 0 : HalfPizzaA.Cost / 2;
            cost += HalfPizzaB == null ? 0 : HalfPizzaB.Cost / 2;
            decimal sizeMultiplier = Size == Size.Small ? 1 : Size == Size.Medium ? 1.5m : 2;
            cost = cost * sizeMultiplier;
            foreach (var kvp in ExtraIngredients)
            {
                cost += Pizza.Ingredients.Where(i => i.Id == kvp.Key.Id).Sum(i => i.Cost) * (kvp.Value - 1);
            }
            return cost;
        }
    }
}