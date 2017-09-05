using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    public abstract class EntityBase<T>
    {
        [Key, Column(Order = 0)]
        public T Id { get; set; }
    }
}
