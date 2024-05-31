using System;
using System.IO;

namespace SubdirectoryFinder
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 1 && (args[0] == "/help" || args[0] == "/?"))
            {
                PrintHelp();
                return 0;
            }

            if (args.Length < 2)
            {
                PrintSyntaxError();
                return 1;
            }

            string targetDirectory = args[0];
            string targetSubdirectory = args[1];

            if (!Directory.Exists(targetDirectory))
            {
                Console.WriteLine("Помилка: Вказаний каталог не існує.");
                return 1;
            }

            if (!targetDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                targetDirectory += Path.DirectorySeparatorChar;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(targetDirectory);

            if ((dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
            {
                Console.WriteLine("Помилка: Вказаний каталог прихований.");
                return 1;
            }
            if ((dirInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                Console.WriteLine("Помилка: Вказаний каталог має атрибут тільки для читання.");
                return 1;
            }
            if ((dirInfo.Attributes & FileAttributes.Archive) == FileAttributes.Archive)
            {
                Console.WriteLine("Помилка: Вказаний каталог архівний.");
                return 1;
            }

            DirectoryInfo[] subdirs = dirInfo.GetDirectories();
            foreach (DirectoryInfo subdir in subdirs)
            {
                if (subdir.Name.Equals(targetSubdirectory, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Знайдено підкаталог з вказаним ім'ям: {subdir.FullName}");
                    return 0;
                }
            }

            Console.WriteLine($"Підкаталог з ім'ям \"{targetSubdirectory}\" не знайдено.");
            return 1;
        }

        static void PrintHelp()
        {
            Console.WriteLine("Помилка: Неправильний синтаксис.");
            Console.WriteLine();
            Console.WriteLine("Довідка: Використання командного рядка:");
            Console.WriteLine("  SubdirectoryFinder.exe [шлях_до_каталогу] [ім'я_підкаталогу]");
            Console.WriteLine();
            Console.WriteLine("  Шлях_до_каталогу - обов'язковий параметр, шлях до каталогу, у якому шукається підкаталог.");
            Console.WriteLine("  Ім'я_підкаталогу   - обов'язковий параметр, ім'я підкаталогу, яке потрібно знайти.");
            Console.WriteLine();
            Console.WriteLine("Приклади використання:");
            Console.WriteLine("  SubdirectoryFinder.exe C:\\Назва_каталогу Назва_підкаталога");
        }

        static void PrintSyntaxError()
        {
            Console.WriteLine("Помилка: Неправильний синтаксис.");
            Console.WriteLine();
            Console.WriteLine("Довідка: Використання командного рядка:");
            Console.WriteLine("  SubdirectoryFinder.exe [шлях_до_каталогу] [ім'я_підкаталогу]");
            Console.WriteLine();
            Console.WriteLine("  Шлях_до_каталогу - обов'язковий параметр, шлях до каталогу, у якому шукається підкаталог.");
            Console.WriteLine("  Ім'я_підкаталогу   - обов'язковий параметр, ім'я підкаталогу, яке потрібно знайти.");
            Console.WriteLine();
            Console.WriteLine("Приклади використання:");
            Console.WriteLine("  SubdirectoryFinder.exe C:\\Назва_каталогу Назва_підкаталога");
        }
    }
}
