using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace D1yuan.InjectionLogger
{
}
    ///// <summary>
    ///// 单例模式--读取配置文件--从配置文件中反射，并自动监控改变
    ///// 
    ///// </summary>
    //public class GetConfigLogger
    //{

    //    #region 单例
    //    static GetConfigLogger instance = null;
    //    private static readonly object instancelock = new object();

    //    public GetConfigLogger()
    //    {
    //    }

    //    public static GetConfigLogger Instance
    //    {

    //        get
    //        {


    //            if (instance == null)

    //            {
    //                lock (instancelock)
    //                {
    //                    if (instance == null)
    //                    {

    //                        instance = new GetConfigLogger();
    //                    }


    //                }
    //            }

    //            return instance;


    //        }
    //    }
    //    #endregion


    //    //配置项 
    //    public bool isConsle = false;
    //    public bool isFile = false;
    //    public LogLevel logLevel = LogLevel.Debug;




    //    public LoggingConfig loggingConfig;



    //    private DateTime prevChange = DateTime.MinValue;
    //    //载入 
    //    public void InitLoad()
    //    {
    //        Debug.Assert(paths?.Length > 0, "失败！没载入配置文件");
    //        Trace.Assert(paths?.Length > 0, "失败！没载入配置文件");

    //        //加载配置
    //        var builder = new ConfigurationBuilder(); 
    //        builder.AddJsonFile("LogSetting.json", optional: true, reloadOnChange: true); 

    //        var config = builder.Build();
    //        Deal(config);
    //        //变更处理--系统异步IO对于文件的监听可能触发2次，500毫秒内只计算一次
    //        ChangeToken.OnChange(() => { return config.GetReloadToken(); }, (obj) =>
    //        {
    //            var now = DateTime.Now;
    //            if ((now - prevChange).TotalMilliseconds > 500)
    //            {
    //                prevChange = now;
    //                var configReload = (IConfiguration)obj;
    //                try
    //                {
    //                        //处理 
    //                        Deal(configReload);
    //                    PrintfLoad();
    //                }
    //                catch (Exception ex)
    //                {
    //                    Debug.Assert(false, ex.Message);
    //                    Trace.Assert(false, ex.Message);
    //                }
    //            }
    //        }, config);
    //        GetConfigLogger.Instance.BeginPrintf();
    //    }

    //    /// <summary>
    //    /// 需要处理的文件
    //    /// </summary>
    //    /// <param name="config"></param>
    //    private void Deal(IConfiguration config)
    //    {

    //        loggingConfig = config.GetRequiredSection("Logging").Get<LoggingConfig>(); 
    //        GetConfigLogger.Instance.SetLoggerConfig(loggerConfig);
    //    }


    //    public void PrintfLoad()
    //    {
    //        Log.PrintfInformation($"配置载入--RuntimeConfig: {JsonConvert.SerializeObject(runtimeConfig)}");
    //        Log.PrintfDebug($"配置载入--TradePairConfig: {JsonConvert.SerializeObject(tradePairConfig)}");
    //        Log.PrintfDebug($"配置载入--GoodsMoneyConfig: {JsonConvert.SerializeObject(goodsMoneyConfig)}");
    //        Log.PrintfDebug($"配置载入--EveryTimeConfig: {JsonConvert.SerializeObject(everyTimeConfig)}");
    //        Log.PrintfDebug($"配置载入--OrderConfig: {JsonConvert.SerializeObject(orderConfig)}");
    //        Log.PrintfDebug($"配置载入--ApiKeyConfig: {JsonConvert.SerializeObject(apiKeyConfig)}");
    //        Log.PrintfInformation($"配置载入--LoggerConfig: {JsonConvert.SerializeObject(loggerConfig)}");
    //        Log.PrintfDebug($"配置载入--DebugParam: {JsonConvert.SerializeObject(debugParam)}");
    //    }
