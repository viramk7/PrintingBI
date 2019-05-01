using PrintingBI.Data.Entities;
using System.Collections.Generic;

namespace PrintingBI.Services.Entities
{
    public interface IEntityService<TEntity> where TEntity : BaseEntity
    {
        IList<TEntity> GetAll();

        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);
    }
}
