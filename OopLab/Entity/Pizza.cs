namespace OopLab.Entity;

public class Pizza : BaseEntity
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public PizzaBase Base { get; set; }
    public PizzaCrust? Crust { get; set; }

    public decimal Cost
    {
        get
        {
            decimal cost = Base.Cost;
            cost += Ingredients.Sum(i => i.Cost);
            cost += Crust != null ? Crust.Ingredients.Sum(i => i.Cost) : 0;
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