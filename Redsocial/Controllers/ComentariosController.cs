using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redsocial.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Redsocial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentariosController : Controller
    {
        private readonly DbContexto _contexto;

        public ComentariosController(DbContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        [Route("ListComentarios/{id}")]
        public async Task<List<ComentarioView>> ListarPublicaciones(int id)
        {
            var consulta = (from a in _contexto.comentarios
                            join u in _contexto.usuarios on a.IdUsuario equals u.id where a.IdPublicacion == id 
                            select new ComentarioView
                            {
                                Id = a.Id,
                                IdPublicacion = a.IdPublicacion,
                                IdUsuario = a.IdUsuario,
                                NombreUsuario = u.nombre,
                                txtComentario = a.txtComentario,
                                fecha = a.fecha

                            }).OrderBy(f=>f.fecha).ToListAsync();
            return await consulta;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Comentario>> Index(Comentario comentario)
        {
            comentario.fecha = DateTime.Now;
            _contexto.comentarios.Add(comentario);
            await _contexto.SaveChangesAsync();
            return (comentario);
        }

        [HttpGet]
        [Route("Bucar/{id}")]
        public async Task<IActionResult> BuscarPorId(int? id)
        {
            var comentario = _contexto.comentarios.FindAsync(id);

            return Ok(comentario);
        }

        [HttpPost]
        [Route("Modificar")]
        public async Task<IActionResult> Actualiza(Comentario comentario)
        {
            if (comentario == null)
            {
                return BadRequest();
            }
            else
            {
                var coment = _contexto.comentarios.Find(comentario.Id);


                coment.IdPublicacion = comentario.IdPublicacion;
                coment.txtComentario = comentario.txtComentario;
                await _contexto.SaveChangesAsync();
                return Ok();
            }

        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            var comentario = _contexto.comentarios.Find(id);
            _contexto.comentarios.Remove(comentario);
            await _contexto.SaveChangesAsync();
            return Ok();
        }
    }
}
