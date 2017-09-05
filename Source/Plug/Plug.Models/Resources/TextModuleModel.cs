using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class TextModuleModel : ModuleModel
    {
        public new string Icon => "TEXT";

        public string Description { get; set; }
    }
}
