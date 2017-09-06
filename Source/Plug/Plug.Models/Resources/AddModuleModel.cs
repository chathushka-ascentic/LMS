using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class AddModuleModel
    {
        public bool CanSkip { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public string Icon { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Uri { get; set; }
        public List<ChoiceModel> Choices { get; set; }
    }
}
