using CharacterSheetManager.DTOs.ItemTemplate;
using CharacterSheetManager.DTOs.SpellTemplate;
using CharacterSheetManager.Exceptions;
using AutoMapper;
using CharacterSheetManager.Models;
using CharacterSheetManager.Repositories.Interfaces;
using CharacterSheetManager.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace CharacterSheetManager.Services.Interfaces
{
    public interface IItemTemplateService
    {
        Task<IEnumerable<ItemTemplateDto>> GetAllAsync();
        Task<IEnumerable<ItemTemplateDto>> GetActiveAsync();
        Task<ItemTemplateDto> GetByIdAsync(int id);
        Task<ItemTemplateDto> CreateAsync(CreateItemTemplateDto createDto);
        Task<ItemTemplateDto> UpdateAsync(int id, UpdateItemTemplateDto updateDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ItemTemplateDto>> GetByCategoryAsync(string category);
        Task<IEnumerable<string>> GetCategoriesAsync();
    }

    public interface ISpellTemplateService
    {
        Task<IEnumerable<SpellTemplateDto>> GetAllAsync();
        Task<IEnumerable<SpellTemplateDto>> GetActiveAsync();
        Task<SpellTemplateDto> GetByIdAsync(int id);
        Task<SpellTemplateDto> CreateAsync(CreateSpellTemplateDto createDto);
        Task<SpellTemplateDto> UpdateAsync(int id, UpdateSpellTemplateDto updateDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<SpellTemplateDto>> GetByLevelAsync(int level);
        Task<IEnumerable<SpellTemplateDto>> GetBySchoolAsync(string school);
        Task<IEnumerable<string>> GetSchoolsAsync();
    }
}



namespace CharacterSheetManager.Services
{
    public class ItemTemplateService : IItemTemplateService
    {
        private readonly IItemTemplateRepository _itemTemplateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemTemplateService> _logger;

        public ItemTemplateService(
            IItemTemplateRepository itemTemplateRepository,
            IMapper mapper,
            ILogger<ItemTemplateService> logger)
        {
            _itemTemplateRepository = itemTemplateRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ItemTemplateDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all item templates");
            var templates = await _itemTemplateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ItemTemplateDto>>(templates);
        }

        public async Task<IEnumerable<ItemTemplateDto>> GetActiveAsync()
        {
            _logger.LogInformation("Getting active item templates");
            var templates = await _itemTemplateRepository.GetActiveAsync();
            return _mapper.Map<IEnumerable<ItemTemplateDto>>(templates);
        }

        public async Task<ItemTemplateDto> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting item template with id {TemplateId}", id);
            var template = await _itemTemplateRepository.GetByIdAsync(id);
            if (template == null)
            {
                throw new NotFoundException("ItemTemplate", id);
            }
            return _mapper.Map<ItemTemplateDto>(template);
        }

        public async Task<ItemTemplateDto> CreateAsync(CreateItemTemplateDto createDto)
        {
            _logger.LogInformation("Creating new item template with name {TemplateName}", createDto.Name);

            ValidateItemTemplateData(createDto);

            var entity = _mapper.Map<ItemTemplate>(createDto);
            var created = await _itemTemplateRepository.CreateAsync(entity);

            _logger.LogInformation("Item template created with id {TemplateId}", created.Id);
            return _mapper.Map<ItemTemplateDto>(created);
        }

        public async Task<ItemTemplateDto> UpdateAsync(int id, UpdateItemTemplateDto updateDto)
        {
            _logger.LogInformation("Updating item template with id {TemplateId}", id);

            var existing = await _itemTemplateRepository.GetByIdAsync(id);
            if (existing == null)
            {
                throw new NotFoundException("ItemTemplate", id);
            }

            ValidateItemTemplateData(updateDto);

            _mapper.Map(updateDto, existing);
            var updated = await _itemTemplateRepository.UpdateAsync(existing);

            _logger.LogInformation("Item template updated with id {TemplateId}", updated.Id);
            return _mapper.Map<ItemTemplateDto>(updated);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting item template with id {TemplateId}", id);

            var exists = await _itemTemplateRepository.ExistsAsync(id);
            if (!exists)
            {
                throw new NotFoundException("ItemTemplate", id);
            }

            await _itemTemplateRepository.DeleteAsync(id);
            _logger.LogInformation("Item template deleted with id {TemplateId}", id);
        }

        public async Task<IEnumerable<ItemTemplateDto>> GetByCategoryAsync(string category)
        {
            _logger.LogInformation("Getting item templates by category {Category}", category);

            if (string.IsNullOrWhiteSpace(category))
            {
                throw new Exceptions.ValidationException("Category cannot be empty");
            }

            var templates = await _itemTemplateRepository.GetByCategoryAsync(category);
            return _mapper.Map<IEnumerable<ItemTemplateDto>>(templates);
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            _logger.LogInformation("Getting all item template categories");
            return await _itemTemplateRepository.GetCategoriesAsync();
        }

        
        
        
        private static void ValidateItemTemplateData(CreateItemTemplateDto dto)
            => ValidateItemTemplateDataCommon(dto.Name, dto.Category, dto.StrengthBonus,
                dto.DexterityBonus, dto.ConstitutionBonus, dto.IntelligenceBonus,
                dto.WisdomBonus, dto.CharismaBonus, dto.ArmorBonus);

        private static void ValidateItemTemplateData(UpdateItemTemplateDto dto)
            => ValidateItemTemplateDataCommon(dto.Name, dto.Category, dto.StrengthBonus,
                dto.DexterityBonus, dto.ConstitutionBonus, dto.IntelligenceBonus,
                dto.WisdomBonus, dto.CharismaBonus, dto.ArmorBonus);

        private static void ValidateItemTemplateDataCommon(
            string name,
            string category,
            int? strengthBonus,
            int? dexterityBonus,
            int? constitutionBonus,
            int? intelligenceBonus,
            int? wisdomBonus,
            int? charismaBonus,
            int? armorBonus)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exceptions.ValidationException("Template name cannot be empty");

            if (string.IsNullOrWhiteSpace(category))
                throw new Exceptions.ValidationException("Template category cannot be empty");

            var bonuses = new[] { strengthBonus, dexterityBonus, constitutionBonus, intelligenceBonus, wisdomBonus, charismaBonus };

            if (bonuses.Any(b => b is < -10 or > 10))
                throw new Exceptions.ValidationException("Stat bonuses must be between -10 and 10");

            if (armorBonus is < -10 or > 20)
                throw new Exceptions.ValidationException("Armor bonus must be between -10 and 20");

            var totalPositive = bonuses.Where(b => b.HasValue && b.Value > 0).Sum(b => b!.Value)
                               + (armorBonus.HasValue && armorBonus.Value > 0 ? armorBonus.Value : 0);

            if (totalPositive > 15)
                throw new Exceptions.ValidationException($"Total positive bonuses cannot exceed 15. Current total: {totalPositive}");
        }
    }
}



