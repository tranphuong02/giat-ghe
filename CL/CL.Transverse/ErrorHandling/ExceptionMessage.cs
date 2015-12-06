using CL.Transverse.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Transverse.ErrorHandling
{
    public static class ExceptionMessage
    {
        /// <summary>
        ///     Get exception message and write to log file
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="messageError"></param>
        /// <param name="sourceLineNumber"></param>
        /// <param name="errorType"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <returns></returns>
        public static string GetExceptionMessage(this Exception ex, string messageError, string memberName, string sourceFilePath, int sourceLineNumber, CLErrorType errorType)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("<Exception details>");
            strBuilder.AppendLine("    We have detected an exception. Message output start");
            strBuilder.AppendLine("    [Date and time of occurrence]");
            strBuilder.AppendLine("        " + DateTime.Now.ToString(Constants.DateFormat.DateWithTime) + "]");
            strBuilder.AppendLine("    [Member Name]");
            strBuilder.AppendLine("        " + memberName);
            strBuilder.AppendLine("    [Source Path]");
            strBuilder.AppendLine("        " + sourceFilePath);
            strBuilder.AppendLine("    [Source Line Number]");
            strBuilder.AppendLine("        " + sourceLineNumber);
            strBuilder.AppendLine("    [Message content]");
            strBuilder.AppendLine("        " + messageError);
            strBuilder.AppendLine("    [Exceptions level]");
            strBuilder.AppendLine("        " + GetErrorTypeText(errorType));
            strBuilder.AppendLine("    [Stack on the internal exception]");
            strBuilder.AppendLine("    [Exception class]      " + ex.GetType());
            strBuilder.AppendLine("    [Exception message]    " + ex.Message);
            strBuilder.AppendLine("    [Exception stack trace]" + ex.StackTrace);

            //Get InnerException Message
            if (ex.InnerException != null)
            {
                strBuilder.AppendLine();
                strBuilder.AppendLine("    [It outputs the exception that was detected by the internal.]");
                strBuilder.AppendLine("        [Exception class]       " + ex.InnerException.GetType());
                strBuilder.AppendLine("        [Exception message]     " + ex.InnerException.Message);
                strBuilder.AppendLine("        [Exception stack trace] " + ex.InnerException.StackTrace);
            }

            strBuilder.AppendLine("</Exception details>");
            strBuilder.AppendLine();

            //return exception info
            return strBuilder.ToString();
        }

        private static string GetErrorTypeText(CLErrorType errorType)
        {
            //Get level of exception
            switch (errorType)
            {
                case CLErrorType.Error:
                    return Constants.CLLogConstants.Error;

                case CLErrorType.Warn:
                    return Constants.CLLogConstants.Warn;

                case CLErrorType.Info:
                    return Constants.CLLogConstants.Info;
            }
            return string.Empty;
        }
    }
}
