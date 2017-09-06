using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class QuestionModuleModel : ModuleModel
    {
        public new string Icon => "QUESTION";

        public string Text { get; set; }

        public virtual List<ChoiceModel> Choices { get; set; }
    }
}
