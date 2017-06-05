using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TheConference.Shared.Infrastructure.Data.EFCore {
    public abstract class DefaultDbContextFactory<T> : IDbContextFactory<T>
        where T : DbContext, new() {
        public T Create(bool seed = false) {
            var environmentName = Environment.GetEnvironmentVariable("Hosting:Environment");
            var basePath = AppContext.BaseDirectory;
            return Create(basePath, environmentName, seed);
        }

        public T Create(DbContextFactoryOptions options) {
            return Create(options.ContentRootPath, options.EnvironmentName, false);
        }
        public T Create(DbContextFactoryOptions options, bool seed) {
            return Create(options.ContentRootPath, options.EnvironmentName, seed);
        }

        private T Create(string basePath, string environmentName, bool seed = false) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var connstr = config.GetConnectionString("(default)");

            if (String.IsNullOrWhiteSpace(connstr) == true) {
                throw new InvalidOperationException(
                "Could not find a connection string named '(default)'.");
            } else {
                return Create(connstr, seed);
            }
        }

        private T Create(string connectionString, bool seed = false) {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<T>();

            optionsBuilder.UseSqlServer(connectionString);

            var instance = (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);

            if (seed) {
                Seed();
            }

            return instance;
        }

        protected abstract void Seed();
    }
}
