using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SetShapeDatabase
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SetShapeContext>
    {
        public SetShapeContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<SetShapeContext>();

            var connectionString = configuration.GetConnectionString("SetShapeContext");

            builder.UseSqlServer(connectionString);

            return new SetShapeContext(builder.Options);
        }
    }
}
