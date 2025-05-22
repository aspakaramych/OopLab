namespace OopLab.Entity;

public abstract class BaseEntity
{
    private Guid _id;
    public Guid Id
    {
        get => _id;
        set => _id = value;
    }
    
    public abstract void Validate();
}