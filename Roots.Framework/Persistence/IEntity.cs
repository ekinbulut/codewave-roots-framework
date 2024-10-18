namespace Roots.Framework.Persistence;

public interface IEntity<T>
{
    public T Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
}