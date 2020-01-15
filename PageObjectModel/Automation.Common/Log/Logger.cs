using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Common.Log
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Logger));

        /// <summary>
        /// Logs message as debug. If message is a format string, then replaces the format item in a specified string representation of corresponding object in the specified array @args
        /// </summary>
        /// <param name="message">message format</param>
        /// <param name="args">format items</param>
        public static void Debug(string message, params object[] args)
        {
            var msg = args.Any() ? String.Format(message, args) : message;
            Console.WriteLine(msg);
            log.Debug(msg);
        }

        /// <summary>
        /// Logs message as debug. If message is a format string, then replaces the format item in a specified string representation of corresponding object in the specified array @args
        /// Add section divider before and after message with given @separator and @repeat count
        /// </summary>
        /// <param name="message"></param>
        /// <param name="separator"></param>
        /// <param name="repeat"></param>
        /// <param name="args"></param>
        public static void Debug(string message, char separator, int repeat = 40, params object[] args)
        {
            var sectionDivider = new string(separator, repeat); ;
            Debug("");
            Debug(sectionDivider);
            Debug(message, args);
            Debug(sectionDivider);
        }

        /// <summary>
        /// Logs message as error. If message is a format string, then replaces the format item in a specified string representation of corresponding object in the specified array @args
        /// </summary>
        /// <param name="message">message format</param>
        /// <param name="args">format items</param>
        public static void Error(string message, params object[] args)
        {
            var msg = args.Any() ? string.Format(message, args) : message;
            Console.WriteLine("Error: " + msg);
            log.Error(msg);
        }

        /// <summary>
        /// Logs message as fatal. If message is a format string, then replaces the format item in a specified string representation of corresponding object in the specified array @args
        /// </summary>
        /// <param name="message">message format</param>
        /// <param name="args">format items</param>
        public static void Fatal(string message, params object[] args)
        {
            var msg = string.Format(message, args);
            Console.WriteLine(msg);
            log.Fatal(msg);
        }

        /// <summary>
        /// Logs message as info. If message is a format string, then replaces the format item in a specified string representation of corresponding object in the specified array @args
        /// </summary>
        /// <param name="message">message format</param>
        /// <param name="args">format items</param>
        public static void Info(string message, params object[] args)
        {
            var msg = string.Format(message, args);
            LogToConsoleWithTimeStamp(msg);
            log.Info(msg);
        }

        /// <summary>
        /// Logs message. If message is a format string, then replaces the format item in a specified string representation of corresponding object in the specified array @args
        /// </summary>
        /// <param name="message">message format</param>
        /// <param name="args">format items</param>
        public static void Log(string message, params object[] args)
        {
            Debug(message, args);
        }

        /// <summary>
        /// Log user actions performed by the test. If message is a format string, then replaces the format item in a specified string representation of corresponding object in the specified array @args
        /// </summary>
        /// <param name="message">message format</param>
        /// <param name="args">format items</param>
        public static void LogExecute(string message, params object[] args)
        {
            var executeMsg = $"EXECUTE: {message}";
            Info(executeMsg, args);
        }

        /// <summary>
        /// Log user actions performed by the test. If message is a format string, then replaces the format item in a specified string representation of corresponding object in the specified array @args
        /// </summary>
        /// <param name="message">message format</param>
        /// <param name="args">format items</param>
        public static void LogValidate(string message, params object[] args)
        {
            var validateMsg = $"VALIDATE: {message}";
            Info(validateMsg, args);
        }

        /// <summary>
        /// Logs message as warn. If message is a format string, then replaces the format item in a specified string representation of corresponding object in the specified array @args
        /// </summary>
        /// <param name="message">message format</param>
        /// <param name="args">format items</param>
        public static void Warn(string message, params object[] args)
        {
            var msg = string.Format(message, args);
            Console.WriteLine(msg);
            log.Warn(msg);
        }

        /// <summary>
        /// Logs to console with time stamp.
        /// </summary>
        /// <param name="value">The value.</param>
        private static void LogToConsoleWithTimeStamp(string value)
        {
            Console.WriteLine("[{0}] {1}", DateTime.UtcNow, value);
        }

        /// <summary>
        /// Replaces the curly braces. Use it when your message contains curly braces
        /// </summary>
        /// <param name="testMessage">The test message.</param>
        /// <returns></returns>
        public static string ReplaceCurlyBraces(string testMessage)
        {
            var sb = new StringBuilder(testMessage);
            sb.Replace("{", "{{").Replace("}", "}}");
            return sb.ToString();
        }
    }
}
