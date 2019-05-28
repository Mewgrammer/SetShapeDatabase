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
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<SetShapeContext>();

            var connectionString = configuration.GetConnectionString("SetShapeContext") ?? "Server=db;Database=SetShape;User=sa;Password=secret_password2019;Trusted_Connection=True;";

            builder.UseSqlServer(connectionString);

            return new SetShapeContext(builder.Options);
        }
    }
}
