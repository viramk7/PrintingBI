using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintingBI.Services.Entities
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repository;

        public EntityService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public IList<TEntity> GetAll()
        {
            return _repository.Table.ToList();
        }

        public TEntity GetById(object id)
        {
            return _repository.GetById(id);
        }

        public void Insert(TEntity entity)
        {
            _repository.Insert(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _repository.Insert(entities);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _repository.Update(entities);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _repository.Delete(entities);
        }
    }
}
