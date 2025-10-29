using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppJob.Core.Services
{
    //public static class Runner
    //{
    //    static Runner() { }

    //    public  static async Task RegisterRequiredServices()
    //    {
    //        var configuration = new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetCurrentDirectory() + "../../../../../AppJob")
    //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //        .Build();

    //        // Set up dependency injection
    //        var serviceCollection = new ServiceCollection();
    //        serviceCollection.AddStorageEmailServices(configuration);
    //        await Task.CompletedTask;
    //    }
    //}
}
