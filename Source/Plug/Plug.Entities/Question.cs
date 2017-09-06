using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    [Table("QuestionModules")]
    public class Question : Module
    {
        [Required]
        [MaxLength(2500)]
        public string Text { get; set; }

        public virtual ICollection<Choice> Choices { get; set; }
    }
}
