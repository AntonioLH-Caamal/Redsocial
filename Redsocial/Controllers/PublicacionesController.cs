using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Redsocial.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Redsocial.Interfaces;

namespace Redsocial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : Controller
    {
        private readonly DbContexto _contexto;
        private readonly ILogger<PublicacionesController> _logger;
        private readonly IExpression<Publicacion> _expression;
        public PublicacionesController(IExpression<Publicacion> expression,DbContexto dbContexto, ILogger<PublicacionesController> logger)
        {
            _contexto = dbContexto;
            _logger = logger;
            _expression = expression;
        }


        [HttpGet]
        [Route("ListPublicaciones/{id}")]
        public async Task<IActionResult> ListarUsuarios(int? id)
        {
            var consulta =  await (from a in _contexto.publicaciones
                            join j in _contexto.categoria on a.IdCategoria equals j.Id
                            join u in _contexto.usuarios on a.IdUsuario equals u.id where a.IdUsuario != id
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
             
            return Ok(consulta);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult>Index(Publicacion publicacion)
        {
            var response = await _expression.Crear(publicacion);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("Bucar/{id}")]
        public async Task<IActionResult> BuscarPorId(int? id)
        {
            var publicacion = _contexto.publicaciones.FindAsync(id);

            return Ok(publicacion);
        }

        [HttpPost]
        [Route("Modificar")]
        public async Task<IActionResult> Actualiza(Publicacion publicacion)
        {
            if (publicacion == null)
            {
                return BadRequest();
            }
            else
            {
                var publication= _contexto.publicaciones.Find(publicacion.Id);

                publication.IdUsuario = publicacion.IdUsuario;
                publication.txtPublicacion = publicacion.txtPublicacion;
                publication.isVisible = publicacion.isVisible;

                await _contexto.SaveChangesAsync();
                return Ok();
            }

        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            var publicacion = _contexto.publicaciones.Find(id);
            _contexto.publicaciones.Remove(publicacion);
            await _contexto.SaveChangesAsync();
            return Ok();
        }
    }
}
