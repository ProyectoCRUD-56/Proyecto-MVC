using System.ComponentModel.DataAnnotations.Schema;

namespace pelis.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        [ForeignKey("CategoriaProductos")]
        public int? CategoriaId { get; set; }
        public virtual CategoriaProductos? CategoriaProductos { get; set; }


    }
}
