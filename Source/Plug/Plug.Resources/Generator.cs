using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Resources
{
    public class Generator
    {
        /// <summary>
        /// Generate an Unique Identifier
        /// </summary>
        public static Guid UniqueIdentifier => Guid.NewGuid();
    }
}
