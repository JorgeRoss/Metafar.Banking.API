using Metafar.Banking.API.Modules.Authentication;
using Metafar.Banking.API.Modules.Feature;
using Metafar.Banking.API.Modules.Injection;
using Metafar.Banking.Persistence;
using Metafar.Banking.Application.UseCases;
using Asp.Versioning.ApiExplorer;
using Metafar.Banking.API.Modules.Versioning;
using Metafar.Banking.API.Modules.Middleware;
using Metafar.Banking.API.Modules.Swagger;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddFeature(builder.Configuration);
        builder.Services.AddControllers().AddNewtonsoftJson();
        builder.Services.AddApplicationServices();
        builder.Services.AddInjection(builder.Configuration);
        builder.Services.AddAuthentication(builder.Configuration);
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddVersioning();
        builder.Services.AddSwagger();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            app.UseReDoc(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.DocumentTitle = "Metafar API Library";
                    options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
                }
            });
        }

        app.UseHttpsRedirection();
        app.UseCors("policyApi");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.AddMiddleware();

        app.Run();
    }
}

public partial class Program { };