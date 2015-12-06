using CL.Transverse.Enums;
using System;

namespace CL.Transverse.ErrorHandling
{
    public class CLException : Exception
    {
        private readonly object[] _params;

        /// <summary>
        ///     Initialize for VST generic exception
        /// </summary>
        public String ErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Error type defined from business logic
        /// </summary>
        public CLErrorType ErrorType
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialize for VST generic exception
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="parammeters"></param>
        public CLException(String errorCode, CLErrorType errorType, params object[] parammeters)
        {
            this.ErrorCode = errorCode;
            this.ErrorType = errorType;
            this._params = parammeters;
        }

        /// <summary>
        ///     Initialize for VST generic exception with inner exception
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="errorCode"></param>
        /// <param name="parammeters"></param>
        public CLException(Exception innerException, String errorCode, CLErrorType errorType, params object[] parammeters)
            : base(String.Empty, innerException)
        {
            this.ErrorCode = errorCode;
            this.ErrorType = errorType;
            this._params = parammeters;
        }

        /// <summary>
        ///     Override returning error message
        /// </summary>
        public override string Message
        {
            get
            {
                return _params != null ? String.Format(ErrorCode, _params) : ErrorCode;
            }
        }
    }
}
