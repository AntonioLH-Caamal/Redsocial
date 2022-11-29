using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Redsocial.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Redsocial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : Controller 
    {
        private readonly ILogger<LikesController> _logger;
        private readonly DbContexto _contexto;
        public LikesController(DbContexto contexto, ILogger<LikesController> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }


        [HttpGet]
        [Route("ListPublicaciones/{id}")]
        public async Task<List<PublicacionView>> ListarPublicaciones(int id)
        {
            //var recomendacion = (from c in _contexto.categoria select c);

            //foreach (var element in recomendacion)
            //{
            //    var buscarLike = (from b in _contexto.likes where b.IdUsuario == id && b.IdCategoria== element.Id);
            //}
            
            var consulta = (from a in _contexto.publicaciones
                            join j in _contexto.categoria on a.IdCategoria equals j.Id
                            join u in _contexto.usuarios on a.IdUsuario equals u.id join l in _contexto.likes on a.IdUsuario equals l.IdUsuario  
                            select new PublicacionView
                            {
                                Id = a.Id,
                                IdUsuario = a.IdUsuario,
                                NombreUsuario = u.nombre,
                                IdCategoria = a.IdCategoria,
                                NombreCategoria = j.nombre,
                                txtPublicacion = a.txtPublicacion,
                                fecha = a.fecha,
                                isVisible = a.isVisible

                            }).ToListAsync();


            return await consulta;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Like>> Index(Like like)
        {
            
            _contexto.likes.Add(like);
            await _contexto.SaveChangesAsync();


            return Ok(like);

        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            var publicacion = _contexto.likes.Find(id);
            _contexto.likes.Remove(publicacion);
            await _contexto.SaveChangesAsync();
            return Ok();
        }
    }
}
