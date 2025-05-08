using OopLab.Enums;

namespace OopLab.Entity;

public class PizzaCrust : BaseEntity
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public UsageTypes UsageType { get; set; }
    public List<Guid> PizzaIds { get; set; }
    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new Exception("Имя обязательно");
        }

        if (Ingredients.Count == 0 || Ingredients == null)
        {
            throw new Exception("Должны быть ингредиенты");
        }
    }
}