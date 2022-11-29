using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Redsocial.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redsocial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly ILogger<CategoriasController> _logger;
        private readonly DbContexto _contexto;
        public CategoriasController(DbContexto contexto, ILogger<CategoriasController> logger)
        {
            _contexto = contexto;
            _logger = logger;
        }

        [HttpGet]
        [Route("ListCategorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> ListarUsuarios()
        {
            return await _contexto.categoria.ToListAsync();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Categoria>> Index(Categoria categoria)
        {
                _contexto.categoria.Add(categoria);
                await _contexto.SaveChangesAsync();
                   

            return Ok(categoria);

        }
    }
}
