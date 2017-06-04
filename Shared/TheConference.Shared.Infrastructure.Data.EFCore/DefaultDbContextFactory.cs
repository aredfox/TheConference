using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheConference.Shared.Infrastructure.Data.EFCore
{
    public abstract class DefaultDbContextFactory<T> : IDbContextFactory<T>
        where T : DbContext, new()
    {
        public T Create() {
            var environmentName = Environment.GetEnvironmentVariable("Hosting:Environment");
            var basePath = AppContext.BaseDirectory;
            return Create(basePath, environmentName);
        }

        public T Create(DbContextFactoryOptions options) {
            return Create(options.ContentRootPath, options.EnvironmentName);
        }

        private T Create(string basePath, string environmentName) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var connstr = config.GetConnectionString("(default)");

            if (String.IsNullOrWhiteSpace(connstr) == true)
            {
                throw new InvalidOperationException(
                "Could not find a connection string named '(default)'.");
            } else
            {
                return Create(connstr);
            }
        }

        private T Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<T>();

            optionsBuilder.UseSqlServer(connectionString);

            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);            
        }
    }
}
