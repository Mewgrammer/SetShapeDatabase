using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.CodeGeneration.TypeScript;

namespace SetShapeDatabase
{
    public class Startup
    {

        private const string ConfigFileName = "--appsettings";
        private const string DefaultConfigFileName = "appsettings";

        public Startup(IHostingEnvironment env)
        {
            var configName = Environment.GetCommandLineArgs().FirstOrDefault(a => a.StartsWith(ConfigFileName))?.Split('=')?.Skip(1)?.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(configName))
                configName = DefaultConfigFileName;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"{configName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configName}.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var connectionString = Configuration.GetConnectionString("SetShapeContext") ?? "Server=.\\SQLEXPRESS;Database=SetShape;Trusted_Connection=True;";
            services.AddDbContext<SetShapeContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddSwaggerDocument();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
            });


            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseMvc();
            GenerateTypescriptApiClient();

        }

        private async void GenerateTypescriptApiClient()
        {
            try
            {
                var document = await SwaggerDocument.FromUrlAsync("http://localhost:30000/swagger/v1/swagger.json");
                var settings = new SwaggerToTypeScriptClientGeneratorSettings
                {
                    ClassName = "{controller}Client",
                    GenerateClientClasses = true,
                    GenerateOptionalParameters = true,
                    GenerateResponseClasses = true,
                };

                var generator = new SwaggerToTypeScriptClientGenerator(document, settings);
                var code = generator.GenerateFile();

                File.WriteAllText("TypescriptApiClient.ts", code);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
