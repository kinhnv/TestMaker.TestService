using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.TestService.Infrastructure.Entities
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
            modelBuilder.Entity<Section>().HasKey(t => t.SectionId);
            modelBuilder.Entity<Question>().HasKey(t => t.QuestionId);
        }
    }
}
