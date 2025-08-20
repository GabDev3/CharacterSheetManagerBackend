using Microsoft.EntityFrameworkCore;
using CharacterSheetManager.Models;

namespace CharacterSheetManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<ItemTemplate> ItemTemplates { get; set; }
        public DbSet<SpellTemplate> SpellTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Character)
                .HasForeignKey(i => i.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Spells)
                .WithOne(s => s.Character)
                .HasForeignKey(s => s.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Character>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Character>()
                .Property(c => c.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
