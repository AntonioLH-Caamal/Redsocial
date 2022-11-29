using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redsocial.Modelos
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string pass { get; set; }


        public virtual ICollection<Publicacion> Publicaciones { get; set; }
        public virtual ICollection<Comentario>Comentarios { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

    }

    public class Login
    {
        public string correo { get; set; }
        public string password { get; set; }
    }
}
