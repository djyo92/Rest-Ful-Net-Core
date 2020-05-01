using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Services
{
    public class WriteToFileHostedServices:IHostedService,IDisposable
    {
        private readonly IWebHostEnvironment environment;
        private readonly string filename="File 1.txt";
        private Timer timer;
        public WriteToFileHostedServices(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedServices: Process started");
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }
        private void DoWork(object state) 
        {
            WriteToFile("WriteToFileHostedServices: Doingsome work at "+DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedServices: Process Stoped");
            timer?.Change(Timeout.Infinite,0);
            return Task.CompletedTask;
        }

        private void WriteToFile(string message)
        {
            var path = $@"{environment.ContentRootPath}\generatefiles\{filename}";
            using (StreamWriter write = new StreamWriter(path,append:true)) 
            {
                write.WriteLine(message);
            }
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
