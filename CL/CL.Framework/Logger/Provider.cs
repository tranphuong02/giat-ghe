using log4net;
using log4net.Config;
using System;
using System.Diagnostics;

namespace CL.Framework.Logger
{
    public class Provider
    {
        #region Fields

        private static volatile Provider _instance;
        private static ILog _logger = LogManager.GetLogger(typeof(Provider));

        #endregion Fields

        #region Instance

        /// <summary>
        ///     Singleton initialization
        /// </summary>
        public static Provider Instance
        {
            get { return _instance ?? (_instance = new Provider()); }
        }

        #endregion Instance

        #region Logging

        /// <summary>
        ///     Write message to log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        public void WriteLog(string message, Enums.LogLevels logLevel = Enums.LogLevels.Info)
        {
            switch (logLevel)
            {
                case Enums.LogLevels.Debug:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Debug;
                    }
                    break;

                case Enums.LogLevels.Info:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Info;
                    }
                    break;

                case Enums.LogLevels.Warn:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Warn;
                    }
                    break;

                case Enums.LogLevels.Error:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Error;
                    }
                    break;

                case Enums.LogLevels.Fatal:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Fatal;
                    }
                    break;
            }
            _logger = LogManager.GetLogger(GetCaller());
            XmlConfigurator.Configure();
            switch (logLevel)
            {
                case Enums.LogLevels.Debug:
                    {
                        _logger.Debug(message);
                    }
                    break;

                case Enums.LogLevels.Info:
                    {
                        _logger.Info(message);
                    }
                    break;

                case Enums.LogLevels.Warn:
                    {
                        _logger.Warn(message);
                    }
                    break;

                case Enums.LogLevels.Error:
                    {
                        _logger.Error(message);
                    }
                    break;

                case Enums.LogLevels.Fatal:
                    {
                        _logger.Fatal(message);
                    }
                    break;
            }

            LogManager.GetRepository().Shutdown();
        }

        /// <summary>
        ///     Write log to log file
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="logLevel"></param>
        public void WriteLog(Exception ex, Enums.LogLevels logLevel)
        {
            switch (logLevel)
            {
                case Enums.LogLevels.Debug:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Debug;
                    }
                    break;

                case Enums.LogLevels.Info:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Info;
                    }
                    break;

                case Enums.LogLevels.Warn:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Warn;
                    }
                    break;

                case Enums.LogLevels.Error:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Error;
                    }
                    break;

                case Enums.LogLevels.Fatal:
                    {
                        ThreadContext.Properties["logPath"] = Constant.Paths.Fatal;
                    }
                    break;
            }
            _logger = LogManager.GetLogger(GetCaller());
            XmlConfigurator.Configure();
            switch (logLevel)
            {
                case Enums.LogLevels.Debug:
                    {
                        _logger.Debug(ex);
                    }
                    break;

                case Enums.LogLevels.Info:
                    {
                        _logger.Info(ex);
                    }
                    break;

                case Enums.LogLevels.Warn:
                    {
                        _logger.Warn(ex);
                    }
                    break;

                case Enums.LogLevels.Error:
                    {
                        _logger.Error(ex);
                    }
                    break;

                case Enums.LogLevels.Fatal:
                    {
                        _logger.Fatal(ex);
                    }
                    break;
            }

            LogManager.GetRepository().Shutdown();
        }

        /// <summary>
        ///     Write message to log debug
        /// </summary>
        /// <param name="data"></param>
        public void LogDebug(object data)
        {
            ThreadContext.Properties["logPath"] = Enums.LogLevels.Debug;
            _logger = LogManager.GetLogger(GetCaller());
            XmlConfigurator.Configure();
            _logger.Debug(data);
            LogManager.GetRepository().Shutdown();
        }

        /// <summary>
        ///     Write log exception to log file
        /// </summary>
        /// <param name="ex"></param>
        public void LogError(Exception ex)
        {
            ThreadContext.Properties["logPath"] = Constant.Paths.Error;
            _logger = LogManager.GetLogger(GetCaller());
            XmlConfigurator.Configure();
            _logger.Error(ex);
            LogManager.GetRepository().Shutdown();
        }

        /// <summary>
        ///     Write log message to log file
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            ThreadContext.Properties["logPath"] = Constant.Paths.Error;
            _logger = LogManager.GetLogger(GetCaller());
            XmlConfigurator.Configure();
            _logger.Error(message);
            LogManager.GetRepository().Shutdown();
        }

        #endregion Logging

        #region Private Method

        /// <summary>
        ///     Get caller type
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private static Type GetCaller(int level = 2)
        {
            return new StackTrace().GetFrame(level).GetMethod().DeclaringType;
        }

        #endregion Private Method
    }
}
