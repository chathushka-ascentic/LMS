using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class ModuleModel : ModelBase
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public bool CanSkip { get; set; }

        public int Order { get; set; }
    }
}
