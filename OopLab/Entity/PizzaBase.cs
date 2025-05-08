namespace OopLab.Entity;

public class PizzaBase : BaseEntity
{
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public bool IsClassic { get; set; }
    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new Exception("Имя обязательно");
        }

        if (Cost < 0)
        {
            throw new Exception("Цена должна быть больше 0");
        }
    }
}