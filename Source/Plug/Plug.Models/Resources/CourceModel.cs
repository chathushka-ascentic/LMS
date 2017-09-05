using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class CourseModel : ModelBase
    {
        public Guid Id { get; set; }

        public string Subject { get; set; }
        
        public string Description { get; set; }

        public bool Enrolled { get; set; }

        public Guid? EnrolledId { get; set; }

        public virtual List<ModuleModel> Modules { get; set; }
    }
}
