namespace CL.Transverse
{
    public static class Constants
    {
        public static class GeneralAdminConfigs
        {
            public const string AdminArea = "Administrator";
            public const string Layout = "~/Areas/Administrator/Views/Shared/_Admin.cshtml";
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
    }
}
