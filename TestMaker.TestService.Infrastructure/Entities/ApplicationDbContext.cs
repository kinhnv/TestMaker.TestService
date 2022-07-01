using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker.TestService.Infrastructure.Entities
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().HasKey(t => t.TestId);
            modelBuilder.Entity<Test>().Property(t => t.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Section>().HasKey(s => s.SectionId);
            modelBuilder.Entity<Section>().Property(s => s.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Question>().HasKey(q => q.QuestionId);
            modelBuilder.Entity<Question>().Property(q => q.IsDeleted).HasDefaultValue(false);
        }
    }
}
