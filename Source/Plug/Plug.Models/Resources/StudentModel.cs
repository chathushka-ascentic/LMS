using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class StudentModel : ModelBase
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}
