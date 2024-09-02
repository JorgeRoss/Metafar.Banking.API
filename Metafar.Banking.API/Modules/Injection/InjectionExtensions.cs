using Metafar.Banking.API.Modules.GlobalException;

namespace Metafar.Banking.API.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<GlobalExceptionHandler>();

            return services;
        }
    }
}
