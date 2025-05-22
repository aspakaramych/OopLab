using OopLab.Entity;

public class Ingredient : BaseEntity
{
    private string _name;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    private decimal _cost;

    public decimal Cost
    {
        get => _cost;
        set => _cost = value;
    }
    
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