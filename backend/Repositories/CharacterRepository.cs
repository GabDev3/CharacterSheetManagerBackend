using Microsoft.EntityFrameworkCore;
using CharacterSheetManager.Models;
using CharacterSheetManager.Data;

namespace CharacterSheetManager.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllAsync();
        Task<Character?> GetByIdAsync(int id);
        Task<Character?> GetByIdWithDetailsAsync(int id);
        Task<Character> CreateAsync(Character character);
        Task<Character> UpdateAsync(Character character);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Character>> GetByClassAsync(string className);
        Task<IEnumerable<Character>> GetByLevelRangeAsync(int minLevel, int maxLevel);
    }
}

namespace CharacterSheetManager.Repositories
{
    public class CharacterRepository : CharacterSheetManager.Repositories.Interfaces.ICharacterRepository
    {
        private readonly AppDbContext _context;

        public CharacterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            return await _context.Characters
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Character?> GetByIdAsync(int id)
        {
            return await _context.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Character?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Characters
                .AsNoTracking()
                .Include(c => c.Items)
                    .ThenInclude(i => i.ItemTemplate)
                .Include(c => c.Spells)
                    .ThenInclude(s => s.SpellTemplate)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Character> CreateAsync(Character character)
        {
            character.CreatedAt = DateTime.UtcNow;
            character.UpdatedAt = DateTime.UtcNow;
            
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task<Character> UpdateAsync(Character character)
        {
            character.UpdatedAt = DateTime.UtcNow;
            
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task DeleteAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character != null)
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Characters.AnyAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Character>> GetByClassAsync(string className)
        {
            return await _context.Characters
                .AsNoTracking()
                .Where(c => c.Class.ToLower() == className.ToLower())
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Character>> GetByLevelRangeAsync(int minLevel, int maxLevel)
        {
            return await _context.Characters
                .AsNoTracking()
                .Where(c => c.Level >= minLevel && c.Level <= maxLevel)
                .OrderBy(c => c.Level)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }
    }
}