using OopLab.Enums;

namespace OopLab.Entity;

public class OrderPizza
{
    public Pizza Pizza { get; set; }
    public Size Size { get; set; }
    public Dictionary<Ingredient, int> ExtraIngredients { get; set; } = new Dictionary<Ingredient, int>();
    public bool IsCustom {get; set; }
    public Pizza HalfPizzaA { get; set; }
    public Pizza HalfPizzaB { get; set; }
    
    public PizzaCrust? Crust { get; set; }

    public decimal Cost
    {
        get
        {
            decimal cost = Pizza.Cost;
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