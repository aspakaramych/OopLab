using OopLab.Repo;

namespace OopLab.Services;

public class IngredientService : BaseService<Ingredient>
{
    public IngredientService(BaseRepo<Ingredient> repo) : base(repo)
    {
    }

    public void Add(string name, decimal cost)
    {
        var ingredient = new Ingredient 
        { 
            Id = Guid.NewGuid(), 
            Name = name, 
            Cost = cost 
        };
        base.Add(ingredient);
    }
}