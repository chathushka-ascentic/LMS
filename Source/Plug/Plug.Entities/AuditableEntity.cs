using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    public abstract class AuditableEntity<T> : EntityBase<T>
    {
        [Column(Order = 1)]
        public string CreateBy { get; set; }

        [Column(Order = 2)]
        public DateTime CreateDate { get; set; }

        [Column(Order = 3)]
        public string UpdateBy { get; set; }

        [Column(Order = 4)]
        public DateTime UpdateDate { get; set; }
    }
}
