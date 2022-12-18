using Dapper.Contrib.Extensions;

namespace Blog.Models
{
    // O Dapper contrib busca na database o nome da classe no plural,
    // entao se na database o nome nao estiver no plural
    // nós que dizer pra ele buscar o nome correto

    [Table("[Tag]")]
    public class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }

    }
}