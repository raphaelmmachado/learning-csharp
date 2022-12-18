using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.UserScreens
{
    public static class DeleteUserScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Passe o Id do usuario para ser deletado!");
            int id;
            ConsoleKeyInfo UserInput = Console.ReadKey();
            if (char.IsDigit(UserInput.KeyChar))
            {
                id = int.Parse(UserInput.KeyChar.ToString());
                Delete(id);
            }
            else return;
            Console.ReadKey();
            MenuUserScreen.Load();
        }
        private static void Delete(int id)
        {
            var repository = new Repository<User>();
            var deleted = repository.Delete(id);
            if (deleted)
            {
                Console.WriteLine();
                Console.WriteLine($"Usuario de Id: {id} EXCLUIDO!");
            }
        }
    }
}