namespace CharacterSheetManager.Services
{
    public class SpellTemplateService : ISpellTemplateService
    {
        private readonly ISpellTemplateRepository _spellTemplateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SpellTemplateService> _logger;

        public SpellTemplateService(
            ISpellTemplateRepository spellTemplateRepository,
            IMapper mapper,
            ILogger<SpellTemplateService> logger)
        {
            _spellTemplateRepository = spellTemplateRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SpellTemplateDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all spell templates");
            var templates = await _spellTemplateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SpellTemplateDto>>(templates);
        }

        public async Task<IEnumerable<SpellTemplateDto>> GetActiveAsync()
        {
            _logger.LogInformation("Getting active spell templates");
            var templates = await _spellTemplateRepository.GetActiveAsync();
            return _mapper.Map<IEnumerable<SpellTemplateDto>>(templates);
        }

        public async Task<SpellTemplateDto> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting spell template with id {TemplateId}", id);
            var template = await _spellTemplateRepository.GetByIdAsync(id);
            if (template == null)
            {
                throw new NotFoundException("SpellTemplate", id);
            }
            return _mapper.Map<SpellTemplateDto>(template);
        }

        public async Task<SpellTemplateDto> CreateAsync(CreateSpellTemplateDto createDto)
        {
            _logger.LogInformation("Creating new spell template with name {TemplateName}", createDto.Name);

            ValidateSpellTemplateData(createDto);

            var entity = _mapper.Map<SpellTemplate>(createDto);
            var created = await _spellTemplateRepository.CreateAsync(entity);

            _logger.LogInformation("Spell template created with id {TemplateId}", created.Id);
            return _mapper.Map<SpellTemplateDto>(created);
        }

