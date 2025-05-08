using OopLab.Entity;

namespace OopLab.Repo;

public abstract class BaseRepo<T> where T : BaseEntity
{
    protected List<T> items = new List<T>();

    public virtual void Add(T item)
    {
        item.Validate();
        items.Add(item);
    }

    public virtual void Update(T item)
    {
        var existingItem = items.FirstOrDefault(x => x.Id == item.Id);
        if (existingItem != null)
        {
            items.Remove(existingItem);
            item.Validate();
            items.Add(item);
        }
    }

    public virtual void Delete(Guid id)
    {
        var existingItem = items.FirstOrDefault(x => x.Id == id);
        if (existingItem != null)
        {
            items.Remove(existingItem);
        }
        else
        {
            throw new ArgumentException("Элемент не найден");
        }
    }

    public virtual T GetById(Guid id)
    {
        return items.FirstOrDefault(x => x.Id == id);
    }

    public virtual List<T> GetAll()
    {
        return items;
    }
}