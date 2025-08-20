using AutoMapper;
using CharacterSheetManager.DTOs.Item;
using CharacterSheetManager.Models;
using CharacterSheetManager.Repositories.Interfaces;
using CharacterSheetManager.Exceptions;

namespace CharacterSheetManager.Services.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllAsync();
        Task<ItemDto> GetByIdAsync(int id);
        Task<IEnumerable<ItemDto>> GetByCharacterIdAsync(int characterId);
        Task<ItemDto> CreateAsync(CreateItemDto createDto);
        Task<ItemDto> CreateFromTemplateAsync(CreateItemFromTemplateDto createDto);
        Task<ItemDto> UpdateAsync(int id, UpdateItemDto updateDto);
        Task DeleteAsync(int id);
    }
}

namespace CharacterSheetManager.Services
{
    public class ItemService : Interfaces.IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTemplateRepository _itemTemplateRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemService> _logger;

        public ItemService(
            IItemRepository itemRepository,
            IItemTemplateRepository itemTemplateRepository,
            ICharacterRepository characterRepository,
            IMapper mapper,
            ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _itemTemplateRepository = itemTemplateRepository;
            _characterRepository = characterRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ItemDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all items");
            
            var items = await _itemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public async Task<ItemDto> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting item with id {ItemId}", id);
            
            var item = await _itemRepository.GetByIdWithDetailsAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Item", id);
            }

