using System;
using System.Text.RegularExpressions;
namespace EditorHTML
{
    public class Viewer
    {
        public static void Show(string text)
        {
            Console.Clear();
            Console.WriteLine("Modo vizualização");
            Console.WriteLine("-----------");
            Replace(text);
            Console.WriteLine("-----------");
            Console.ReadKey();
            Menu.Show();
        }
        public static void Replace(string text)
        {
            var strong = new Regex(@"<\s*strong[^>]*>(.*?)<\s*/\s*strong>");
            var words = text.Split(" ");

            for (int i = 0; i < words.Length; i++)
            {
                if (strong.IsMatch(words[i]))
                {
                    Console.Write(
                    words[i].Substring(
                        words[i].IndexOf(">") + 1,
                        (words[i].LastIndexOf("<") - 1) -
                        words[i].IndexOf(">")
                    ));
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(words[i]);
                    Console.Write(" ");
                }
            }
        }
    }
}