using Catalog.API.DTOs;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;
        private readonly ILogger<ItemsController> logger;

        public ItemsController(IItemsRepository repository, ILogger<ItemsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());

            logger.LogInformation(
                $"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {items.Count()} items"
            );

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO itemDTO)
        {
            Item item =
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = itemDTO.Name,
                    Price = itemDTO.Price,
                    CreatedDate = DateTimeOffset.UtcNow,
                };
            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDTO itemDTO)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with { Name = itemDTO.Name, Price = itemDTO.Price };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
