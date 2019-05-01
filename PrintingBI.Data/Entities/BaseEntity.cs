using System.ComponentModel.DataAnnotations;

namespace PrintingBI.Data.Entities
{
    public abstract partial class BaseEntity
    {
        
    }

    public interface IEntity<T>
    {
        T Id { get; set; }
    }

    public  abstract class Entity<T> : BaseEntity,IEntity<T> 
    {
        [Key]
        public T Id { get; set; }
    }
}