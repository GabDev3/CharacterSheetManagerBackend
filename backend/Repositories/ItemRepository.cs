using Microsoft.EntityFrameworkCore;
using CharacterSheetManager.Models;
using CharacterSheetManager.Data;

namespace CharacterSheetManager.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item?> GetByIdAsync(int id);
        Task<Item?> GetByIdWithDetailsAsync(int id);
        Task<IEnumerable<Item>> GetByCharacterIdAsync(int characterId);
        Task<Item> CreateAsync(Item item);
        Task<Item> UpdateAsync(Item item);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Item>> GetByTemplateIdAsync(int templateId);
    }
}

namespace CharacterSheetManager.Repositories
{
    public class ItemRepository : CharacterSheetManager.Repositories.Interfaces.IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.ItemTemplate)
                .Include(i => i.Character)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Items
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Item?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.ItemTemplate)
                .Include(i => i.Character)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Item>> GetByCharacterIdAsync(int characterId)
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.ItemTemplate)
                .Where(i => i.CharacterId == characterId)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<Item> CreateAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            
            
            return await GetByIdWithDetailsAsync(item.Id) ?? item;
        }

        public async Task<Item> UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            
            
            return await GetByIdWithDetailsAsync(item.Id) ?? item;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Items.AnyAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Item>> GetByTemplateIdAsync(int templateId)
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.ItemTemplate)
                .Include(i => i.Character)
                .Where(i => i.ItemTemplateId == templateId)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }
    }
}