using OopLab.Entity;

namespace OopLab.Repo;

public interface IBaseRepo<T> where T : BaseEntity
{
    void Add(T item);
    void Update(T item);
    void Delete(Guid id);
    T GetById(Guid id);
    List<T> GetAll();
}