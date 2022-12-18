using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.TagScreens
{
    public static class UpdateTagScreen
    {
        public static void Load()
        {
            Console.WriteLine("Passe o id");
            int id;
            ConsoleKeyInfo UserInput = Console.ReadKey();
            if (char.IsDigit(UserInput.KeyChar))
            {
                id = int.Parse(UserInput.KeyChar.ToString());
            }
            else return;
            Console.WriteLine();
            Console.WriteLine("Passe nome e slug");
            var line = Console.ReadLine();
            var arr = line?.Split(" ");
            var tagName = arr?[0];
            var slug = arr?[1].ToLower();
            Update(id, tagName!, slug!);
        }
        private static void Update(int id, string name, string slug)
        {
            var tag = new Tag()
            {
                Id = id,
                Name = name,
                Slug = slug,
            };
            var repository = new Repository<Tag>();
            var updated = repository.Update(tag);
            if (updated)
                Console.WriteLine($"Tag de Id: {tag.Id} e nome: {tag.Name} Atualizada com sucesso!");
        }
    }
}