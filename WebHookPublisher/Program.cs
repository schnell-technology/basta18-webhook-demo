using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebHookPublisher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("BASTA! Spring 2018 Demonstration");
            Console.WriteLine("WebHook - PUBLISHER");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("================================");
            

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((log) => { log.ClearProviders(); })
                .UseStartup<Startup>()
                .UseUrls("http://+:3000")
                .Build();
    }
}
