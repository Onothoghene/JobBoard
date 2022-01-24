using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JobBoard.Models
{
    public class JobBoardContext : DbContext
    {
        public JobBoardContext()
        {
        }

        public JobBoardContext(DbContextOptions<JobBoardContext> options) : base(options)
        {
        }

        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<JobType> JobTypes { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<SavedJobs> SavedJobs { get; set; }
        public DbSet<JobApplication> JobApplications{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                               .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json");
                IConfiguration Configuration = builder.Build();
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
