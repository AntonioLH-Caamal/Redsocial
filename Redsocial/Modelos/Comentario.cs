using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redsocial.Modelos
{
    [Table("Comentarios")]
    public class Comentario
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Publicacion")]
        public int IdPublicacion {  get; set; }
        public virtual Publicacion Publicacion { get; set; }

        [ForeignKey("Usuarios")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public string txtComentario { get; set; }
        public DateTime fecha { get; set; }

    }

    public class ComentarioView
    {
        public int Id { get; set; }
        public int IdPublicacion { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string txtComentario { get; set; }
        public DateTime fecha { get; set; }

    }


}
