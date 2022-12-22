using System.ComponentModel.DataAnnotations;
namespace Shop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "campo obrigatorio")]
        [MaxLength(20, ErrorMessage = "entre 3 e 20 char")]
        [MinLength(3, ErrorMessage = "entre 3 e 20 char")]
        public string? Username { get; set; }


        [Required(ErrorMessage = "campo obrigatorio")]
        [MaxLength(20, ErrorMessage = "entre 3 e 20 char")]
        [MinLength(3, ErrorMessage = "entre 3 e 20 char")]
        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}