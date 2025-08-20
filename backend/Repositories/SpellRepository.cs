using Microsoft.EntityFrameworkCore;
using CharacterSheetManager.Models;
using CharacterSheetManager.Data;

namespace CharacterSheetManager.Repositories.Interfaces
{
    public interface ISpellRepository
    {
        Task<IEnumerable<Spell>> GetAllAsync();
        Task<Spell?> GetByIdAsync(int id);
        Task<Spell?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Spell>> GetByCharacterIdAsync(int characterId);
        Task<Spell> CreateAsync(Spell spell);
        Task<Spell> UpdateAsync(Spell spell);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Spell>> GetByTemplateIdAsync(int templateId);
    }
}

namespace CharacterSheetManager.Repositories
{
    public class SpellRepository : Interfaces.ISpellRepository
    {
        private readonly AppDbContext _context;

        public SpellRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Spell>> GetAllAsync()
        {
            return await _context.Spells
                .AsNoTracking()
                .Include(s => s.SpellTemplate)
                .Include(s => s.Character)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<Spell?> GetByIdAsync(int id)
        {
            return await _context.Spells
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Spell?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Spells
                .AsNoTracking()
                .Include(s => s.SpellTemplate)
                .Include(s => s.Character)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Spell>> GetByCharacterIdAsync(int characterId)
        {
            return await _context.Spells
                .AsNoTracking()
                .Include(s => s.SpellTemplate)
                .Where(s => s.CharacterId == characterId)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<Spell> CreateAsync(Spell spell)
        {
            _context.Spells.Add(spell);
            await _context.SaveChangesAsync();
            
            
            return await GetByIdWithDetailsAsync(spell.Id) ?? spell;
        }

        public async Task<Spell> UpdateAsync(Spell spell)
        {
            _context.Spells.Update(spell);
            await _context.SaveChangesAsync();
            
            
            return await GetByIdWithDetailsAsync(spell.Id) ?? spell;
        }

        public async Task DeleteAsync(int id)
        {
            var spell = await _context.Spells.FindAsync(id);
            if (spell != null)
            {
                _context.Spells.Remove(spell);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Spells.AnyAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Spell>> GetByTemplateIdAsync(int templateId)
        {
            return await _context.Spells
                .AsNoTracking()
                .Include(s => s.SpellTemplate)
                .Include(s => s.Character)
                .Where(s => s.SpellTemplateId == templateId)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
    }
}