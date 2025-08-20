using Microsoft.AspNetCore.Mvc;
using CharacterSheetManager.DTOs.ItemTemplate;
using CharacterSheetManager.DTOs.SpellTemplate;
using CharacterSheetManager.Services.Interfaces;

namespace CharacterSheetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemTemplatesController : ControllerBase
    {
        private readonly IItemTemplateService _itemTemplateService;
        private readonly ILogger<ItemTemplatesController> _logger;

        public ItemTemplatesController(IItemTemplateService itemTemplateService, ILogger<ItemTemplatesController> logger)
        {
            _itemTemplateService = itemTemplateService;
            _logger = logger;
        }

        /// <summary>
        /// Get all item templates
        /// </summary>
        /// <returns>List of all item templates</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemTemplateDto>>> GetAllItemTemplates()
        {
            var templates = await _itemTemplateService.GetAllAsync();
            return Ok(templates);
        }

        /// <summary>
        /// Get only active item templates
        /// </summary>
        /// <returns>List of active item templates</returns>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ItemTemplateDto>>> GetActiveItemTemplates()
        {
            var templates = await _itemTemplateService.GetActiveAsync();
            return Ok(templates);
        }

        /// <summary>
        /// Get a specific item template by ID
        /// </summary>
        /// <param name="id">Item template ID</param>
        /// <returns>Item template details</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemTemplateDto>> GetItemTemplateById(int id)
        {
            var template = await _itemTemplateService.GetByIdAsync(id);
            return Ok(template);
        }

        /// <summary>
        /// Get item templates by category
        /// </summary>
        /// <param name="category">Category name</param>
        /// <returns>List of item templates in the category</returns>
        [HttpGet("by-category/{category}")]
        public async Task<ActionResult<IEnumerable<ItemTemplateDto>>> GetItemTemplatesByCategory(string category)
        {
            var templates = await _itemTemplateService.GetByCategoryAsync(category);
            return Ok(templates);
        }

        /// <summary>
        /// Get all available categories
        /// </summary>
        /// <returns>List of category names</returns>
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetCategories()
        {
            var categories = await _itemTemplateService.GetCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Create a new item template
        /// </summary>
        /// <param name="createDto">Item template creation data</param>
        /// <returns>Created item template details</returns>
        [HttpPost]
        public async Task<ActionResult<ItemTemplateDto>> CreateItemTemplate([FromBody] CreateItemTemplateDto createDto)
        {
            var template = await _itemTemplateService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetItemTemplateById), new { id = template.Id }, template);
        }

        /// <summary>
        /// Update an item template completely
        /// </summary>
        /// <param name="id">Item template ID</param>
        /// <param name="updateDto">Item template update data</param>
        /// <returns>Updated item template details</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ItemTemplateDto>> UpdateItemTemplate(int id, [FromBody] UpdateItemTemplateDto updateDto)
        {
            var template = await _itemTemplateService.UpdateAsync(id, updateDto);
            return Ok(template);
        }

        /// <summary>
        /// Partially update an item template
        /// </summary>
        /// <param name="id">Item template ID</param>
        /// <param name="patchDto">Partial item template update data</param>
        /// <returns>Updated item template details</returns>
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<ItemTemplateDto>> PatchItemTemplate(int id, [FromBody] UpdateItemTemplateDto patchDto)
        {
            var template = await _itemTemplateService.UpdateAsync(id, patchDto);
            return Ok(template);
        }

        /// <summary>
        /// Delete an item template
        /// </summary>
        /// <param name="id">Item template ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteItemTemplate(int id)
        {
            await _itemTemplateService.DeleteAsync(id);
            return NoContent();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SpellTemplatesController : ControllerBase
    {
        private readonly ISpellTemplateService _spellTemplateService;
        private readonly ILogger<SpellTemplatesController> _logger;

        public SpellTemplatesController(ISpellTemplateService spellTemplateService, ILogger<SpellTemplatesController> logger)
        {
            _spellTemplateService = spellTemplateService;
            _logger = logger;
        }

        /// <summary>
        /// Get all spell templates
        /// </summary>
        /// <returns>List of all spell templates</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpellTemplateDto>>> GetAllSpellTemplates()
        {
            var templates = await _spellTemplateService.GetAllAsync();
            return Ok(templates);
        }

        /// <summary>
        /// Get only active spell templates
        /// </summary>
        /// <returns>List of active spell templates</returns>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<SpellTemplateDto>>> GetActiveSpellTemplates()
        {
            var templates = await _spellTemplateService.GetActiveAsync();
            return Ok(templates);
        }

        /// <summary>
        /// Get a specific spell template by ID
        /// </summary>
        /// <param name="id">Spell template ID</param>
        /// <returns>Spell template details</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpellTemplateDto>> GetSpellTemplateById(int id)
        {
            var template = await _spellTemplateService.GetByIdAsync(id);
            return Ok(template);
        }

        /// <summary>
        /// Get spell templates by level
        /// </summary>
        /// <param name="level">Spell level (0-9)</param>
        /// <returns>List of spell templates of the specified level</returns>
        [HttpGet("by-level/{level:int}")]
        public async Task<ActionResult<IEnumerable<SpellTemplateDto>>> GetSpellTemplatesByLevel(int level)
        {
            var templates = await _spellTemplateService.GetByLevelAsync(level);
            return Ok(templates);
        }

        /// <summary>
        /// Get spell templates by school
        /// </summary>
        /// <param name="school">School name</param>
        /// <returns>List of spell templates in the school</returns>
        [HttpGet("by-school/{school}")]
        public async Task<ActionResult<IEnumerable<SpellTemplateDto>>> GetSpellTemplatesBySchool(string school)
        {
            var templates = await _spellTemplateService.GetBySchoolAsync(school);
            return Ok(templates);
        }

        /// <summary>
        /// Get all available schools
        /// </summary>
        /// <returns>List of school names</returns>
        [HttpGet("schools")]
        public async Task<ActionResult<IEnumerable<string>>> GetSchools()
        {
            var schools = await _spellTemplateService.GetSchoolsAsync();
            return Ok(schools);
        }

        /// <summary>
        /// Create a new spell template
        /// </summary>
        /// <param name="createDto">Spell template creation data</param>
        /// <returns>Created spell template details</returns>
        [HttpPost]
        public async Task<ActionResult<SpellTemplateDto>> CreateSpellTemplate([FromBody] CreateSpellTemplateDto createDto)
        {
            var template = await _spellTemplateService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetSpellTemplateById), new { id = template.Id }, template);
        }

        /// <summary>
        /// Update a spell template completely
        /// </summary>
        /// <param name="id">Spell template ID</param>
        /// <param name="updateDto">Spell template update data</param>
        /// <returns>Updated spell template details</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SpellTemplateDto>> UpdateSpellTemplate(int id, [FromBody] UpdateSpellTemplateDto updateDto)
        {
            var template = await _spellTemplateService.UpdateAsync(id, updateDto);
            return Ok(template);
        }

        /// <summary>
        /// Partially update a spell template
        /// </summary>
        /// <param name="id">Spell template ID</param>
        /// <param name="patchDto">Partial spell template update data</param>
        /// <returns>Updated spell template details</returns>
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<SpellTemplateDto>> PatchSpellTemplate(int id, [FromBody] UpdateSpellTemplateDto patchDto)
        {
            var template = await _spellTemplateService.UpdateAsync(id, patchDto);
            return Ok(template);
        }

        /// <summary>
        /// Delete a spell template
        /// </summary>
        /// <param name="id">Spell template ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSpellTemplate(int id)
        {
            await _spellTemplateService.DeleteAsync(id);
            return NoContent();
        }
    }
}