using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
public class User
{
    [Key]
    [Required]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
    public string username { set; get; }
    [Required]
    public string password { set; get; }
    [Required]
    public DateTime createAt { set; get; } = DateTime.Now;

    [JsonIgnore]
    public ICollection<Product>? Products { get; set; }
}
