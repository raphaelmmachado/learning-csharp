using Blog.Models;
using Blog.Repositories;

namespace Blog.Screens.TagScreens
{
    public static class CreateTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Escolha o nome da tag (nome 'espaço' slug)!");
            var line = Console.ReadLine();
            var arr = line?.Split(" ");
            var tagName = arr?[0];
            var slug = arr?[1].ToLower();
            Create(tagName!, slug!);
            Console.WriteLine("-------------");
            ListTagScreen.Load();
            Console.ReadKey();
        }


        private static void Create(string name, string slug)
        {
            var tag = new Tag()
            {
                Name = name,
                Slug = slug,
            };
            var repository = new Repository<Tag>();
            var id = repository.Create(tag);
            Console.WriteLine($"TAG {tag.Name} CRIADA! Id: {id}");
        }
    }
}