using System;
using System.Collections.Generic;
using System.Text;

namespace Plug.Models
{
    public class OutputResult
    {
        public bool Sucess { get; set; }

        public string AdditionalMessage { get; set; }

        public Error Error { get; set; }
        
        public static OutputResult FailOutputResult(string errorMessage, string additionalMessage = null, string description = null)
        {
            return new OutputResult
            {
                Sucess = false,
                Error = new Error(errorMessage, description),
                AdditionalMessage = additionalMessage
            };
        }

        public static OutputResult SuccessOutputResult(string additionalMessage = null)
        {
            return new OutputResult
            {
                Sucess = true,
                AdditionalMessage = additionalMessage
            };
        }
    }
}
