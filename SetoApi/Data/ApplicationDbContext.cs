using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SetoClass.Models;

namespace SetoApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
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

            // Identity tablo yapılarını da oluşturmamız için bu satır önemli
            base.OnModelCreating(modelBuilder);

            // Resume -> ResumeSkill (1:N)
            modelBuilder.Entity<ResumeSkill>()
                .HasOne(rs => rs.Resume)
                .WithMany(r => r.Skills)
                .HasForeignKey(rs => rs.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Resume -> ResumeSocial (1:N)
            modelBuilder.Entity<ResumeSocial>()
                .HasOne(rs => rs.Resume)
                .WithMany(r => r.Socials)
                .HasForeignKey(rs => rs.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
