using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebHookSubscriber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("BASTA! Spring 2018 Demonstration");
            Console.WriteLine("WebHook - SUBSCRIBER");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("================================");
            

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((log) => { log.ClearProviders(); })
                .UseStartup<Startup>()
                .UseUrls("http://+:9000")
                .Build();
    }
}
