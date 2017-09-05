using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    [Table("TextModules")]
    public class Text : Module
    {
        [Required]
        public string Description { get; set; }
    }
}
