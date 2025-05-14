using OopLab.Entity;

namespace OopLab.Services;

public interface IBaseService<T> where T : BaseEntity
{
    void Add(T item);
    void Update(T item);
    void Delete(Guid id);
    T GetById(Guid id);
    List<T> GetAll();
}