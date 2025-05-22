namespace OopLab.Entity;

public class PizzaBase : BaseEntity
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

    private bool _isClassic;

    public bool IsClassic
    {
        get => _isClassic;
        set => _isClassic = value;
    }
    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(_name))
        {
            throw new Exception("Имя обязательно");
        }

        if (Cost < 0)
        {
            throw new Exception("Цена должна быть больше 0");
        }
    }
}