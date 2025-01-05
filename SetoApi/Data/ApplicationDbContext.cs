using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SetoClass.Models;

namespace SetoApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<ResumeSkill> ResumeSkills { get; set; }
        public DbSet<ResumeSocial> ResumeSocials { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ResumeSkill>()
                .HasOne(rs => rs.Resume)
                .WithMany(r => r.Skills)
                .HasForeignKey(rs => rs.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ResumeSocial>()
                .HasOne(rs => rs.Resume)
                .WithMany(r => r.Socials)
                .HasForeignKey(rs => rs.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}