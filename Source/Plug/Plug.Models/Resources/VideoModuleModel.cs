using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class VideoModuleModel : ModuleModel
    {
        public new string Icon => "VIDEO";

        public string Uri { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
