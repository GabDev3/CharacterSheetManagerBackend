using Microsoft.AspNetCore.Mvc;
using CharacterSheetManager.DTOs.Character;
using CharacterSheetManager.Services.Interfaces;

namespace CharacterSheetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly ILogger<CharactersController> _logger;

        public CharactersController(ICharacterService characterService, ILogger<CharactersController> logger)
        {
            _characterService = characterService;
            _logger = logger;
        }

        /// <summary>
        /// Get all characters (summary view)
        /// </summary>
        /// <returns>List of character summaries</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterSummaryDto>>> GetAllCharacters()
        {
            var characters = await _characterService.GetAllAsync();
            return Ok(characters);
        }

        /// <summary>
        /// Get a specific character by ID with full details
        /// </summary>
        /// <param name="id">Character ID</param>
        /// <returns>Character details including items and spells</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CharacterDto>> GetCharacterById(int id)
        {
            var character = await _characterService.GetByIdAsync(id);
            return Ok(character);
        }

        /// <summary>
        /// Create a new character
        /// </summary>
        /// <param name="createDto">Character creation data</param>
        /// <returns>Created character details</returns>
        [HttpPost]
        public async Task<ActionResult<CharacterDto>> CreateCharacter([FromBody] CreateCharacterDto createDto)
        {
            var character = await _characterService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetCharacterById), new { id = character.Id }, character);
        }

        /// <summary>
        /// Update a character completely
        /// </summary>
        /// <param name="id">Character ID</param>
        /// <param name="updateDto">Character update data</param>
        /// <returns>Updated character details</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CharacterDto>> UpdateCharacter(int id, [FromBody] UpdateCharacterDto updateDto)
        {
            var character = await _characterService.UpdateAsync(id, updateDto);
            return Ok(character);
        }

        /// <summary>
        /// Partially update a character
        /// </summary>
        /// <param name="id">Character ID</param>
        /// <param name="patchDto">Partial character update data</param>
        /// <returns>Updated character details</returns>
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CharacterDto>> PatchCharacter(int id, [FromBody] PatchCharacterDto patchDto)
        {
            _logger.LogInformation("PATCH request for character {CharacterId} with data: {@PatchData}", id, patchDto);
            
            try
            {
                var character = await _characterService.PatchAsync(id, patchDto);
                return Ok(character);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error patching character {CharacterId}", id);
                throw;
            }
        }

        /// <summary>
        /// Delete a character
        /// </summary>
        /// <param name="id">Character ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            await _characterService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get characters by class
        /// </summary>
        /// <param name="className">Character class name</param>
        /// <returns>List of characters with the specified class</returns>
        [HttpGet("by-class/{className}")]
        public async Task<ActionResult<IEnumerable<CharacterSummaryDto>>> GetCharactersByClass(string className)
        {
            var characters = await _characterService.GetByClassAsync(className);
            return Ok(characters);
        }

        /// <summary>
        /// Get characters by level range
        /// </summary>
        /// <param name="minLevel">Minimum level (inclusive)</param>
        /// <param name="maxLevel">Maximum level (inclusive)</param>
        /// <returns>List of characters within the specified level range</returns>
        [HttpGet("by-level")]
        public async Task<ActionResult<IEnumerable<CharacterSummaryDto>>> GetCharactersByLevelRange(
            [FromQuery] int minLevel, [FromQuery] int maxLevel)
        {
            var characters = await _characterService.GetByLevelRangeAsync(minLevel, maxLevel);
            return Ok(characters);
        }
    }
}