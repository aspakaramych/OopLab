using OopLab.Entity;
using OopLab.Repo;

namespace OopLab.Services;

public abstract class BaseService<T> where T : BaseEntity
{
    protected BaseRepo<T> repo;

    protected BaseService(BaseRepo<T> repo)
    {
        this.repo = repo;
    }

    public virtual void Add(T item)
    {
        repo.Add(item);
    }

    public virtual void Update(T item)
    {
        repo.Update(item);
    }

    public virtual void Delete(Guid id)
    {
        repo.Delete(id);
    }

    public virtual T GetById(Guid id)
    {
        return repo.GetById(id);
    }

    public virtual List<T> GetAll()
    {
        return repo.GetAll();
    }
}