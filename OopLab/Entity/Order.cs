namespace OopLab.Entity;

public class Order : BaseEntity
{
    private List<OrderPizza> _pizzas = new List<OrderPizza>();

    public List<OrderPizza> Pizzas
    {
        get => _pizzas;
        set => _pizzas = value ?? new List<OrderPizza>();
    }

    private string _comment;

    public string Comment
    {
        set => _comment = value;
    }

    private DateTime _orderTime;

    public DateTime OrderTime {
        get => _orderTime;
        set => _orderTime = value;
    }

    private bool _isDeferred;

    public bool IsDeferred
    {
        get => _isDeferred;
        set => _isDeferred = value;
    }

    private DateTime? _deferredDateTime;
    public DateTime? DeferredDateTime{
        get => _deferredDateTime;
        set => _deferredDateTime = value;
        
    }
    public decimal TotalCost 
    { 
        get 
        {
            return Pizzas.Sum(p => p.Cost);
        } 
    }
    public override void Validate()
    {
        if (Pizzas == null || Pizzas.Count == 0)
            throw new ArgumentException("Заказ должен иметь хотя бы 1 пиццу");
        if (DeferredDateTime < DateTime.Now)
        {
            throw new ArgumentException("Нельзя сделать заказ в прошлое");
        }
    }
}