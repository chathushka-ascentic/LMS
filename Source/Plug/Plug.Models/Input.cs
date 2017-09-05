using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class Input<T>
    {
        public string OperationBy { get; set; }

        public T Arguments { get; set; }

        public virtual bool Validate
        {
            get
            {
                return Arguments != null;
            }
        }
    }
}
