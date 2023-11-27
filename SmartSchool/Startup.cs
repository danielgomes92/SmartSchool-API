using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartSchool.Data;
using System;
using System.IO;
using System.Reflection;

namespace SmartSchool
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<SmartContext>(
				context => context.UseSqlite(Configuration.GetConnectionString("Default")));

			services.AddControllers()
				.AddNewtonsoftJson(option =>
				option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // busca dentro do assembly quem está herdando os profiles.

			services.AddScoped<IRepository, Repository>();

			services.AddApiVersioning(opptions =>
			{
				opptions.DefaultApiVersion = ApiVersion.Default; //igual ao  Default que é equivalente a ApiVersion(1,0)
				opptions.AssumeDefaultVersionWhenUnspecified = true; //permite que a API assuma o valor padrão caso isso não for especificado;
				opptions.ReportApiVersions = true; //envia as versões disponíveis para a API em todas as respostas na forma de um cabeçalho: "api-supported-versions".
				opptions.ApiVersionReader = new UrlSegmentApiVersionReader();
			});

			services.AddSwaggerGen(
				options =>
				{
					options.SwaggerDoc("SmartschoolAPI", new OpenApiInfo()
					{
						Title = "SmartSchool API",
						Version = "1.0",
						Description = "Curso de criação Web API com .NET 6 + EF Core + Docker " +
						"com o intuito de aderir conhecimentos práticos e esclarecimento teórico."
					});

					var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
					var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

					options.IncludeXmlComments(xmlCommentsFullPath);
				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseSwagger()
				.UseSwaggerUI(options =>
				{
					options.SwaggerEndpoint($"/swagger/{}/swagger.json", "smartschool".ToUpperInvariant());
					options.RoutePrefix = "";
				});

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}