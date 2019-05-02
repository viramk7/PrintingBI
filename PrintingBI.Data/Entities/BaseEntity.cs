using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}