            return _mapper.Map<ItemDto>(item);
        }

        public async Task<IEnumerable<ItemDto>> GetByCharacterIdAsync(int characterId)
        {
            _logger.LogInformation("Getting items for character with id {CharacterId}", characterId);
            
            // Validate that character exists
            var characterExists = await _characterRepository.ExistsAsync(characterId);
            if (!characterExists)
            {
                throw new NotFoundException("Character", characterId);
            }

            var items = await _itemRepository.GetByCharacterIdAsync(characterId);
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public async Task<ItemDto> CreateAsync(CreateItemDto createDto)
        {
            _logger.LogInformation("Creating new item with name {ItemName} for character {CharacterId}", 
                createDto.Name, createDto.CharacterId);
            
            // Validate that character exists
            var characterExists = await _characterRepository.ExistsAsync(createDto.CharacterId);
            if (!characterExists)
            {
                throw new NotFoundException("Character", createDto.CharacterId);
            }

            // Validate template if provided
            if (createDto.ItemTemplateId.HasValue)
            {
                var templateExists = await _itemTemplateRepository.ExistsAsync(createDto.ItemTemplateId.Value);
                if (!templateExists)
                {
                    throw new NotFoundException("ItemTemplate", createDto.ItemTemplateId.Value);
                }
            }

            // Business logic: Validate bonuses
            ValidateItemBonuses(createDto.StrengthBonus, createDto.DexterityBonus, 
                createDto.ConstitutionBonus, createDto.IntelligenceBonus, 
                createDto.WisdomBonus, createDto.CharismaBonus, createDto.ArmorBonus);

            var item = _mapper.Map<Item>(createDto);
            var createdItem = await _itemRepository.CreateAsync(item);
            
            _logger.LogInformation("Item created with id {ItemId}", createdItem.Id);
            return _mapper.Map<ItemDto>(createdItem);
        }

        public async Task<ItemDto> CreateFromTemplateAsync(CreateItemFromTemplateDto createDto)
        {
            _logger.LogInformation("Creating item from template {TemplateId} for character {CharacterId}", 
                createDto.ItemTemplateId, createDto.CharacterId);
            
            // Validate that character exists
            var characterExists = await _characterRepository.ExistsAsync(createDto.CharacterId);
            if (!characterExists)
            {
                throw new NotFoundException("Character", createDto.CharacterId);
            }

            // Get the template
            var template = await _itemTemplateRepository.GetByIdAsync(createDto.ItemTemplateId);
            if (template == null)
            {
                throw new NotFoundException("ItemTemplate", createDto.ItemTemplateId);
            }

            if (!template.IsActive)
            {
                throw new ValidationException("Cannot create item from inactive template");
            }

            // Create item from template with optional overrides
            var item = new Item
            {
                Name = createDto.CustomName ?? template.Name,
                Effect = createDto.CustomEffect ?? template.Effect,
                StrengthBonus = createDto.StrengthBonus ?? template.StrengthBonus,
                DexterityBonus = createDto.DexterityBonus ?? template.DexterityBonus,
                ConstitutionBonus = createDto.ConstitutionBonus ?? template.ConstitutionBonus,
                IntelligenceBonus = createDto.IntelligenceBonus ?? template.IntelligenceBonus,
                WisdomBonus = createDto.WisdomBonus ?? template.WisdomBonus,
                CharismaBonus = createDto.CharismaBonus ?? template.CharismaBonus,
                ArmorBonus = createDto.ArmorBonus ?? template.ArmorBonus,
                ItemTemplateId = createDto.ItemTemplateId,
                CharacterId = createDto.CharacterId
            };

            ValidateItemBonuses(item.StrengthBonus, item.DexterityBonus, 
                item.ConstitutionBonus, item.IntelligenceBonus, 
                item.WisdomBonus, item.CharismaBonus, item.ArmorBonus);

            var createdItem = await _itemRepository.CreateAsync(item);
            
            _logger.LogInformation("Item created from template with id {ItemId}", createdItem.Id);
            return _mapper.Map<ItemDto>(createdItem);
        }

        public async Task<ItemDto> UpdateAsync(int id, UpdateItemDto updateDto)
        {
            _logger.LogInformation("Updating item with id {ItemId}", id);
            
            var existingItem = await _itemRepository.GetByIdAsync(id);
            if (existingItem == null)
            {
                throw new NotFoundException("Item", id);
            }

            // Validate template if provided
            if (updateDto.ItemTemplateId.HasValue)
            {
                var templateExists = await _itemTemplateRepository.ExistsAsync(updateDto.ItemTemplateId.Value);
                if (!templateExists)
                {
                    throw new NotFoundException("ItemTemplate", updateDto.ItemTemplateId.Value);
                }
            }

            // Business logic: Validate bonuses
            ValidateItemBonuses(updateDto.StrengthBonus, updateDto.DexterityBonus, 
                updateDto.ConstitutionBonus, updateDto.IntelligenceBonus, 
                updateDto.WisdomBonus, updateDto.CharismaBonus, updateDto.ArmorBonus);

            // Map updates to existing item
            _mapper.Map(updateDto, existingItem);
            
            var updatedItem = await _itemRepository.UpdateAsync(existingItem);
            
            _logger.LogInformation("Item updated with id {ItemId}", updatedItem.Id);
            return _mapper.Map<ItemDto>(updatedItem);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting item with id {ItemId}", id);
            
            var exists = await _itemRepository.ExistsAsync(id);
            if (!exists)
            {
                throw new NotFoundException("Item", id);
            }

            await _itemRepository.DeleteAsync(id);
            _logger.LogInformation("Item deleted with id {ItemId}", id);
        }

        private static void ValidateItemBonuses(int? strengthBonus, int? dexterityBonus, 
            int? constitutionBonus, int? intelligenceBonus, int? wisdomBonus, 
            int? charismaBonus, int? armorBonus)
        {
            var bonuses = new[] { strengthBonus, dexterityBonus, constitutionBonus, 
                intelligenceBonus, wisdomBonus, charismaBonus };

            // Check stat bonus ranges
            if (bonuses.Any(bonus => bonus.HasValue && (bonus.Value < -10 || bonus.Value > 10)))
            {
                throw new ValidationException("Stat bonuses must be between -10 and 10");
            }

            // Check armor bonus range
            if (armorBonus.HasValue && (armorBonus.Value < -10 || armorBonus.Value > 20))
            {
                throw new ValidationException("Armor bonus must be between -10 and 20");
            }

            // Business rule: Total positive bonuses cannot exceed 15
            var totalPositiveBonuses = bonuses.Where(b => b.HasValue && b.Value > 0).Sum(b => b.Value);
            if (armorBonus.HasValue && armorBonus.Value > 0)
            {
                totalPositiveBonuses += armorBonus.Value;
            }

            if (totalPositiveBonuses > 15)
            {
                throw new ValidationException($"Total positive bonuses cannot exceed 15. Current total: {totalPositiveBonuses}");
            }
        }
    }
}