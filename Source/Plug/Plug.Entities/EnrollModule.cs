using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    [Table("EnrollModule")]
    public class EnrollModule
    {
        [Key, Column(Order = 0)]
        public Guid EnrollmentId { get; set; }

        [Key, Column(Order = 1)]
        public Guid ModuleId { get; set; }

        public virtual Enrollment Enrollment { get; set; }

        public virtual Module Module { get; set; }

        public bool IsStarted { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime LastVisited { get; set; }
    }
}
