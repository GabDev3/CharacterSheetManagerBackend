using AutoMapper;
using AutoMapper.QueryableExtensions;
using CharacterSheetManager.DTOs.Character;
using CharacterSheetManager.Models;
using CharacterSheetManager.Repositories.Interfaces;
using CharacterSheetManager.Exceptions;

namespace CharacterSheetManager.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<CharacterSummaryDto>> GetAllAsync();
        Task<CharacterDto> GetByIdAsync(int id);
        Task<CharacterDto> CreateAsync(CreateCharacterDto createDto);
        Task<CharacterDto> UpdateAsync(int id, UpdateCharacterDto updateDto);
        Task<CharacterDto> PatchAsync(int id, PatchCharacterDto patchDto); 
        Task DeleteAsync(int id);
        Task<IEnumerable<CharacterSummaryDto>> GetByClassAsync(string className);
        Task<IEnumerable<CharacterSummaryDto>> GetByLevelRangeAsync(int minLevel, int maxLevel);
    }
}

namespace CharacterSheetManager.Services
{
    public class CharacterService : Interfaces.ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CharacterService> _logger;

        public CharacterService(
            ICharacterRepository characterRepository,
            IMapper mapper,
            ILogger<CharacterService> logger)
        {
            _characterRepository = characterRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CharacterSummaryDto>> GetAllAsync()
        {
            _logger.LogInformation("Getting all characters");
            
            var characters = await _characterRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CharacterSummaryDto>>(characters);
        }

        public async Task<CharacterDto> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting character with id {CharacterId}", id);
            
            var character = await _characterRepository.GetByIdWithDetailsAsync(id);
            if (character == null)
            {
                throw new NotFoundException("Character", id);
            }

            return _mapper.Map<CharacterDto>(character);
        }

        public async Task<CharacterDto> CreateAsync(CreateCharacterDto createDto)
        {
            _logger.LogInformation("Creating new character with name {CharacterName}", createDto.Name);
            
            
            ValidateCharacterStats(createDto.Strength, createDto.Dexterity, createDto.Constitution,
                createDto.Intelligence, createDto.Wisdom, createDto.Charisma);

            var character = _mapper.Map<Character>(createDto);
            var createdCharacter = await _characterRepository.CreateAsync(character);
            
            _logger.LogInformation("Character created with id {CharacterId}", createdCharacter.Id);
            return _mapper.Map<CharacterDto>(createdCharacter);
        }

