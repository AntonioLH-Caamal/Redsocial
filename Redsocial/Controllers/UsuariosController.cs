using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redsocial.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Redsocial.Interfaces;
using Redsocial.Servicio;
using System;

namespace Redsocial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IExpression<Usuario>  _expression;
        private readonly DbContexto _contexto;
        public UsuariosController(IExpression<Usuario> expression, ILogger<UsuariosController> logger,  DbContexto context)
        {
            _logger = logger;
            _expression = expression;
            _contexto = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUsuarios(Login login)
        {
           
            var response = new ResponseHelper();
            var user = (from a in _contexto.usuarios where a.correo == login.correo && a.pass == login.password select a.id);



            if (user.Count() > 0)
            {
                _logger.LogInformation("User logged in");
                response.Success = true;
                response.Menssage = "Acesso Correcto";
                return Ok(user);
            }
            else
            {
                response.Success = false;
                response.Menssage = "Verifique contraseña o correo";
                return Ok(response);
            }


        }


        [HttpGet]
        [Route("ListUsers")]
        public async Task<IActionResult> ListarUsuarios()
        {
            List<Usuario> usuarios = await _expression.ObtieneLista();

            return Ok(usuarios);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult>Index(Usuario usuario)
        {
           var response = await _expression.Crear(usuario);
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
            var usuario = await _contexto.usuarios.FindAsync(id);

            return Ok(usuario);
        }

        [HttpPost]
        [Route("Modificar")]
        public async Task<IActionResult> Actualiza(Usuario usuario)
        {
            ResponseHelper response = await _expression.Actualizar(usuario);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {

            ResponseHelper response = new ResponseHelper();
            if (id == null || id == 0)
            {
                response.Success = false;
                response.Menssage = "EL id del elemento no encontrado";
                return BadRequest(response);
            }

            response = await _expression.Eliminar(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
