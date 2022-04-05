using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace D1yuan.InjectionLogger
{
    /// <summary>
    /// 即打印Console，也打印日志。
    /// 日志等级使用Microsoft.Extensions.Logging
    /// </summary>
    public static class AllLog
    {
        public static void Printf(string outStr)
        {
            if (String.IsNullOrWhiteSpace(outStr))
            {
                string text = $"{DateTime.Now.GetDateTimeString() }:{outStr}";
                CLog.Instance.EnqueueMessage(text);
                FLog.Instance.EnqueueMessage(text);
            }
        }

        //public static void PrintfTrace(string outStr)
        //{
        //    if (GetConfigLogger.Instance.loggerConfig.GetLevel() <= LogLevel.Trace || String.IsNullOrWhiteSpace(outStr))
        //    {
        //        string text = $"{DateTime.Now.GetDateTimeString() }Trace调试:{outStr}";
        //        CLog.Instance.EnqueueMessage(text);
        //        FLog.Instance.EnqueueMessage(text);
        //    }
        //}
        //public static void PrintfDebug(string outStr)
        //{
        //    if (GetConfigLogger.Instance.loggerConfig.GetLevel() <= LogLevel.Debug || String.IsNullOrWhiteSpace(outStr))
        //    {
        //        string text = $"{DateTime.Now.GetDateTimeString() }Debug调试:{outStr}";
        //        CLog.Instance.EnqueueMessage(text);
        //        FLog.Instance.EnqueueMessage(text);
        //    }
        //}

        //public static void PrintfInformation(string outStr)
        //{
        //    if (GetConfigLogger.Instance.loggerConfig.GetLevel() <= LogLevel.Information || String.IsNullOrWhiteSpace(outStr))
        //    {
        //        string text = $"{DateTime.Now.GetDateTimeString()} Information信息:{outStr}";
        //        CLog.Instance.EnqueueMessage(text);
        //        FLog.Instance.EnqueueMessage(text);
        //    }
        //}

        //public static void PrintfWarning(string outStr)
        //{
        //    if (GetConfigLogger.Instance.loggerConfig.GetLevel() <= LogLevel.Warning || String.IsNullOrWhiteSpace(outStr))
        //    {
        //        string text = $"{DateTime.Now.GetDateTimeString()} Warning警告:{outStr}";
        //        CLog.Instance.EnqueueMessage(text);
        //        FLog.Instance.EnqueueMessage(text);
        //    }
        //}

        //public static void PrintfError(string outStr)
        //{
        //    if (GetConfigLogger.Instance.loggerConfig.GetLevel() <= LogLevel.Error || String.IsNullOrWhiteSpace(outStr))
        //    {
        //        string text = $"{DateTime.Now.GetDateTimeString()} Error错误--错误--错误:{outStr}";
        //        CLog.Instance.EnqueueMessage(text);
        //        FLog.Instance.EnqueueMessage(text);
        //    }
        //}
        //public static void PrintfFatal(string outStr)
        //{
        //    if (GetConfigLogger.Instance.loggerConfig.GetLevel() <= LogLevel.Critical || String.IsNullOrWhiteSpace(outStr))
        //    {
        //        string text = $"{DateTime.Now.GetDateTimeString() }Fatal严重错误:{outStr}";
        //        CLog.Instance.EnqueueMessage(text);
        //        FLog.Instance.EnqueueMessage(text);
        //    }
        //}

        public static string GetDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        public static string GetDateTimePathString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmmssfff");
        }

        //public static string GetFloatString(this Double doubItem)
        //{ 

        //}


    }



    //Trace	0	
    //包含最详细消息的日志。 这些消息可能包含敏感应用程序数据。 这些消息默认情况下处于禁用状态，并且绝不应在生产环境中启用。

    //Debug	1	
    //在开发过程中用于交互式调查的日志。 这些日志应主要包含对调试有用的信息，并且没有长期价值。

    //Information	2	
    //跟踪应用程序的常规流的日志。 这些日志应具有长期价值。

    //Warning	3	
    //突出显示应用程序流中的异常或意外事件（不会导致应用程序执行停止）的日志。

    //Error	4	
    //当前执行流因故障而停止时突出显示的日志。 这些日志指示当前活动中的故障，而不是应用程序范围内的故障。

    //Critical	5	
    //描述不可恢复的应用程序/系统崩溃或需要立即引起注意的灾难性故障的日志。

    //None	6	
    //不用于写入日志消息。 指定日志记录类别不应写入任何消息。




}

