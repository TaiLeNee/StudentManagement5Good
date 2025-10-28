using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement5Good;
using StudentManagement5Good.Winform;
using StudentManagement5Good.Helpers;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.Services;
using System.Text;

namespace StudentManagement5GoodTempp
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Apply emergency thread fix to prevent cross-thread errors
            UserDashboardThreadSafePatch.EmergencyThreadFix();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Set application-wide font để hỗ trợ tiếng Việt tốt hơn
            Application.SetCompatibleTextRenderingDefault(false);

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Configure services
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);

            // Build service provider
            ServiceProvider = services.BuildServiceProvider();

            try
            {
                // Create and run the main login form directly
                var mainForm = new Login(ServiceProvider);
                
                // Apply Vietnamese fix sau khi form được tạo
                mainForm.Load += (sender, e) => 
                {

                };
                
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi động ứng dụng: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restore thread checks when application exits
                UserDashboardThreadSafePatch.RestoreThreadChecks();
            }
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add Entity Framework DbContext với connection pooling để tăng hiệu suất
            services.AddDbContextPool<StudentManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging(false);
                options.EnableServiceProviderCaching(true);
                options.EnableDetailedErrors(false);
            });

            // Add services
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();

            // Không cần register Login form vì chúng ta tạo trực tiếp
            // services.AddTransient<Login>();
            
            // Add other forms as transient to ensure fresh instances
            services.AddTransient<UserDashboard>();
            services.AddTransient<StudentDashboard>();
            // services.AddTransient<MinhChungApprovalForm>();
            services.AddTransient<MinhChungForm>();

            // Add other services as needed
            // services.AddScoped<IOtherService, OtherService>();
        }
    }
}