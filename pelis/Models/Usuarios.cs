using System.ComponentModel.DataAnnotations.Schema;

namespace pelis.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Email { get; set; }

        [ForeignKey("Perfil")]
        public int? PerfilId { get; set; }
        public virtual Perfil? Perfil { get; set; }

    }
}
