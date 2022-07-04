using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
  [ApiController]
  [Route("items")]
  public class ItemsController : ControllerBase
  {
    private readonly IItemsRepository repository;

    public ItemsController(IItemsRepository repository)
    {
      this.repository = repository;
    }

    [HttpGet]
    public IEnumerable<ItemDTO> GetItems()
    {
      var items = repository.GetItems().Select(item => item.AsDto());
      return items;
    }

    [HttpGet("{id}")]
    public ActionResult<ItemDTO> GetItem(Guid id)
    {
      var item = repository.GetItem(id);

      if (item is null)
      {
        return NotFound();
      }

      return Ok(item.AsDto());
    }

    [HttpPost]
    public ActionResult<ItemDTO> CreateItem(CreateItemDTO itemDTO)
    {
      Item item = new()
      {
        Id = Guid.NewGuid(),
        Name = itemDTO.Name,
        Price = itemDTO.Price,
        CreatedDate = DateTimeOffset.UtcNow,
      };
      repository.CreateItem(item);

      return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
    }

    [HttpPut("{id}")]
    public ActionResult UpdateItem(Guid id, UpdateItemDTO itemDTO)
    {
      var existingItem = repository.GetItem(id);

      if (existingItem is null)
      {
        return NotFound();
      }

      Item updatedItem = existingItem with
      {
        Name = itemDTO.Name,
        Price = itemDTO.Price
      };

      repository.UpdateItem(updatedItem);

      return NoContent();
    }

    [HttpDelete]
    public ActionResult DeleteItem(Guid id)
    {
      var item = repository.GetItem(id);

      if (item is null)
      {
        return NotFound();
      }

      repository.DeleteItem(id);

      return NoContent();
    }
  }
}