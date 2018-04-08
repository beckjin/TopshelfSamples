using System;
using System.Timers;
using Topshelf;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            // 配置和运行宿主服务
            HostFactory.Run(x =>
            {
                // 指定服务类型。这里设置为 CacheService
                x.Service<CacheService>(s =>
                {
                    // 通过 new CacheService() 构建一个服务实例 
                    s.ConstructUsing(name => new CacheService());
                    // 当服务启动后执行什么
                    s.WhenStarted(tc => tc.Start());
                    // 当服务停止后执行什么
                    s.WhenStopped(tc => tc.Stop());
                });

                // 服务用本地系统账号来运行
                x.RunAsLocalSystem();

                // 服务描述信息
                x.SetDescription("缓存服务");
                // 服务显示名称
                x.SetDisplayName("CacheService");
                // 服务名称
                x.SetServiceName("CacheService");

            });
        }
    }

    public class CacheService
    {
        //private readonly string host = ConfigurationManager.AppSettings["Host"];
        //private readonly string port = ConfigurationManager.AppSettings["Port"];

        //readonly Server server;
        //public CacheService()
        //{
        //    server = new Server
        //    {
        //        Services = { MDCache.BindService(new CacheServiceImpl()) },
        //        Ports = { new ServerPort(host, Convert.ToInt32(port), ServerCredentials.Insecure) }
        //    };
        //}
        //public void Start() { server.Start(); }
        //public void Stop() { server.ShutdownAsync(); }

        readonly Timer _timer;
        public CacheService()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