        public async Task<CharacterDto> UpdateAsync(int id, UpdateCharacterDto updateDto)
        {
            _logger.LogInformation("Updating character with id {CharacterId}", id);
            
            var existingCharacter = await _characterRepository.GetByIdAsync(id);
            if (existingCharacter == null)
            {
                throw new NotFoundException("Character", id);
            }

            
            ValidateCharacterStats(updateDto.Strength, updateDto.Dexterity, updateDto.Constitution,
                updateDto.Intelligence, updateDto.Wisdom, updateDto.Charisma);

            
            _mapper.Map(updateDto, existingCharacter);
            
            var updatedCharacter = await _characterRepository.UpdateAsync(existingCharacter);
            
            _logger.LogInformation("Character updated with id {CharacterId}", updatedCharacter.Id);
            return _mapper.Map<CharacterDto>(updatedCharacter);
        }

        
        public async Task<CharacterDto> PatchAsync(int id, PatchCharacterDto patchDto)
        {
            _logger.LogInformation("Patching character with id {CharacterId}. Fields to update: {@PatchData}", id, patchDto);
            
            var existingCharacter = await _characterRepository.GetByIdAsync(id);
            if (existingCharacter == null)
            {
                throw new NotFoundException("Character", id);
            }

            
            var originalStats = new
            {
                Strength = existingCharacter.Strength,
                Dexterity = existingCharacter.Dexterity,
                Constitution = existingCharacter.Constitution,
                Intelligence = existingCharacter.Intelligence,
                Wisdom = existingCharacter.Wisdom,
                Charisma = existingCharacter.Charisma
            };

            
            if (patchDto.Name != null)
            {
                _logger.LogInformation("Updating Name: {OldName} -> {NewName}", existingCharacter.Name, patchDto.Name);
                existingCharacter.Name = patchDto.Name;
            }

            if (patchDto.Class != null)
            {
                _logger.LogInformation("Updating Class: {OldClass} -> {NewClass}", existingCharacter.Class, patchDto.Class);
                existingCharacter.Class = patchDto.Class;
            }

            if (patchDto.Level.HasValue)
            {
                _logger.LogInformation("Updating Level: {OldLevel} -> {NewLevel}", existingCharacter.Level, patchDto.Level.Value);
                existingCharacter.Level = patchDto.Level.Value;
            }

            if (patchDto.ArmorClass.HasValue)
            {
                _logger.LogInformation("Updating ArmorClass: {OldAC} -> {NewAC}", existingCharacter.ArmorClass, patchDto.ArmorClass.Value);
                existingCharacter.ArmorClass = patchDto.ArmorClass.Value;
            }

            if (patchDto.Strength.HasValue)
            {
                _logger.LogInformation("Updating Strength: {OldStr} -> {NewStr}", existingCharacter.Strength, patchDto.Strength.Value);
                existingCharacter.Strength = patchDto.Strength.Value;
            }

            if (patchDto.Dexterity.HasValue)
            {
                _logger.LogInformation("Updating Dexterity: {OldDex} -> {NewDex}", existingCharacter.Dexterity, patchDto.Dexterity.Value);
                existingCharacter.Dexterity = patchDto.Dexterity.Value;
            }

            if (patchDto.Constitution.HasValue)
            {
                _logger.LogInformation("Updating Constitution: {OldCon} -> {NewCon}", existingCharacter.Constitution, patchDto.Constitution.Value);
                existingCharacter.Constitution = patchDto.Constitution.Value;
            }

            if (patchDto.Intelligence.HasValue)
            {
                _logger.LogInformation("Updating Intelligence: {OldInt} -> {NewInt}", existingCharacter.Intelligence, patchDto.Intelligence.Value);
                existingCharacter.Intelligence = patchDto.Intelligence.Value;
            }

            if (patchDto.Wisdom.HasValue)
            {
                _logger.LogInformation("Updating Wisdom: {OldWis} -> {NewWis}", existingCharacter.Wisdom, patchDto.Wisdom.Value);
                existingCharacter.Wisdom = patchDto.Wisdom.Value;
            }

            if (patchDto.Charisma.HasValue)
            {
                _logger.LogInformation("Updating Charisma: {OldCha} -> {NewCha}", existingCharacter.Charisma, patchDto.Charisma.Value);
                existingCharacter.Charisma = patchDto.Charisma.Value;
            }

            if (patchDto.ImageBase64 != null)
            {
                _logger.LogInformation("Updating ImageBase64: {HasOldImage} -> {HasNewImage}", 
                    !string.IsNullOrEmpty(existingCharacter.ImageBase64), 
                    !string.IsNullOrEmpty(patchDto.ImageBase64));
                existingCharacter.ImageBase64 = patchDto.ImageBase64;
            }

            
            
            bool statsChanged = patchDto.Strength.HasValue || patchDto.Dexterity.HasValue || 
                               patchDto.Constitution.HasValue || patchDto.Intelligence.HasValue || 
                               patchDto.Wisdom.HasValue || patchDto.Charisma.HasValue;

            if (statsChanged)
            {
                _logger.LogInformation("Stats changed, validating new stats");
                ValidateCharacterStats(existingCharacter.Strength, existingCharacter.Dexterity, 
                    existingCharacter.Constitution, existingCharacter.Intelligence, 
                    existingCharacter.Wisdom, existingCharacter.Charisma);
            }

            var updatedCharacter = await _characterRepository.UpdateAsync(existingCharacter);
            
            _logger.LogInformation("Character patched successfully with id {CharacterId}", updatedCharacter.Id);
            return _mapper.Map<CharacterDto>(updatedCharacter);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting character with id {CharacterId}", id);
            
            var exists = await _characterRepository.ExistsAsync(id);
            if (!exists)
            {
                throw new NotFoundException("Character", id);
            }

            await _characterRepository.DeleteAsync(id);
            _logger.LogInformation("Character deleted with id {CharacterId}", id);
        }

        public async Task<IEnumerable<CharacterSummaryDto>> GetByClassAsync(string className)
        {
            _logger.LogInformation("Getting characters by class {ClassName}", className);
            
            if (string.IsNullOrWhiteSpace(className))
            {
                throw new ValidationException("Class name cannot be empty");
            }

            var characters = await _characterRepository.GetByClassAsync(className);
            return _mapper.Map<IEnumerable<CharacterSummaryDto>>(characters);
        }

        public async Task<IEnumerable<CharacterSummaryDto>> GetByLevelRangeAsync(int minLevel, int maxLevel)
        {
            _logger.LogInformation("Getting characters by level range {MinLevel}-{MaxLevel}", minLevel, maxLevel);
            
            if (minLevel < 1 || maxLevel < 1 || minLevel > maxLevel)
            {
                throw new ValidationException("Invalid level range");
            }

            var characters = await _characterRepository.GetByLevelRangeAsync(minLevel, maxLevel);
            return _mapper.Map<IEnumerable<CharacterSummaryDto>>(characters);
        }

        private static void ValidateCharacterStats(int strength, int dexterity, int constitution,
            int intelligence, int wisdom, int charisma)
        {
            var stats = new[] { strength, dexterity, constitution, intelligence, wisdom, charisma };
            
            if (stats.Any(stat => stat < 1 || stat > 20))
            {
                throw new ValidationException("All character stats must be between 1 and 20");
            }

            
            var totalStats = stats.Sum();
            if (totalStats > 90)
            {
                throw new ValidationException($"Total character stats cannot exceed 90. Current total: {totalStats}");
            }
        }
    }
}