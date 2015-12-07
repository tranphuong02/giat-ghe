using CL.Transverse.Enums;

namespace CL.Transverse.ErrorHandling
{
    public class CLBusinessException : CLException
    {
        /// <summary>
        ///     Initialize for VST business exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorType"></param>
        /// <param name="parammeters"></param>
        public CLBusinessException(string errorCode, CLErrorType errorType, params object[] parammeters)
            : base(errorCode, errorType, parammeters)
        {
            Parammeters = parammeters;
            if (errorType == CLErrorType.Error)
            {
                //Log error
                var errorMsg = string.Format(errorCode, parammeters);
                Framework.Logger.Provider.Instance.LogError(this.GetExceptionMessage(errorMsg, CLErrorType.Error));
            }
        }

        /// <summary>
        ///     Translate and add parameters for MSG
        /// </summary>
        public override string Message
        {
            get
            {
                if (Parammeters != null && Parammeters.Length > 0)
                {
                    return string.Format(base.Message, Parammeters);
                }
                return base.Message;
            }
        }

        /// <summary>
        ///     Translate and add parameters for ErrorCode
        /// </summary>
        public string ErrorCodeMsg
        {
            get
            {
                if (Parammeters != null && Parammeters.Length > 0)
                {
                    return string.Format(ErrorCode, Parammeters);
                }
                return ErrorCode;
            }
        }

        /// <summary>
        ///     Messages parameters
        /// </summary>
        public object[] Parammeters { get; set; }
    }
}
