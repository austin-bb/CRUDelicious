#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace Dishes.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Display(Name = "Chef's Name")]
    [Required]
    [MinLength(2, ErrorMessage = "Must be at least 2 characters")]
    public string ChefName { get; set; }

    [Display(Name = "Name of dish")]
    [Required]
    [MinLength(2, ErrorMessage = "Must be at least 2 characters")]
    public string DishName { get; set; } 

    [Display(Name = "# of calories")]
    [Required]
    [Range(0, 10000, ErrorMessage = "Must be greater than 0")]
    public int Calories { get; set; }

    [Required]
    public int Tastiness { get; set; }

    [Required]
    [MinLength(10, ErrorMessage = "Must be at least 10 characters")]
    [MaxLength(100, ErrorMessage = "Must be less than 100 characters")]
    public string Description { get; set; }

    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
