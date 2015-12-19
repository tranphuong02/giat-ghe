using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace CL.Transverse
{
    public static class Constants
    {
        public class ResourcePath
        {
            public static readonly string AppDataFolder = (string)AppDomain.CurrentDomain.GetData("DataDirectory") ?? AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            public static readonly string Resource = Path.Combine(BaseDirectory, ConfigurationManager.AppSettings["ResourceDirectory"]);
        }

        public class FileExtension
        {
            public static readonly List<string> Image = new List<string>
            {
                "jpg",
                "png",
                "jpeg",
                "ico",
                "gif"
            };
            public static readonly List<string> Html = new List<string>
            {
                "htm",
                "html"
            };
            public static readonly List<string> Pdf = new List<string>
            {
                "pdf"
            };
            public static readonly List<string> Word = new List<string>
            {
                "doc",
                "docx",
            };
            public static readonly List<string> Excel = new List<string>
            {
                "xls",
                "xlsx",
            };
            public static readonly List<string> Txt = new List<string>
            {
                "txt"
            };
        }
        public static class GeneralAdminConfigs
        {
            public const string AdminArea = "Administrator";
            public const string Layout = "~/Areas/Administrator/Views/Shared/_Admin.cshtml";
            public const string StatusMessageText = "StatusMessageText";
            public const string StatusMessageType = "StatusMessageType";
        }

        public class DateFormat
        {
            /// <summary>
            /// date time format yyyy-MM-dd
            /// </summary>
            public const string DateOnly = "yyyy-MM-dd";

            /// <summary>
            /// date time format yyyy-MM-dd
            /// </summary>
            public const string TimeOnly = "HH:mm:ss";

            /// <summary>
            /// date time format yyyy-MM-dd HH:mm:ss
            /// </summary>
            public const string DateWithTime = "yyyy-MM-dd HH:mm:ss";

            /// <summary>
            /// date time format yyyy-MM-dd-HH-mm-ss
            /// </summary>
            public const string FullDateTime = "yyyy-MM-dd-HH-mm-ss";

            /// <summary>
            /// date time format dd-MM-yyyy
            /// </summary>
            public const string VietNamDate = "dd-MM-yyyy";
        }

        public class CLLogConstants
        {
            //level of exception
            /// <summary>
            /// Error
            /// </summary>
            public const string Error = "ERROR";

            /// <summary>
            /// Warning
            /// </summary>
            public const string Warn = "WARN";

            /// <summary>
            /// Info
            /// </summary>
            public const string Info = "INFO";
        }

        public class Message
        {
            public const string ActiveSuccessFormat = "Active {0} {1} is successful.";
            public const string ActiveFailFormat = "Active {0} {1} is un-successful.";
            public const string DeActiveSuccessFormat = "De-Active {0} {1} is successful.";
            public const string DeActiveFailFormat = "De-Active {0} {1} is un-successful.";
            public const string DeleteSuccessFormat = "Active {0} {1} is successful.";
            public const string DeleteFailFormat = "Active {0} {1} is un-successful.";
            public const string AddSuccessFormat = "Add {0} {1} is successful.";
            public const string AddFailFormat = "Add {0} {1} is un-successful.";
            public const string EditSuccessFormat = "Edit {0} {1} is successful.";
            public const string EditFailFormat = "Edit {0} {1} is un-successful.";
            public const string ErrorOccur = "Error occur, please try again!";
            public const string Oops = "Oops!! Please fill all the required fields and then try agains.";
        }

        public class Page
        {
            public const string Category = "Category";
            public const string Resource = "Resource";
        }
    }
}
