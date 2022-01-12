using Microsoft.Extensions.DependencyInjection;
using PortalApp.Services;
using PortalApp.Services.Interface;

namespace PortalApp
{
    public static class ServiceRegister
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IExamService, ExamService>();
        }
    }
}
