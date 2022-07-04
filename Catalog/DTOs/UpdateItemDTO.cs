using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{
  public class UpdateItemDTO
  {
    [Required]
    public string? Name { get; init; }
    [Required]
    [Range(1, 1000)]
    public decimal Price { get; init; }
  }
}