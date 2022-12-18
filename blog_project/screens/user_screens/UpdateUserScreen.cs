using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.UserScreens
{
    public static class UpdateUserScreen
    {
        public static void Load()
        {
            Console.WriteLine("Passe o id");
            int id;
            ConsoleKeyInfo UserInput = Console.ReadKey();
            if (char.IsDigit(UserInput.KeyChar))
            {
                Console.WriteLine();
                id = int.Parse(UserInput.KeyChar.ToString());
            }
            else return;
            Console.WriteLine("Digite o nome de usuario");
            var userName = Console.ReadLine();
            Console.WriteLine("Digite o email do usuario");
            var userEmail = Console.ReadLine();
            Console.WriteLine("Digite a hash do usuario");
            var userHash = Console.ReadLine();
            Console.WriteLine("Digite a bio do usuario");
            var userBio = Console.ReadLine();
            Console.WriteLine("Digite o link da imagem do usuario");
            var userImage = Console.ReadLine();
            Console.WriteLine("Digite o slug do usuario");
            var userSlug = Console.ReadLine();
            Console.WriteLine("-------------");
            if (userName == null || userEmail == null || userHash == null || userBio == null || userImage == null || userSlug == null) return;
            Update(id, userName, userEmail, userHash, userBio, userImage, userSlug);
            Console.ReadKey();
            MenuUserScreen.Load();
        }
        private static void Update(int id, string name, string email, string hash, string bio, string image, string slug)
        {
            var user = new User()
            {
                Id = id,
                Name = name,
                Email = email,
                PasswordHash = hash,
                Bio = bio,
                Image = image,
                Slug = slug,
            };
            try
            {
                var repository = new Repository<User>();
                var updated = repository.Update(user);
                if (updated)
                    Console.WriteLine($"Usuario de Id: {user.Id} e nome: {user.Name} Atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar");
                Console.WriteLine(ex.Message);
            }
        }
    }
}