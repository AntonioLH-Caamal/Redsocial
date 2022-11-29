using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redsocial.Modelos
{
    [Table("Publicaciones")]
    public class Publicacion
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria{ get; set; }

        public string txtPublicacion { get; set; }
        public DateTime fecha { get; set; }
        public bool isVisible { get; set; }
        public  virtual ICollection<Comentario> Comentarios { get; set; }
    }


    public class PublicacionView
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string txtPublicacion { get; set; }
        public DateTime fecha { get; set; }
        public bool isVisible { get; set; }
    }
}
