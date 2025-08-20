using Microsoft.AspNetCore.Mvc;
using CharacterSheetManager.DTOs.Item;
using CharacterSheetManager.Services.Interfaces;

namespace CharacterSheetManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IItemService itemService, ILogger<ItemsController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns>List of all items</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItems()
        {
            var items = await _itemService.GetAllAsync();
            return Ok(items);
        }

        /// <summary>
        /// Get a specific item by ID
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>Item details</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ItemDto>> GetItemById(int id)
        {
            var item = await _itemService.GetByIdAsync(id);
            return Ok(item);
        }

        /// <summary>
        /// Get items by character ID
        /// </summary>
        /// <param name="characterId">Character ID</param>
        /// <returns>List of items belonging to the character</returns>
        [HttpGet("by-character/{characterId:int}")]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsByCharacterId(int characterId)
        {
            var items = await _itemService.GetByCharacterIdAsync(characterId);
            return Ok(items);
        }

        /// <summary>
        /// Create a new custom item
        /// </summary>
        /// <param name="createDto">Item creation data</param>
        /// <returns>Created item details</returns>
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem([FromBody] CreateItemDto createDto)
        {
            var item = await _itemService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        /// <summary>
        /// Create an item from a template
        /// </summary>
        /// <param name="createDto">Item from template creation data</param>
        /// <returns>Created item details</returns>
        [HttpPost("from-template")]
        public async Task<ActionResult<ItemDto>> CreateItemFromTemplate([FromBody] CreateItemFromTemplateDto createDto)
        {
            var item = await _itemService.CreateFromTemplateAsync(createDto);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        /// <summary>
        /// Update an item completely
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <param name="updateDto">Item update data</param>
        /// <returns>Updated item details</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ItemDto>> UpdateItem(int id, [FromBody] UpdateItemDto updateDto)
        {
            var item = await _itemService.UpdateAsync(id, updateDto);
            return Ok(item);
        }

        /// <summary>
        /// Partially update an item
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <param name="patchDto">Partial item update data</param>
        /// <returns>Updated item details</returns>
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<ItemDto>> PatchItem(int id, [FromBody] UpdateItemDto patchDto)
        {
            var item = await _itemService.UpdateAsync(id, patchDto);
            return Ok(item);
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            await _itemService.DeleteAsync(id);
            return NoContent();
        }
    }
}