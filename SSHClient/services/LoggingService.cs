/*
 * File: LoggingService.cs
 * Purpose: Provides logging functionality for the SSHClient application.
 * Author: Owen Dyer
 * Created: 2025-12-18
 */

using SSHClient.Logging;
using System;
using System.IO;

namespace SSHClient.Services
{
    public static class LoggingService
    {
        private static readonly string LogFilePath = Path.Combine(AppContext.BaseDirectory, "logs", "application.log");

        /// <summary>
        /// Writes a log entry with the specified severity level, message, and optional exception details.
        /// </summary>
        /// <param name="level">The severity level of the log entry. Determines how the message is categorized and filtered.</param>
        /// <param name="message">The message to log. Provides context or information about the event being logged.</param>
        /// <param name="ex">An optional exception to include with the log entry. If not <see langword="null"/>, exception details are appended to the
        /// message.</param>
        public static void Log(LogLevel level, string message, Exception? ex)
        {
            var finalMessage = ex == null ? message : $"{message} | {ex}";
            Write(level, finalMessage);
        }

        /// <summary>
        /// Logs an informational message to the application's logging system.
        /// </summary>
        /// <remarks>Use this method to record general information about application execution, such as
        /// status updates or routine operations. Informational messages are typically used for tracking normal workflow
        /// and do not indicate errors or warnings.</remarks>
        /// <param name="message">The message to log. This value cannot be null.</param>
        public static void Info(string message) =>
            Log(LogLevel.Info, message, null);

        /// <summary>
        /// Logs a warning message to the application's logging system.
        /// </summary>
        /// <param name="message">The message to log. This should describe the warning condition or event.</param>
        public static void Warning(string message) =>
            Log(LogLevel.Warning, message, null);

        /// <summary>
        /// Logs an error message and associated exception at the error level.
        /// </summary>
        /// <param name="message">The error message to log. This should describe the error condition or context.</param>
        /// <param name="ex">The exception related to the error, or <see langword="null"/> if no exception is available.</param>
        public static void Error(string message, Exception? ex) =>
            Log(LogLevel.Error, message, ex);

        /// <summary>
        /// Writes a log entry with the specified log level and message to the configured log file.
        /// </summary>
        /// <remarks>If the log file or its directory cannot be created or written to, the method
        /// suppresses any exceptions and does not log the message. Logging failures do not affect application
        /// execution.</remarks>
        /// <param name="level">The severity level of the log entry. Determines how the message is categorized in the log.</param>
        /// <param name="message">The message to be written to the log. This should describe the event or information to record.</param>
        private static void Write(LogLevel level, string message)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath)!);
                Console.WriteLine(LogFilePath);

                var logLine =
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level.ToString().ToUpper()}] {message}";

                File.AppendAllText(LogFilePath, logLine + Environment.NewLine);
            }
            catch
            {
                // Nothing, prevent app from crashing because logging failed
            }
        }
    }
}