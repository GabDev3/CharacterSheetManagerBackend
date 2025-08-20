using AutoMapper;
using CharacterSheetManager.DTOs.Spell;
using CharacterSheetManager.Models;
using CharacterSheetManager.Repositories.Interfaces;
using CharacterSheetManager.Exceptions;

namespace CharacterSheetManager.Services.Interfaces
{
    public interface ISpellService
    {
        Task<IEnumerable<SpellDto>> GetAllAsync();
        Task<SpellDto> GetByIdAsync(int id);
        Task<IEnumerable<SpellDto>> GetByCharacterIdAsync(int characterId);
        Task<SpellDto> CreateAsync(CreateSpellDto createDto);
        Task<SpellDto> CreateFromTemplateAsync(CreateSpellFromTemplateDto createDto);
        Task<SpellDto> UpdateAsync(int id, UpdateSpellDto updateDto);
        Task DeleteAsync(int id);
    }
}

namespace CharacterSheetManager.Services
{
    public class SpellService : Interfaces.ISpellService
    {
        private readonly ISpellRepository _spellRepository;
        private readonly ISpellTemplateRepository _spellTemplateRepository;
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SpellService> _logger;

        public SpellService(
            ISpellRepository spellRepository,
            ISpellTemplateRepository spellTemplateRepository,
            ICharacterRepository characterRepository,
            IMapper mapper,
            ILogger<SpellService> logger)
        {
            _spellRepository = spellRepository;
            _spellTemplateRepository = spellTemplateRepository;
            _characterRepository = characterRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SpellDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all spells");
            
            var spells = await _spellRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SpellDto>>(spells);
        }

        public async Task<SpellDto> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting spell with id {SpellId}", id);
            
            var spell = await _spellRepository.GetByIdWithDetailsAsync(id);
            if (spell == null)
            {
                throw new NotFoundException("Spell", id);
            }

            return _mapper.Map<SpellDto>(spell);
        }

        public async Task<IEnumerable<SpellDto>> GetByCharacterIdAsync(int characterId)
        {
            _logger.LogInformation("Getting spells for character with id {CharacterId}", characterId);
            
            
            var characterExists = await _characterRepository.ExistsAsync(characterId);
            if (!characterExists)
            {
                throw new NotFoundException("Character", characterId);
            }

            var spells = await _spellRepository.GetByCharacterIdAsync(characterId);
            return _mapper.Map<IEnumerable<SpellDto>>(spells);
        }

        public async Task<SpellDto> CreateAsync(CreateSpellDto createDto)
        {
            _logger.LogInformation("Creating new spell with name {SpellName} for character {CharacterId}", 
                createDto.Name, createDto.CharacterId);
            
            
            var characterExists = await _characterRepository.ExistsAsync(createDto.CharacterId);
            if (!characterExists)
            {
                throw new NotFoundException("Character", createDto.CharacterId);
            }

            
            if (createDto.SpellTemplateId.HasValue)
            {
                var templateExists = await _spellTemplateRepository.ExistsAsync(createDto.SpellTemplateId.Value);
                if (!templateExists)
                {
                    throw new NotFoundException("SpellTemplate", createDto.SpellTemplateId.Value);
                }
            }

            
            ValidateSpellData(createDto.Name, createDto.Damage);

            var spell = _mapper.Map<Spell>(createDto);
            var createdSpell = await _spellRepository.CreateAsync(spell);
            
            _logger.LogInformation("Spell created with id {SpellId}", createdSpell.Id);
            return _mapper.Map<SpellDto>(createdSpell);
        }

        public async Task<SpellDto> CreateFromTemplateAsync(CreateSpellFromTemplateDto createDto)
        {
            _logger.LogInformation("Creating spell from template {TemplateId} for character {CharacterId}", 
                createDto.SpellTemplateId, createDto.CharacterId);
            
            
            var characterExists = await _characterRepository.ExistsAsync(createDto.CharacterId);
            if (!characterExists)
            {
                throw new NotFoundException("Character", createDto.CharacterId);
            }

            
            var template = await _spellTemplateRepository.GetByIdAsync(createDto.SpellTemplateId);
            if (template == null)
            {
                throw new NotFoundException("SpellTemplate", createDto.SpellTemplateId);
            }

            if (!template.IsActive)
            {
                throw new ValidationException("Cannot create spell from inactive template");
            }

            
            var character = await _characterRepository.GetByIdAsync(createDto.CharacterId);
            if (character != null && !CanCharacterLearnSpell(character, template))
            {
                throw new ValidationException($"Character level {character.Level} is too low to learn a level {template.Level} spell");
            }

            
            var spell = new Spell
            {
                Name = createDto.CustomName ?? template.Name,
                Effect = createDto.CustomEffect ?? template.Effect,
                Damage = createDto.CustomDamage ?? template.Damage,
                SpellTemplateId = createDto.SpellTemplateId,
                CharacterId = createDto.CharacterId
            };

            ValidateSpellData(spell.Name, spell.Damage);

            var createdSpell = await _spellRepository.CreateAsync(spell);
            
            _logger.LogInformation("Spell created from template with id {SpellId}", createdSpell.Id);
            return _mapper.Map<SpellDto>(createdSpell);
        }

        public async Task<SpellDto> UpdateAsync(int id, UpdateSpellDto updateDto)
        {
            _logger.LogInformation("Updating spell with id {SpellId}", id);
            
            var existingSpell = await _spellRepository.GetByIdAsync(id);
            if (existingSpell == null)
            {
                throw new NotFoundException("Spell", id);
            }

            
            if (updateDto.SpellTemplateId.HasValue)
            {
                var templateExists = await _spellTemplateRepository.ExistsAsync(updateDto.SpellTemplateId.Value);
                if (!templateExists)
                {
                    throw new NotFoundException("SpellTemplate", updateDto.SpellTemplateId.Value);
                }
            }

            
            ValidateSpellData(updateDto.Name, updateDto.Damage);

            
            _mapper.Map(updateDto, existingSpell);
            
            var updatedSpell = await _spellRepository.UpdateAsync(existingSpell);
            
            _logger.LogInformation("Spell updated with id {SpellId}", updatedSpell.Id);
            return _mapper.Map<SpellDto>(updatedSpell);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting spell with id {SpellId}", id);
            
            var exists = await _spellRepository.ExistsAsync(id);
            if (!exists)
            {
                throw new NotFoundException("Spell", id);
            }

            await _spellRepository.DeleteAsync(id);
            _logger.LogInformation("Spell deleted with id {SpellId}", id);
        }

        private static void ValidateSpellData(string name, string? damage)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException("Spell name cannot be empty");
            }

            
            if (!string.IsNullOrWhiteSpace(damage) && !IsValidDamageFormat(damage))
            {
                throw new ValidationException("Invalid damage format. Use dice notation like '1d6', '2d8+3', etc.");
            }
        }

        private static bool IsValidDamageFormat(string damage)
        {
            
            
            return damage.Contains('d') || damage.ToLower().Contains("mod") || 
                   int.TryParse(damage, out _);
        }

        private static bool CanCharacterLearnSpell(Character character, SpellTemplate template)
        {
            
            
            
            
            if (template.Level == 0) return true; 
            
            var requiredLevel = template.Level * 2 - 1;
            return character.Level >= requiredLevel;
        }
    }
}