using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    [Table("Modules")]
    public abstract class Module : AuditableEntity<Guid>
    {
        [Required]
        [MaxLength(1000)]
        public string Title { get; set; }
        
        public string Icon { get; set; }

        public bool CanSkip { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order { get; set; }

        public Guid CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public virtual ICollection<EnrollModule> EnrollModules { get; set; }
    }
}
