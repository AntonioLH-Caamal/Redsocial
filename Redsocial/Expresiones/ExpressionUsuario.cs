using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Redsocial.Interfaces;
using Redsocial.Modelos;
using Redsocial.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redsocial.Servicio
{
    public class ExpressionUsuario : IExpression<Usuario>
    { 
        private readonly ILogger<ExpressionUsuario> _logger;
        private readonly Context<Usuario> contextUsuario;
        private readonly DbContexto _contexto;


        public ExpressionUsuario(DbContexto contexto, ILogger<ExpressionUsuario> logger)
        {
            contextUsuario = new Context<Usuario>(contexto);
            _logger = logger;
            _contexto = contexto;  
        }

 


        public async Task<ResponseHelper> Actualizar(Usuario usuario)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                if (await contextUsuario.Actualizar(usuario) > 0)
                {
                    response.Success = true;
                    response.Menssage = "El Usuario se ha actualizado.";
                    _logger.LogInformation(response.Menssage);
                    return response;
                }
            }
            catch (Exception a)
            {
                response.Success = false;
                response.Menssage = "Error al actualizar";
                _logger.LogError(a.Message);
            }

            return response;
        }

        public async Task<Usuario> BuscarPorId(int? id)
        {
            try
            {
                var result = await contextUsuario.BuscarPorId(id);

                return result;
            }
            catch (Exception a)
            {

                _logger.LogError(a.Message);
            }
            return null;
        }

        public async Task<ResponseHelper> Crear(Usuario usuario)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var validaCorreo = (from a in _contexto.usuarios where a.correo == usuario.correo select a);

                if (validaCorreo.Count() == 0)
                {

                    if (await contextUsuario.Crear(usuario) > 0)
                    {
                        response.Success = true;
                        response.Menssage = "Usuario Creado";
                    }
                    else
                    {
                        response.Success = false;
                        response.Menssage = "Usuario No creado";
                    }
                }
            }
            catch (Exception a)
            {
                response.Menssage = "Proceso roto";
                _logger.LogError(a.Message);
            }
            return response;
        }

        public async Task<ResponseHelper> Eliminar(int? id)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var consulta = contextUsuario.BuscarPorId(id);

                if (consulta != null)
                {
                    if (await contextUsuario.Eliminar(id) > 0)
                    {
                        response.Success = true;
                        response.Menssage = "El usuario fue eliminado con éxito.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Menssage = "El usuario fue eliminado con éxito.";
                    }
                }
            }
            catch (Exception a)
            {
                response.Menssage = "Proceso roto";
                _logger.LogError(a.Message);
            }

            return response;
        }

        public async Task<List<Usuario>> ObtieneLista()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                var consulta = await contextUsuario.ObtieneLista();

                if (consulta.Count() > 0)
                {
                    usuarios = consulta.ToList();

                    return usuarios;
                }


                return usuarios;
            }
            catch (Exception a)
            {

                _logger.LogError(a.Message);
            }
            return usuarios;
        }

    }
}
