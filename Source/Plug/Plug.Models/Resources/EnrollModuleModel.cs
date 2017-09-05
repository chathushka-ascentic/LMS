using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class EnrollModuleModel : ModelBase
    {
        public Guid EnrollmentId { get; set; }

        public Guid ModuleId { get; set; }

        public bool IsStarted { get; set; }

        public bool IsCompleted { get; set; }

        public ModuleModel Module { get; set; }

        public DateTime LastVisited { get; set; }

        public Guid CompletedEnrollmentId { get; set; }

        public Guid CompletedModuleId { get; set; }
    }
}
