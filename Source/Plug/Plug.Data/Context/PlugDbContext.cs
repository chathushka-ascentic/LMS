using Microsoft.EntityFrameworkCore;
using Plug.Entities;

namespace Plug.Data
{
    /// <summary>
    /// Db Context Class
    /// </summary>
    public class PlugDbContext : DbContext
    {
        // Do a direct migration need to have a connection string
        #region Constant

        const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=PlugDB;Trusted_Connection=True;";

        #endregion

        #region DBSets

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Choice> Choices { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<EnrollModule> EnrollModules { get; set; }

        #endregion

        #region Constructor

        public PlugDbContext()
            :base(new DbContextOptionsBuilder<PlugDbContext>().UseSqlServer(ConnectionString).Options)
        {

        }

        public PlugDbContext(DbContextOptions<PlugDbContext> options)
            : base(options)
        {
        }

        #endregion

        #region Overrides

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Text>().ToTable("TextModules");
            modelBuilder.Entity<Video>().ToTable("VideoModules");
            modelBuilder.Entity<Question>().ToTable("QuestionModules");
            modelBuilder.Entity<EnrollModule>().HasKey(t => new { t.EnrollmentId, t.ModuleId });
            modelBuilder.Entity<EnrollModule>().HasOne(u => u.Enrollment).WithMany(u => u.EnrollModules).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }

        #endregion
    }
}
