using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SmartSchool.Data;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
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

			services.AddApiVersioning(setup =>
			{
				setup.DefaultApiVersion = new ApiVersion(1, 0); //igual ao  Default que é equivalente a ApiVersion(1,0)
				setup.AssumeDefaultVersionWhenUnspecified = true; //permite que a API assuma o valor padrão caso isso não for especificado;
				setup.ReportApiVersions = true; //envia as versões disponíveis para a API em todas as respostas na forma de um cabeçalho: "api-supported-versions".
				setup.ApiVersionReader = new UrlSegmentApiVersionReader();
			})
			.AddApiExplorer(setup =>
			{
				setup.GroupNameFormat = "'v'VVV";
				setup.SubstituteApiVersionInUrl = true;
			});

			var descriptions = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
			services.AddSwaggerGen(options =>
				{
                    foreach (var description in descriptions.ApiVersionDescriptions)
                    {
						options.SwaggerDoc(description.GroupName, new OpenApiInfo()
						{
							Title = "SmartSchool API",
							Version = description.ApiVersion.ToString(),
							Description = "Curso de criação Web API com .NET 6 + EF Core + Docker " +
							"com o intuito de aderir conhecimentos práticos e esclarecimento teórico."
						});
                    }

					var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
					var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

					options.IncludeXmlComments(xmlCommentsFullPath);
				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseSwagger();
			app.UseSwaggerUI(options =>
				{
					foreach (var description in provider.ApiVersionDescriptions)
					{
						options.SwaggerEndpoint(
							$"/swagger/{description.GroupName}/swagger.json",
							description.GroupName.ToUpperInvariant()
						);
					};
					options.RoutePrefix = string.Empty;
				});

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}