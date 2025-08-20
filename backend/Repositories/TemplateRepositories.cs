using Microsoft.EntityFrameworkCore;
using CharacterSheetManager.Models;
using CharacterSheetManager.Data;

namespace CharacterSheetManager.Repositories.Interfaces
{
    public interface IItemTemplateRepository
    {
        Task<IEnumerable<ItemTemplate>> GetAllAsync();
        Task<IEnumerable<ItemTemplate>> GetActiveAsync();
        Task<ItemTemplate?> GetByIdAsync(int id);
        Task<ItemTemplate> CreateAsync(ItemTemplate itemTemplate);
        Task<ItemTemplate> UpdateAsync(ItemTemplate itemTemplate);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<ItemTemplate>> GetByCategoryAsync(string category);
        Task<IEnumerable<string>> GetCategoriesAsync();
    }

    public interface ISpellTemplateRepository
    {
        Task<IEnumerable<SpellTemplate>> GetAllAsync();
        Task<IEnumerable<SpellTemplate>> GetActiveAsync();
        Task<SpellTemplate?> GetByIdAsync(int id);
        Task<SpellTemplate> CreateAsync(SpellTemplate spellTemplate);
        Task<SpellTemplate> UpdateAsync(SpellTemplate spellTemplate);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<SpellTemplate>> GetByLevelAsync(int level);
        Task<IEnumerable<SpellTemplate>> GetBySchoolAsync(string school);
        Task<IEnumerable<string>> GetSchoolsAsync();
    }
}

namespace CharacterSheetManager.Repositories
{
    public class ItemTemplateRepository : Interfaces.IItemTemplateRepository
    {
        private readonly AppDbContext _context;

        public ItemTemplateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemTemplate>> GetAllAsync()
        {
            return await _context.ItemTemplates
                .AsNoTracking()
                .OrderBy(it => it.Category)
                .ThenBy(it => it.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<ItemTemplate>> GetActiveAsync()
        {
            return await _context.ItemTemplates
                .AsNoTracking()
                .Where(it => it.IsActive)
                .OrderBy(it => it.Category)
                .ThenBy(it => it.Name)
                .ToListAsync();
        }

        public async Task<ItemTemplate?> GetByIdAsync(int id)
        {
            return await _context.ItemTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(it => it.Id == id);
        }

        public async Task<ItemTemplate> CreateAsync(ItemTemplate itemTemplate)
        {
            itemTemplate.CreatedAt = DateTime.UtcNow;
            itemTemplate.UpdatedAt = DateTime.UtcNow;
            
            _context.ItemTemplates.Add(itemTemplate);
            await _context.SaveChangesAsync();
            return itemTemplate;
        }

        public async Task<ItemTemplate> UpdateAsync(ItemTemplate itemTemplate)
        {
            itemTemplate.UpdatedAt = DateTime.UtcNow;
            
            _context.ItemTemplates.Update(itemTemplate);
            await _context.SaveChangesAsync();
            return itemTemplate;
        }

        public async Task DeleteAsync(int id)
        {
            var itemTemplate = await _context.ItemTemplates.FindAsync(id);
            if (itemTemplate != null)
            {
                _context.ItemTemplates.Remove(itemTemplate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ItemTemplates.AnyAsync(it => it.Id == id);
        }

        public async Task<IEnumerable<ItemTemplate>> GetByCategoryAsync(string category)
        {
            return await _context.ItemTemplates
                .AsNoTracking()
                .Where(it => it.Category.ToLower() == category.ToLower() && it.IsActive)
                .OrderBy(it => it.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            return await _context.ItemTemplates
                .AsNoTracking()
                .Where(it => it.IsActive)
                .Select(it => it.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }
    }

    public class SpellTemplateRepository : Interfaces.ISpellTemplateRepository
    {
        private readonly AppDbContext _context;

        public SpellTemplateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SpellTemplate>> GetAllAsync()
        {
            return await _context.SpellTemplates
                .AsNoTracking()
                .OrderBy(st => st.Level)
                .ThenBy(st => st.School)
                .ThenBy(st => st.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<SpellTemplate>> GetActiveAsync()
        {
            return await _context.SpellTemplates
                .AsNoTracking()
                .Where(st => st.IsActive)
                .OrderBy(st => st.Level)
                .ThenBy(st => st.School)
                .ThenBy(st => st.Name)
                .ToListAsync();
        }

        public async Task<SpellTemplate?> GetByIdAsync(int id)
        {
            return await _context.SpellTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(st => st.Id == id);
        }

        public async Task<SpellTemplate> CreateAsync(SpellTemplate spellTemplate)
        {
            spellTemplate.CreatedAt = DateTime.UtcNow;
            spellTemplate.UpdatedAt = DateTime.UtcNow;
            
            _context.SpellTemplates.Add(spellTemplate);
            await _context.SaveChangesAsync();
            return spellTemplate;
        }

        public async Task<SpellTemplate> UpdateAsync(SpellTemplate spellTemplate)
        {
            spellTemplate.UpdatedAt = DateTime.UtcNow;
            
            _context.SpellTemplates.Update(spellTemplate);
            await _context.SaveChangesAsync();
            return spellTemplate;
        }

        public async Task DeleteAsync(int id)
        {
            var spellTemplate = await _context.SpellTemplates.FindAsync(id);
            if (spellTemplate != null)
            {
                _context.SpellTemplates.Remove(spellTemplate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.SpellTemplates.AnyAsync(st => st.Id == id);
        }

        public async Task<IEnumerable<SpellTemplate>> GetByLevelAsync(int level)
        {
            return await _context.SpellTemplates
                .AsNoTracking()
                .Where(st => st.Level == level && st.IsActive)
                .OrderBy(st => st.School)
                .ThenBy(st => st.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<SpellTemplate>> GetBySchoolAsync(string school)
        {
            return await _context.SpellTemplates
                .AsNoTracking()
                .Where(st => st.School.ToLower() == school.ToLower() && st.IsActive)
                .OrderBy(st => st.Level)
                .ThenBy(st => st.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetSchoolsAsync()
        {
            return await _context.SpellTemplates
                .AsNoTracking()
                .Where(st => st.IsActive)
                .Select(st => st.School)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();
        }
    }
}