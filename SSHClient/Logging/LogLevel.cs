/*
 * File: LogLevel.cs
 * Purpose: Defines log levels for the SSHClient logging system.
 * Author: Owen Dyer
 * Created: 2025-12-18
 */

namespace SSHClient.Logging
{
    /// <summary>
    /// Enumeration of log levels for the SSHClient logging system.
    /// </summary>
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }
}