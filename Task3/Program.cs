using System;
using System.IO;

using Task2;

namespace Task3
{
    class Program
    {
        static int countFileDel = 0;
        static long sizeFileDel = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь до папки:");
            string dirName = Console.ReadLine();

            if (Directory.Exists(dirName))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                Console.WriteLine("Исходный размер папки: {0} - {1} байт", dirName, DirectoryExtension.DirSize(dirInfo));
                GetCatalog(dirInfo);
                Console.WriteLine("Удалено {0} файлов - Освобождено: {1} байт", countFileDel, sizeFileDel);
                Console.WriteLine("Текущий размер папки: {0} - {1} байт", dirName, DirectoryExtension.DirSize(dirInfo));
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

                    if (TimeSpan.FromMinutes(30) >= DateTime.Now - lastAccessTimeFile)
                    {

                    }
                    else
                    {
                        fi.Delete();
                        sizeFileDel += fi.Length;
                        countFileDel++;
                    }
                }

                subDirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo dir in subDirs)
                {
                    DateTime lastAccessTimeDir = dir.LastAccessTime;

                    if (TimeSpan.FromMinutes(30) >= DateTime.Now - lastAccessTimeDir)
                    {

                    }
                    else
                    {
                        dir.Delete(true);
                    }

                    //Рекурсия
                    GetCatalog(dir);
                }
            }
        }
    }
}