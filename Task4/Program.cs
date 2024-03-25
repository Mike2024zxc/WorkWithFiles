using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Task4
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Укажите путь до файла (bin):");
            string dirName = Console.ReadLine();

            if (File.Exists(dirName))
            {
                CreatDir();
                ReadValues(dirName);
            }
            else
            {
                Console.WriteLine("Такой директории не существует!");
            }
        }

        static void CreatDir()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\User\Desktop");
            if (!dirInfo.Exists)
                dirInfo.Create();
            dirInfo.CreateSubdirectory("Student");
        }

        static void ReadValues(string dirName)
        {
            string Name;
            string Group;
            long DataOfBirth;
            decimal Number;

            if (File.Exists(dirName))
            {

                // Создаем объект BinaryReader и инициализируем его возвратом метода File.Open.
                using (BinaryReader reader = new BinaryReader(File.Open(dirName, FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        // Применяем специализированные методы Read для считывания соответствующего типа данных.
                        Name = reader.ReadString();
                        Group = reader.ReadString();
                        DataOfBirth = reader.ReadInt64();
                        Number = reader.ReadDecimal();

                        Console.WriteLine("Из файла считано:");

                        Console.WriteLine("Имя: " + Name);
                        Console.WriteLine("Группа: " + Group);
                        Console.WriteLine("Дата рождения: " + DataOfBirth);
                        Console.WriteLine("Средний балл: " + Number);

                        var student = new Student(Name, Group, DataOfBirth, Number);
                        string PathGroupName = "C:\\Users\\User\\Desktop\\Student\\" + student.Group + ".txt";

                        DateTime ds = new DateTime(student.DataOfBirth);

                        if (!File.Exists(PathGroupName))
                        {
                            using (File.CreateText(PathGroupName))
                            {
                            }
                        }

                        var fileInfo = new FileInfo(PathGroupName);
                        //Создаем файл и записываем в него.
                        using (StreamWriter sw = fileInfo.AppendText())
                        {
                            sw.WriteLine("{0},{1},{2}", student.Name, ds, student.Number);
                        }
                    }

                }
            }
        }

    }
}
