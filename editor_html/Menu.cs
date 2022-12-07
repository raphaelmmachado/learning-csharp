namespace EditorHTML
{
    public static class Menu
    {
        public static void Show()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            DrawScreen();
            WriteOptions();
            var option = short.Parse(Console.ReadLine()!);
            HandleMenuOption(option);
        }
        static void WriteOptions()
        {
            Console.SetCursorPosition(3, 1);
            Console.WriteLine("Editor Html");
            Console.SetCursorPosition(3, 2);
            Console.WriteLine("=========");
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("Selecione uma opcao abaixo");
            Console.SetCursorPosition(3, 4);
            Console.WriteLine("1 - Novo Arquivo");
            Console.SetCursorPosition(3, 5);
            Console.WriteLine("2 - Abrir");
            Console.SetCursorPosition(3, 7);
            Console.WriteLine("0 - Sair");
            Console.SetCursorPosition(3, 8);
            Console.WriteLine($"Opcao:");
            Console.SetCursorPosition(10, 8);

        }
        static void DrawScreen()
        {
            Draw("+", 0);
            Draw("-", 30);
            Draw("+", 0);
            BreakLine();
            DrawBar();
            Draw("+", 0);
            Draw("-", 30);
            Draw("+", 0);
            BreakLine();
        }

        public static void Draw(string character, int times)
        {
            for (int i = 0; i <= times; i++)
            {
                Console.Write(character);
            }
        }
        public static void BreakLine()
        {
            Console.Write("\n");
        }
        public static void DrawBar()
        {
            for (int i = 0; i <= 10; i++)
            {
                Console.Write("|");
                Draw(" ", 30);
                Console.Write("|");
                BreakLine();
            }
        }
        public static void HandleMenuOption(short option)
        {
            switch (option)
            {
                case 1: Editor.Show(); break;
                case 2: Console.WriteLine("Html"); break;
                case 0:
                    {
                        Console.Clear();
                        Environment.Exit(0);
                        break;
                    }
                default: Show(); break;

            }
        }
    }
}