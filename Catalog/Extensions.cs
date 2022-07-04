using Catalog.DTOs;
using Catalog.Entities;

namespace Catalog
{
  public static class Extensions
  {
    public static ItemDTO AsDto(this Item item)
    {
      return new ItemDTO
      {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        CreatedDate = item.CreatedDate
      };
    }
  }
}