using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redsocial.Modelos
{
    [Table("Likes")]
    public class Like
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Categorias")]
        public int IdCategoria { get; set; }
        public virtual Categoria Categorias  { get; set; }
        [ForeignKey("Usuarios")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuarios { get; set; }
    }
}
