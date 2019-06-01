using AutoMapper;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.Generic;
using System.Collections.Generic;
using System.Linq;

namespace PrintingBI.Services.Entities
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public EntityService(IRepository<TEntity> repository,
                             IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IList<TEntity> GetAll()
        {
            return _repository.Table.ToList();
        }

        public TDto GetAll<TDto>()
        {
            var list = _repository.Table.ToList();
            return _mapper.Map<TDto>(list);
        }

        public TEntity GetById(object id)
        {
            return _repository.GetById(id);
        }

        public TDto GetById<TDto>(object id)
        {
            var entity = _repository.GetById(id);
            return _mapper.Map<TDto>(entity);
        }

        public void Insert(TEntity entity)
        {
            _repository.Insert(entity);
        }

        public void Insert<TDto>(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Insert(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _repository.Insert(entities);
        }

        public void Insert<TDto>(IEnumerable<TDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
            _repository.Insert(entities);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public void Update<TDto>(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Update(entity);
        }

        public void Update<TDto>(object id,TDto dto)
        {
            var oldEntity = _repository.GetById(id);
            var entity = _mapper.Map(dto, oldEntity);
            _repository.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _repository.Update(entities);
        }

        public void Update<TDto>(IEnumerable<TDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
            _repository.Update(entities);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public void Delete<TDto>(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Delete(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _repository.Delete(entities);
        }

        public void Delete<TDto>(IEnumerable<TDto> dtos)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(dtos);
            _repository.Delete(entities);
        }

        public void DeleteById(object id)
        {
            _repository.DeleteById(id);
        }
    }
}
