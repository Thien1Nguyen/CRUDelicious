#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required]
    public string Name { get; set; } 
    [Required]
    public string Chef { get; set; }
    [Required]
    [Range(1,5)]
    public int Tasteiness { get; set; }
    [Required]
    [CaloriesVal]
    public int Calories {get; set;}
    [Required]
    public string Description {get; set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class CaloriesValAttribute : ValidationAttribute
{
    // Call upon the protected IsValid method
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {

        if (value == null || (int)value < 0)
        {
            // we return an error message in ValidationResult we want to render    
            return new ValidationResult("Please Enter a Valid Amount of Calories");
        }
        else
        {
            // Otherwise, we were successful and can report our success  
            return ValidationResult.Success;
        }
    }
}
