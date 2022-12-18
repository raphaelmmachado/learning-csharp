// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using Blog.Models;
using Blog.Repositories;
using Blog.Screens.TagScreens;
using Blog.Screens.UserScreens;


namespace Blog
{
    class Program
    {
        private const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#t;TrustServerCertificate=True;Encrypt=True";
        static void Main(string[] args)
        {
            Database.Connection = new SqlConnection(CONNECTION_STRING);
            Database.Connection.Open();
            Load();
            Database.Connection.Close();
            Console.ReadKey();
            //USER
            // CreateUser(connection, "Teta", "teta@teta.com", "123456", "dog com tetas", "https://", "teta");
            // UpdateUser(connection, 7, "Thiago", "thiago@gmail.com", "123456", "gamer", "https://", "thiago");
            // DeleteUser(connection, 6);
            // ReadUsers(connection);
            // ReadUsersWithRoles(connection);
            //ROLES
            // CreateRole(connection);
            // ReadRoles(connection);

            //TAGS
            // ReadTags(connection);


        }

        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine();
            Console.WriteLine("1 - Gestão de usuário");
            Console.WriteLine("2 - Gestão de perfil");
            Console.WriteLine("3 - Gestão de categoria");
            Console.WriteLine("4 - Gestão de tag");
            Console.WriteLine("5 - Vincular perfil/usuário");
            Console.WriteLine("6 - Vincular post/tag");
            Console.WriteLine("7 - Relatórios");
            Console.WriteLine();
            Console.WriteLine();

            var option = short.Parse(Console.ReadLine()!);

            switch (option)
            {
                case 1:
                    MenuUserScreen.Load();
                    break;
                case 2:

                    break;
                case 3:
                    // MenuCategoryScreen.Load();
                    break;
                case 4:
                    MenuTagScreen.Load();
                    break;
                default:; break;
            }
        }
        public static void ReadUsers()
        {
            var repository = new Repository<User>();
            var users = repository.Get();
            Console.WriteLine("TODOS OS USUARIOS:");
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id} - {user.Name}");
            }
        }

        public static void ReadUser(int id)
        {
            var repository = new Repository<User>();
            var user = repository.Get(id);
            Console.WriteLine($"Usuario: {user.Name} ENCONTRADO!");
        }
        public static void CreateUser(string name, string email, string hash, string bio, string image, string slug)
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
            var repository = new Repository<User>();
            var id = repository.Create(user);
            Console.WriteLine($"USUARIO {user.Name} CRIADO! Id: {id}");
        }
        public static void ReadUsersWithRoles()
        {
            var repository = new UserRepository();
            var items = repository.GetWithRoles();
            foreach (var item in items!)
            {
                Console.WriteLine($"name: {item.Name}");
                foreach (var role in item.Roles!)
                {
                    Console.WriteLine($"name: {item.Name} - role: {role.Name}");
                }
            }
        }
        public static void ReadRoles()
        {
            var repository = new Repository<Role>();
            var items = repository.Get();
            Console.WriteLine("TODOS OS ROLES:");
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id} - {item.Name}");
            }
        }
        public static void ReadTags()
        {
            var repository = new Repository<Tag>();
            var items = repository.Get();
            Console.WriteLine("TODOS AS TAGS:");
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id} - {item.Name}");
            }
        }
        public static void UpdateUser(
         int id,
         string name,
         string email,
         string hash,
         string bio,
         string image,
         string slug)
        {
            // update precisa passar o Id
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
            var repository = new Repository<User>();
            var updated = repository.Update(user);
            if (updated)
                Console.WriteLine($"Usuario de Id: {user.Id} e nome: {user.Name} Atualizado com sucesso!");
        }
        public static void DeleteUser(int id)
        {
            var repository = new Repository<User>();
            var deleted = repository.Delete(id);
            if (deleted)
                Console.WriteLine($"Usuario de Id: {id} EXCLUIDO com sucesso!");
        }


        // public static void CreateRole(SqlConnection connection)
        // {
        //     var role = new Role()
        //     {
        //         Name = "Author",
        //         Slug = "author"
        //     };
        //     var repository = new RoleRepository(connection);
        //     repository.Create(role);
        // }
    }
}