using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiPrimerWebApiM3.DataContext;
using MiPrimerWebApiM3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Services
{
    public class ConsumeScopeService:IHostedService,IDisposable
    {
        private readonly string filename = "File 1.txt";
        private readonly IServiceProvider service;
        private Timer timer;
        public ConsumeScopeService(IServiceProvider service)
        {
            this.service = service;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            using (var scope = service.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();
                var message = "ConsumeScopeService. Resive a message at "+ DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                var log = new HostedServiceLog() { Message = message };
                context.HostedServiceLogs.Add(log);
                context.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }


        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
