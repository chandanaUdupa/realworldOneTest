using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace realworldOneTest.Utility
{

    public interface ILoggable
    {
        DiagnosticLog.ApplicationModules MyModule { get; }
        string GetCurrentUser { get; }
        DiagnosticLog.LogType LogType { get; }
    }

    public static class LogExtension
    {

        public static void realWorldOneTestBegin(this ILogger log, string message, [CallerMemberName] string caller = "Unknow caller")
        {
            string header = $"-------- { caller }--------";
            log.realWorldOneTestSingleLogInfo(header);
            log.realWorldOneTestSingleLogInfo(message);
        }

        public static void realWorldOneTestEnd(this ILogger log, string message, [CallerMemberName] string caller = "Unknow caller")
        {
            string footer = $"-------- { caller }--------";
            log.realWorldOneTestSingleLogInfo(message);
            log.realWorldOneTestSingleLogInfo(footer);
        }



        public static void realWorldOneTestSingleLogInfo(this ILogger log, string message)
        {
            log.LogInformation("");
            log.LogInformation("-- realWorldOneTest -- " + message);
            log.LogInformation("");
        }

        public static void realWorldOneTestSingleLogError(this ILogger log, string message)
        {
            log.LogError("");
            log.LogError("-- realWorldOneTest -- " + message);
            log.LogError("");
        }

        public static void realWorldOneTestSingleLogWarning(this ILogger log, string message)
        {
            log.LogWarning("");
            log.LogWarning("-- realWorldOneTest -- " + message);
            log.LogWarning("");
        }
    }

    public static class DiagnosticLog
    {
        public enum ApplicationModules : long
        {
            Writeback = 0b1_0000_0000_0000_0000_0000_0000_0000_0001
        }

        public enum LogType
        {
            Debug,
            Performance,
            Both
        }

        private static long appToTrace;


        public static void TraceModule(ApplicationModules module)
        {
            appToTrace |= (long)module;
        }

        public static void TraceModule(long modules)
        {
            appToTrace = modules;
        }

        public static void Debug(string message, ILoggable loggable)
        {
            if ((appToTrace & (long)(loggable.MyModule)) != 0L)
            {
                if (loggable.LogType == LogType.Debug || loggable.LogType == LogType.Both)
                {
                    Trace.Write($"User: {loggable.GetCurrentUser}" + message);
                }
            }
        }

        public static void Performance(string message, ILoggable loggable)
        {
            if ((appToTrace & (long)(loggable.MyModule)) != 0L)
            {
                if (loggable.LogType == LogType.Performance || loggable.LogType == LogType.Both)
                {
                    Trace.Write($"User: {loggable.GetCurrentUser}" + message);
                }
            }
        }

    }
}

