using Redsocial.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Redsocial.Interfaces
{
    public interface IExpression<TEntity> where TEntity : class
    {
        Task<ResponseHelper> Crear(TEntity entity);

        Task<ResponseHelper> Eliminar(int? id);

        Task<ResponseHelper> Actualizar(TEntity entity);

        Task<List<TEntity>> ObtieneLista();

        Task<TEntity> BuscarPorId(int? id);
    }
}
