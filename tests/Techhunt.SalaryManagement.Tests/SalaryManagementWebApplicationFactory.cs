using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using Techhunt.SalaryManagement.Application;
using Techhunt.SalaryManagement.Infrastructure.Persistance;

namespace Techhunt.SalaryManagement.Tests
{
    public class SalaryManagementWebApplicationFactory<TStartup> 
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        private static readonly object padlock = new object();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SalaryManagementDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<SalaryManagementDbContext>(options =>
                {
                    options.UseInMemoryDatabase("SalaryManagement");
                }, ServiceLifetime.Singleton);

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<SalaryManagementDbContext>();
                    if (!dbContext.Employees.Any())
                    {
                        lock (padlock)
                        {
                            if (!dbContext.Employees.Any())
                            {
                                var csvMapper = scopedServices.GetRequiredService<ICsvMapper>();

                                dbContext.Database.EnsureCreated();
                                var path = Path.Combine("CsvFiles", "seed-data.csv");
                                using (var csvFile = File.OpenRead(path))
                                using (var stream = new MemoryStream())
                                {
                                    csvFile.CopyTo(stream);
                                    var employees = csvMapper.GetEmployees(stream).Select(e => new EmployeeDbModel(e));
                                    dbContext.Employees.AddRange(employees);
                                    dbContext.SaveChanges();
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}
