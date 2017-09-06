using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    [Table("Courses")]
    public class Course : AuditableEntity<Guid>
    {
        [Required]
        [MaxLength(1000)]
        public string Subject { get; set; }

        [MaxLength(5000)]
        public string Description { get; set; }

        public virtual ICollection<Module> Modules { get; set; }
    }
}
