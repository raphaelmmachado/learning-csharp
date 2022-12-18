using Dapper.Contrib.Extensions;

namespace Blog.Models
{
    // O Dapper contrib busca na database o nome da classe no plural,
    // entao se na database o nome nao estiver no plural
    // nós que dizer pra ele buscar o nome correto
    // interface eu que testei
    [Table("[User]")]
    public class User
    {
        // boa prática sempre instaciar listas no metodo construtor.
        public User()
        => Roles = new List<Role>();

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public string? Slug { get; set; }

        [Write(false)]
        public List<Role>? Roles { get; set; }
    }
}