using System.ComponentModel.DataAnnotations;
public class User
{
    [Key]
    [Required]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Name must be between 5 and 50 characters.")]
    public string? name { set; get; }
    [Required]
    public string? password { set; get; }
    [Required]
    public DateTime createAt { set; get; } = DateTime.Now;
}
