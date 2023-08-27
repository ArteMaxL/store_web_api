namespace Core.Entities;

public class Categoria : BaseEntity
{
    public ICollection<Producto> Productos { get; set; }
}
