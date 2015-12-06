using System;
using System.Configuration;
using System.IO;

namespace CL.Framework.Logger
{
    public static class Constant
    {
        public static class Paths
        {
            public static readonly string BaseDirectory = (string)AppDomain.CurrentDomain.BaseDirectory;

            public static string Debug = Path.Combine(BaseDirectory, ConfigurationManager.AppSettings["LoggerDebugPath"]);
            public static string Info = Path.Combine(BaseDirectory, ConfigurationManager.AppSettings["LoggerInfoPath"]);
            public static string Warn = Path.Combine(BaseDirectory, ConfigurationManager.AppSettings["LoggerWarnPath"]);
            public static string Error = Path.Combine(BaseDirectory, ConfigurationManager.AppSettings["LoggerErrorPath"]);
            public static string Fatal = Path.Combine(BaseDirectory, ConfigurationManager.AppSettings["LoggerFatalPath"]);
        }
    }
}
