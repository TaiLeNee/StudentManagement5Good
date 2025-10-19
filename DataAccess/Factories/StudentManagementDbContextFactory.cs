using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudentManagement5GoodTempp.DataAccess.Context;

namespace StudentManagement5GoodTempp.DataAccess.Factories
{
    public class StudentManagementDbContextFactory : IDesignTimeDbContextFactory<StudentManagementDbContext>
    {
        public StudentManagementDbContext CreateDbContext(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Create DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<StudentManagementDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new StudentManagementDbContext(optionsBuilder.Options);
        }
    }
}