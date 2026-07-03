using PuzzleCircuit.API.Services;

namespace PuzzleCircuit.API {
    internal static class ServiceRegistration {
        public static IServiceCollection RegisterAppServices(this IServiceCollection services) {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
