using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redsocial.Modelos
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        public string nombre { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Publicacion> Publicaciones { get; set; }


    }
}