        public async Task<SpellTemplateDto> UpdateAsync(int id, UpdateSpellTemplateDto updateDto)
        {
            _logger.LogInformation("Updating spell template with id {TemplateId}", id);

            var existing = await _spellTemplateRepository.GetByIdAsync(id);
            if (existing == null)
            {
                throw new NotFoundException("SpellTemplate", id);
            }

            ValidateSpellTemplateData(updateDto);

            _mapper.Map(updateDto, existing);
            var updated = await _spellTemplateRepository.UpdateAsync(existing);

            _logger.LogInformation("Spell template updated with id {TemplateId}", updated.Id);
            return _mapper.Map<SpellTemplateDto>(updated);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting spell template with id {TemplateId}", id);

            var exists = await _spellTemplateRepository.ExistsAsync(id);
            if (!exists)
            {
                throw new NotFoundException("SpellTemplate", id);
            }

            await _spellTemplateRepository.DeleteAsync(id);
            _logger.LogInformation("Spell template deleted with id {TemplateId}", id);
        }

        public async Task<IEnumerable<SpellTemplateDto>> GetByLevelAsync(int level)
        {
            _logger.LogInformation("Getting spell templates by level {Level}", level);

            if (level < 0 || level > 9)
                throw new Exceptions.ValidationException("Spell level must be between 0 and 9");

            var templates = await _spellTemplateRepository.GetByLevelAsync(level);
            return _mapper.Map<IEnumerable<SpellTemplateDto>>(templates);
        }

        public async Task<IEnumerable<SpellTemplateDto>> GetBySchoolAsync(string school)
        {
            _logger.LogInformation("Getting spell templates by school {School}", school);

            if (string.IsNullOrWhiteSpace(school))
                throw new Exceptions.ValidationException("School cannot be empty");

            var templates = await _spellTemplateRepository.GetBySchoolAsync(school);
            return _mapper.Map<IEnumerable<SpellTemplateDto>>(templates);
        }

        public async Task<IEnumerable<string>> GetSchoolsAsync()
        {
            _logger.LogInformation("Getting all spell template schools");
            return await _spellTemplateRepository.GetSchoolsAsync();
        }

        
        
        
        private static void ValidateSpellTemplateData(CreateSpellTemplateDto dto)
            => ValidateSpellTemplateDataCommon(
                dto.Name, dto.Level, dto.School, dto.CastingTime, dto.Range, dto.Components, dto.Duration, dto.Damage);

        private static void ValidateSpellTemplateData(UpdateSpellTemplateDto dto)
            => ValidateSpellTemplateDataCommon(
                dto.Name, dto.Level, dto.School, dto.CastingTime, dto.Range, dto.Components, dto.Duration, dto.Damage);

        private static void ValidateSpellTemplateDataCommon(
            string name,
            int level,
            string school,
            string castingTime,
            string range,
            string components,
            string duration,
            string? damage)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exceptions.ValidationException("Template name cannot be empty");

            if (level is < 0 or > 9)
                throw new Exceptions.ValidationException("Spell level must be between 0 and 9");

            if (string.IsNullOrWhiteSpace(school))
                throw new Exceptions.ValidationException("School cannot be empty");

            if (string.IsNullOrWhiteSpace(castingTime))
                throw new Exceptions.ValidationException("Casting time cannot be empty");

            if (string.IsNullOrWhiteSpace(range))
                throw new Exceptions.ValidationException("Range cannot be empty");

            if (string.IsNullOrWhiteSpace(components))
                throw new Exceptions.ValidationException("Components cannot be empty");

            if (string.IsNullOrWhiteSpace(duration))
                throw new Exceptions.ValidationException("Duration cannot be empty");

            if (!string.IsNullOrWhiteSpace(damage) && !IsValidDamageFormat(damage))
                throw new Exceptions.ValidationException("Invalid damage format. Use dice notation like '1d6', '2d8+3', etc.");
        }

        private static bool IsValidDamageFormat(string damage)
        {
            
            
            var regex = new Regex(@"^(?:\d+d\d+(?:[+-]\d+)?)|(?:\d+)$", RegexOptions.IgnoreCase);
            return regex.IsMatch(damage.Trim());
        }
    }
}
