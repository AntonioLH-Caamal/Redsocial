using Microsoft.Extensions.Logging;
using Redsocial.Interfaces;
using Redsocial.Modelos;
using Redsocial.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redsocial.Servicio
{
    public class ExpressionComentario : IExpression<Comentario>
    {
        private readonly ILogger<ExpressionComentario> _logger;
        private readonly Context<Comentario> _ContextComentario;
        private readonly DbContexto _contexto;

        public ExpressionComentario(DbContexto contexto, ILogger<ExpressionComentario> logger)
        {
            _ContextComentario = new Context<Comentario>(contexto);
            _logger = logger;
            _contexto = contexto;
        }
        public Task<ResponseHelper> Actualizar(Comentario comentario)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comentario> BuscarPorId(int? id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseHelper> Crear(Comentario comentario)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                if (await _ContextComentario.Crear(comentario) > 0)
                {
                    response.Success = true;
                    response.Menssage = "Comentario ha Creada";

                }
                else
                {
                    response.Success = false;
                    response.Menssage = "Comentario NO creada";

                }

            }
            catch (Exception a)
            {

                _logger.LogError(a.Message);
            }

            return response;
        }

        public Task<ResponseHelper> Eliminar(int? id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<ComentarioView>> ObtieneLista()
        {
            throw new System.NotImplementedException();
        }

        Task<List<Comentario>> IExpression<Comentario>.ObtieneLista()
        {
            throw new NotImplementedException();
        }
    }
}
