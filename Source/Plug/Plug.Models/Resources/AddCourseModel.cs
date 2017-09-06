using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class AddCourseModel
    {
        public string Subject { get; set; }

        public string Description { get; set; }

        public virtual List<AddModuleModel> Modules { get; set; }
    }
}
