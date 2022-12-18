using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.UserScreens
{
    public static class CreateUserScreen
    {
        public static void Load()
        {
            Console.Clear();
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

            if (userName == null ||
             userEmail == null ||
             userHash == null ||
             userBio == null ||
             userImage == null ||
             userSlug == null) return;
            else
            {
                var user = CreateObject(userName, userEmail, userHash, userBio, userImage, userSlug);
                Create(user);
            }
            Console.ReadKey();
            MenuUserScreen.Load();
        }


        private static void Create(User user)
        {
            try
            {
                var repository = new Repository<User>();
                var id = repository.Create(user);
                Console.WriteLine($"usuario: {user.Name} - email: {user.Email} CRIADA! Id: {id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nao foi possivel criar usuario");
                Console.WriteLine(ex.Message);
            }


        }
        public static User CreateObject(string name, string email, string hash, string bio, string image, string slug)
        {
            var user = new User()
            {
                Name = name,
                Email = email,
                PasswordHash = hash,
                Bio = bio,
                Image = image,
                Slug = slug,
            };
            return user;
        }
        public static bool CheckInput(string name, string email, string hash, string bio, string image, string slug)
        {
            if (name == null || email == null || hash == null || bio == null || image == null || slug == null) return false;
            else return true;

        }
    }
}