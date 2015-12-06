using CL.Transverse.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Transverse.ErrorHandling
{
    public class ErrorModel
    {
        public CLErrorType Type { get; set; }

        public String Code { get; set; }

        public String Message { get; set; }

        public ErrorModel(CLErrorType type, string code)
        {
            Type = type;
            Code = code;
            Message = code;
        }

        public ErrorModel(CLException ex)
        {
            Type = ex.ErrorType;
            Code = ex.ErrorCode;
            Message = ex.Message;
        }
    }
}
