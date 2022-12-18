using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.TagScreens
{
    public static class DeleteTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Passe o Id da tag para ser deletada!");
            int id;
            ConsoleKeyInfo UserInput = Console.ReadKey();
            if (char.IsDigit(UserInput.KeyChar))
            {
                id = int.Parse(UserInput.KeyChar.ToString());
                Delete(id);
            }
            else return;
            Console.ReadKey();
        }
        private static void Delete(int id)
        {
            var repository = new Repository<Tag>();
            var deleted = repository.Delete(id);
            if (deleted)
            {
                Console.WriteLine();
                Console.WriteLine($"Tag de Id: {id} EXCLUIDA!");
            }
        }
    }
}