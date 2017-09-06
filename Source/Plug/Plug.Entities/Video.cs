using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plug.Entities
{
    [Table("VideoModules")]
    public class Video : Module
    {
        [Required]
        public string Uri { get; set; }
        
        public TimeSpan Duration { get; set; }
    }
}
