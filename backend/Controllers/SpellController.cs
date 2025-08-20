using Microsoft.AspNetCore.Mvc;
using CharacterSheetManager.DTOs.Spell;
using CharacterSheetManager.Services.Interfaces;

namespace CharacterSheetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SpellsController : ControllerBase
    {
        private readonly ISpellService _spellService;
        private readonly ILogger<SpellsController> _logger;

        public SpellsController(ISpellService spellService, ILogger<SpellsController> logger)
        {
            _spellService = spellService;
            _logger = logger;
        }

        /// <summary>
        /// Get all spells
        /// </summary>
        /// <returns>List of all spells</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpellDto>>> GetAllSpells()
        {
            var spells = await _spellService.GetAllAsync();
            return Ok(spells);
        }

        /// <summary>
        /// Get a specific spell by ID
        /// </summary>
        /// <param name="id">Spell ID</param>
        /// <returns>Spell details</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpellDto>> GetSpellById(int id)
        {
            var spell = await _spellService.GetByIdAsync(id);
            return Ok(spell);
        }

        /// <summary>
        /// Get spells by character ID
        /// </summary>
        /// <param name="characterId">Character ID</param>
        /// <returns>List of spells belonging to the character</returns>
        [HttpGet("by-character/{characterId:int}")]
        public async Task<ActionResult<IEnumerable<SpellDto>>> GetSpellsByCharacterId(int characterId)
        {
            var spells = await _spellService.GetByCharacterIdAsync(characterId);
            return Ok(spells);
        }

        /// <summary>
        /// Create a new custom spell
        /// </summary>
        /// <param name="createDto">Spell creation data</param>
        /// <returns>Created spell details</returns>
        [HttpPost]
        public async Task<ActionResult<SpellDto>> CreateSpell([FromBody] CreateSpellDto createDto)
        {
            var spell = await _spellService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetSpellById), new { id = spell.Id }, spell);
        }

        /// <summary>
        /// Create a spell from a template
        /// </summary>
        /// <param name="createDto">Spell from template creation data</param>
        /// <returns>Created spell details</returns>
        [HttpPost("from-template")]
        public async Task<ActionResult<SpellDto>> CreateSpellFromTemplate([FromBody] CreateSpellFromTemplateDto createDto)
        {
            var spell = await _spellService.CreateFromTemplateAsync(createDto);
            return CreatedAtAction(nameof(GetSpellById), new { id = spell.Id }, spell);
        }

        /// <summary>
        /// Update a spell completely
        /// </summary>
        /// <param name="id">Spell ID</param>
        /// <param name="updateDto">Spell update data</param>
        /// <returns>Updated spell details</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SpellDto>> UpdateSpell(int id, [FromBody] UpdateSpellDto updateDto)
        {
            var spell = await _spellService.UpdateAsync(id, updateDto);
            return Ok(spell);
        }

        /// <summary>
        /// Partially update a spell
        /// </summary>
        /// <param name="id">Spell ID</param>
        /// <param name="patchDto">Partial spell update data</param>
        /// <returns>Updated spell details</returns>
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<SpellDto>> PatchSpell(int id, [FromBody] UpdateSpellDto patchDto)
        {
            var spell = await _spellService.UpdateAsync(id, patchDto);
            return Ok(spell);
        }

        /// <summary>
        /// Delete a spell
        /// </summary>
        /// <param name="id">Spell ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSpell(int id)
        {
            await _spellService.DeleteAsync(id);
            return NoContent();
        }
    }
}