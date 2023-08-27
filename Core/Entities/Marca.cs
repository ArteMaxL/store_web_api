namespace Core.Entities;

public class Marca : BaseEntity
{
    public ICollection<Producto> Productos { get; set; }
}
