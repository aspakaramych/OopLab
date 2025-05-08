using OopLab.Repo;

namespace OopLab.Services;

public class IngredientService : BaseService<Ingredient>
{
    public IngredientService(BaseRepo<Ingredient> repo) : base(repo)
    {
    }
}