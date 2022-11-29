using Microsoft.Extensions.Logging;
using Redsocial.Interfaces;
using Redsocial.Modelos;
using Redsocial.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Redsocial.Servicio
{
    public class ExpressionPublicacion : IExpression<Publicacion>
    {
        private readonly ILogger<ExpressionPublicacion> _logger;
        private readonly Context<Publicacion> _ContextPublicacion;
        private readonly DbContexto _contexto;

        public ExpressionPublicacion(DbContexto contexto, ILogger<ExpressionPublicacion> logger)
        {
            _ContextPublicacion = new Context<Publicacion>(contexto);
            _logger = logger;
            _contexto = contexto;
        }
        public Task<ResponseHelper> Actualizar(Publicacion publicacion)
        {
            throw new System.NotImplementedException();
        }

        public Task<Usuario> BuscarPorId(int? id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResponseHelper> Crear(Publicacion publicacion)
        {
            ResponseHelper response  = new ResponseHelper();
            try
            {
                if ( await _ContextPublicacion.Crear(publicacion)>0)
                {
                    response.Success = true;
                    response.Menssage = "Publicacion Creada";

                }
                else
                {
                    response.Success = false;
                    response.Menssage = "Publicacion NO creada";

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

        public async Task<List<PublicacionView>> ObtieneListaView()
        {
            try
            {
                var consulta = (from a in _contexto.publicaciones
                                join j in _contexto.categoria on a.IdCategoria equals j.Id
                                join u in _contexto.usuarios on a.IdUsuario equals u.id
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
            catch (Exception a)
            {

                _logger.LogError(a.Message);
            }
            return null;
        }

        Task<Publicacion> IExpression<Publicacion>.BuscarPorId(int? id)
        {
            throw new NotImplementedException();
        }

        Task<List<Publicacion>> IExpression<Publicacion>.ObtieneLista()
        {
            throw new NotImplementedException();
        }
    }
}
