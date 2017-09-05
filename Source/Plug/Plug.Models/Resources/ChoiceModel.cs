using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class ChoiceModel : ModelBase
    {
        public string Option { get; set; }

        public bool IsAnswer { get; set; }

        public bool Checked { get; set; }
    }
}
