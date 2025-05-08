namespace OopLab.Entity;

public class Order : BaseEntity
{
    public List<OrderPizza> Pizzas { get; set; } = new List<OrderPizza>();
    public string Comment { get; set; }
    public DateTime OrderTime { get; set; }
    public bool IsDeferred { get; set; }
    public DateTime? DeferredDateTime { get; set; }
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
            throw new ArgumentException("Order must have at least one pizza");
    }
}