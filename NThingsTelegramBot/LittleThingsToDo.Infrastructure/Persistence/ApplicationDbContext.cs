using System;
using System.Threading;
using System.Threading.Tasks;
using LittleThingsToDo.Application.Interfaces.Services;
using LittleThingsToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LittleThingsToDo.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentAuthorService _authorService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, 
            ICurrentAuthorService authorService)
            : base(options)
        {
            _authorService = authorService;
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<LittleThing> LittleThings { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await SaveChangesAsync(true, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _authorService.CurrentAuthorId;
                        entry.Entity.CreatedOn = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = _authorService.CurrentAuthorId;
                        entry.Entity.ModifiedOn = DateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany<LittleThing>()
                .WithOne()
                .HasForeignKey(lt => lt.CreatedBy);

            modelBuilder.Entity<Author>()
                .HasMany<LittleThing>()
                .WithOne()
                .HasForeignKey(lt => lt.ModifiedBy);

            modelBuilder.Entity<Author>()
                .HasMany<Entry>()
                .WithOne()
                .HasForeignKey(entry => entry.CreatedBy);

            modelBuilder.Entity<Author>()
                .HasMany<Entry>()
                .WithOne()
                .HasForeignKey(entry => entry.ModifiedBy);

            modelBuilder.Entity<LittleThing>()
                .HasMany(lt => lt.Entries)
                .WithOne(entry => entry.LittleThing)
                .OnDelete(DeleteBehavior.Restrict);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
