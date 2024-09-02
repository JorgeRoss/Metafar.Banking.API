using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Metafar.Banking.API.Modules.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "Metafar",
                Description = "API Library. ",
                TermsOfService = new Uri("https://metafar.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Jorge Ross",
                    Email = "jorge.ross@hotmail.com",
                    Url = new Uri("https://metafar.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Licence",
                    Url = new Uri("https://metafar.com/licence")
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += "API Version Deprecated.";
            }

            return info;
        }
    }
}
