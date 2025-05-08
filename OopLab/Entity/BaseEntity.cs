namespace OopLab.Entity;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public abstract void Validate();
}