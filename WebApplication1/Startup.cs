using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.OpenApi.Models;
using WebApplication1.Controllers;
using Autofac.Extras.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using ClassLibrary;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace WebApplication1
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public ILifetimeScope AutofacContainer { get; private set; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().AddControllersAsServices();
			services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "PSIProject", Version = "v1" });
			});
			services.AddDbContext<ProjectDatabaseContext>(options =>
			options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			builder.RegisterType<AccountsController>().EnableClassInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<ProfilesController>().EnableClassInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<AdvertisementsController>().EnableClassInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<DistributorController>().EnableClassInterceptors().InterceptedBy(typeof(LoggingInterceptor));
			builder.RegisterType<RatingController>().EnableClassInterceptors().InterceptedBy(typeof(LoggingInterceptor));
            builder.RegisterType<LoggingInterceptor>().SingleInstance();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PSIProject v1"));
			}

			this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

			app.UseMiddleware<ExceptionLoggingMiddleware>();

			app.UseExceptionHandler("/error");

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
