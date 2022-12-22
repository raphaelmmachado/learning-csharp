using System.ComponentModel.DataAnnotations;
namespace Shop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "campo obrigatorio")]
        [MaxLength(60, ErrorMessage = "entre 3 e 60 char")]
        [MinLength(3, ErrorMessage = "entre 3 e 60 char")]
        public string? Title { get; set; }

        [MaxLength(1024, ErrorMessage = "max 1024 char")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "obrigatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "O preco deve ser maior que zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "obrigatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "invalida")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

    }
}