using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Product
{
    [Key]
    [Required]
    public string Id { set; get; } = Guid.NewGuid().ToString("N");

    [Required]
    [StringLength(255, MinimumLength = 10, ErrorMessage = "Name must be between 10 and 255 characters.")]
    public string name { set; get; }

    [Required]
     [Range(100000, 1000000, ErrorMessage = "Price must be between 100.000VND and 1000.0000VND")]
    public int price { set; get; }

    [Required]
    [Range(0, 10000, ErrorMessage = "Quantity must be between 0 and 10000.")]
    public int quantity { set; get; }

    [Required]
    [StringLength(1024, MinimumLength = 255, ErrorMessage = "Description must be between 255 and 1024 characters.")]
    public string description { set; get; }

    public string? CreatedById { get; set; }

    [JsonIgnore]
    [ForeignKey("CreatedById")]
    public User? CreatedBy { get; set; }
}
