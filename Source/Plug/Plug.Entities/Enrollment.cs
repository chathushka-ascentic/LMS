using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    [Table("Enrollments")]
    public class Enrollment : AuditableEntity<Guid>
    {
        public Guid CourseId { get; set; }
        
        public Guid StudentId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        public virtual ICollection<EnrollModule> EnrollModules { get; set; }
    }
}
