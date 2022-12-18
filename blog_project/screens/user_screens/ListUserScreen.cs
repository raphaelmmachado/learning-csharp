using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.UserScreens
{
    public static class ListUserScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Lista de usuarios");
            Console.WriteLine("-------------");
            List();
            Console.ReadKey();
            MenuUserScreen.Load();
        }
        private static void List()
        {
            var repository = new Repository<User>();
            var users = repository.Get();
            foreach (var item in users)
            {
                Console.WriteLine($"{item.Id} user: {item.Name} email:({item.Email})");
            }
        }
    }
}