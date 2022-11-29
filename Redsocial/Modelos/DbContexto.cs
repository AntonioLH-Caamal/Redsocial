using Microsoft.EntityFrameworkCore;

namespace Redsocial.Modelos
{

    public class DbContexto: DbContext
    {
        public DbContexto() { }
        public DbContexto(DbContextOptions<DbContexto> options): base(options){}

        public virtual DbSet<Usuario> usuarios { get; set; }
        public virtual DbSet<Publicacion> publicaciones { get; set; }
        public virtual DbSet<Comentario> comentarios { get; set; }
        public virtual DbSet<Like> likes { get; set; }
        public virtual DbSet<Categoria> categoria { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }
    }
}
