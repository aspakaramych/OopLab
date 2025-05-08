using OopLab.Entity;
using OopLab.Repo;

namespace OopLab.Services;

public class PizzaBaseService : BaseService<PizzaBase>
{
    private readonly PizzaBaseRepo _repo;
    public PizzaBaseService(PizzaBaseRepo repo) : base(repo)
    {
        _repo = repo;
    }

    public override void Add(PizzaBase item)
    {
        ValidatePizzaBase(item);
        base.Add(item);
    }

    public override void Update(PizzaBase item)
    {
        ValidatePizzaBase(item);
        base.Update(item);
    }

    private void ValidatePizzaBase(PizzaBase item)
    {
        if (item.IsClassic)
        {
            var existingClassic = _repo.GetAll().FirstOrDefault(b => b.IsClassic);
            if (existingClassic != null && existingClassic.Id != item.Id)
            {
                throw new ArgumentException("Может существовать только 1 классическая основа");
            }
        }
        else
        {
            var classicBase = _repo.GetAll().FirstOrDefault(b => b.IsClassic);
            if (classicBase != null)
            {
                decimal maxCost = classicBase.Cost * 1.2m;
                if (item.Cost > maxCost)
                {
                    throw new ArgumentException($"Основа не может стоить больше чем на 20% от классической, максимальная цена: {maxCost}");
                }
            }
        }
    }
}