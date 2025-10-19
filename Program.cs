using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement5Good;
using StudentManagement5Good.Winform;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.Services;

namespace StudentManagement5GoodTempp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Configure services
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);

            // Build service provider
            var serviceProvider = services.BuildServiceProvider();

            // Get the main form from DI container
            var mainForm = serviceProvider.GetRequiredService<Login>();

            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add Entity Framework DbContext
            services.AddDbContext<StudentManagementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Add services
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();

            // Add forms
            services.AddTransient<Login>();
            services.AddTransient<UserDashboard>();
            services.AddTransient<StudentDashboard>();

            // Add other services as needed
            // services.AddScoped<IOtherService, OtherService>();
        }
    }
}