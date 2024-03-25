using System;
using System.IO;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь до папки:");
            string dirName = Console.ReadLine();

            if (Directory.Exists(dirName))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                GetCatalog(dirInfo);

                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Размер папки на диске: {0} - {1} байт", dirName, DirectoryExtension.DirSize(dirInfo));
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("Такой директории не существует!");
            }
        }
        static void GetCatalog(DirectoryInfo dirInfo)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            //Получаем все файлы в текущем каталоге.
            try
            {
                files = dirInfo.GetFiles();
            }
            catch (UnauthorizedAccessException e)  //Недостаточно привелегий для доступа к файлу или папке.
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);     //Заданная папка не найдена по указанному пути.
            }

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    Console.WriteLine("Файл:");
                    Console.WriteLine(fi.FullName);
                    Console.WriteLine(fi.Name + $" - {fi.Length}байт");
                }

                subDirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo dir in subDirs)
                {
                    Console.WriteLine("Папка:");
                    Console.WriteLine(dir.FullName);
                    Console.WriteLine(dir.Name + $" - {DirectoryExtension.DirSize(dir)}байт");

                    //Рекурсия
                    GetCatalog(dir);
                }
            }
        }
    }
}