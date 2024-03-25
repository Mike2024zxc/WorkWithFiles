using System;
using System.IO;

namespace Task1
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
                    DateTime lastAccessTimeFile = fi.LastAccessTime;

                    TimeSpan rezFile = DateTime.Now - lastAccessTimeFile;

                    if (TimeSpan.FromMinutes(30) >= DateTime.Now - lastAccessTimeFile)
                    {
                        Console.WriteLine("Файл:");
                        Console.WriteLine(fi.FullName + " - ФАЙЛ НЕ УДАЛЁН");
                        Console.WriteLine("Время доступа к файлу: - {0} Время сейчас: - {1}", fi.LastAccessTime, DateTime.Now);
                        Console.WriteLine("Разница по времени - {0}", rezFile);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Файл:");
                        fi.Delete();
                        Console.WriteLine(fi.FullName + " - ФАЙЛ УДАЛЕН");
                        Console.WriteLine("Время доступа к файлу: - {0} Время сейчас: - {1}", fi.LastAccessTime, DateTime.Now);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                subDirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo dir in subDirs)
                {
                    DateTime lastAccessTimeDir = dir.LastAccessTime;

                    TimeSpan rezDir = DateTime.Now - lastAccessTimeDir;

                    if (TimeSpan.FromMinutes(30) >= DateTime.Now - lastAccessTimeDir)
                    {
                        Console.WriteLine("Папка:");
                        Console.WriteLine(dir.FullName + " - ПАПКА НЕ УДАЛЕНА");
                        Console.WriteLine("Время доступа к папке: - {0} Время сейчас: - {1}", dir.LastAccessTime, DateTime.Now);
                        Console.WriteLine("Разница по времени - {0}", rezDir);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Папка:");
                        dir.Delete(true);
                        Console.WriteLine(dir.FullName + " - ПАПКА УДАЛЕНА");
                        Console.WriteLine("Время доступа к папке: - {0} Время сейчас: - {1}", dir.LastAccessTime, DateTime.Now);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    //Рекурсия
                    GetCatalog(dir);
                }
            }
        }
    }
}
