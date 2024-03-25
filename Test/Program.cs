using System;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        Придумайте простой класс, который будет предоставлять информацию об установленном в системе диске.

        Нужны свойства, чтобы хранить: имя диска, объём, свободное место.Свойства инициализируются при создании нового объекта в методе-конструкторе.

      Посмотреть ответ для самопроверки
public class Drive
        {
            public Drive(string name, long totalSpace, long freeSpace)
            {
                Name = name;
                TotalSpace = totalSpace;
                FreeSpace = freeSpace;
            }

            public string Name { get; }
            public long TotalSpace { get; }
            public long FreeSpace { get; }
        }

        Сейчас пользователь видит, что у  вас на диске все файлы лежат в одной куче.

Нужно создать папки(директории) для сортировки файлов.

Добавьте метод для создания новой директории на диске.Подумайте, какую коллекцию использовать для хранения созданных директорий.

Пусть директория (папка) будет представлена классом: 

public class Folder
        {
            public List<string> Files { get; set; } = new List<string>();
        }

        Реализация метода: 

Принимает на вход имя папки, добавляет её в коллекцию папок, выводит информацию о том, что папка создана.

Посмотреть ответ для самопроверки
Dictionary<string, Folder> Folders = new Dictionary<string, Folder>();

        public void CreateFolder(string name)
        {
            Folders.Add(name, new Folder());
        }

        Дан класс, представляющий папку(директорию) :

public class Folder
        {
            public Folder(string name)
            {
                Name = name;
            }

            string Name { get; set; }
            List<string> Files { get; set; } = new List<string>();

            void AddFile(string name)
            {
                if (!Files.Contains(name))
                    Files.Add(name);
            }
        }
        Что нужно сделать, чтобы создать папку и добавить в неё новый файл?
    }
}
Добавлю в базовый класс тип-перечисление:
DriveType
{
    USB,
  HDD,
  CD
}
===========================================================================
Для представления диска в пространстве имен System.IO имеется класс DriveInfo.

Он имеет статический метод GetDrives(), возвращающий имена всех логических дисков компьютера.

Также в классе есть много свойств, откуда вы можете получить полезную информацию: 

AvailableFreeSpace: указывает на объем доступного свободного места на диске в байтах;
DriveFormat: получает имя файловой системы;
DriveType: представляет тип диска;
IsReady: готов ли диск (например, DVD-диск может быть не вставлен в дисковод);
Name: получает имя диска;
TotalFreeSpace: получает общий объем свободного места на диске в байтах;
TotalSize: общий размер диска в байтах;
VolumeLabel: получает или устанавливает метку тома.

Попробуем получить информацию о дисках на компьютере: 

using System;
using System.IO;
namespace DriveManager
{
   class Program
   {
       static void Main(string[] args)
       {
	// получим системные диски
           DriveInfo[] drives = DriveInfo.GetDrives();
 
	// Пробежимся по дискам и выведем их свойства
           foreach (DriveInfo drive in drives)
           {
               Console.WriteLine($"Название: {drive.Name}");
               Console.WriteLine($"Тип: {drive.DriveType}");
               if (drive.IsReady)
               {
                   Console.WriteLine($"Объем: {drive.TotalSize}");
                   Console.WriteLine($"Свободно: {drive.TotalFreeSpace}");
                   Console.WriteLine($"Метка: {drive.VolumeLabel}");
               }
           }
       }
   }
}
Таким образом, класс DriveInfo предоставляет нам удобный высокоуровневый интерфейс получения информации с дисковой системы машины.

Для работы с ними в языке С#  предусмотрены два класса: Directory и DirectoryInfo.

Класс Directory предоставляет ряд статических методов для управления каталогами.  
Наиболее используемые:

CreateDirectory(path): создает каталог по указанному пути;
Delete(path): удаляет каталог по указанному пути;
Exists(path): определяет, существует ли каталог по указанному пути; 
GetDirectories(path): получает список каталогов в каталоге path;
GetFiles(path): получает список файлов в каталоге path;
Move(source, destination): перемещает каталог;
GetParent(path): получение родительского каталога.

Класс DirectoryInfo по функционалу во многом похож на Directory и позволяет нам создавать, удалять, перемещать и производить другие операции с каталогами. 
Наиболее используемые:

Create(): создает каталог;
CreateSubdirectory(path): создает подкаталог по указанному пути path;
Delete(): удаляет каталог;
Свойство Exists: определяет, существует ли каталог;
GetDirectories(): получает список каталогов;
GetFiles(): получает список файлов;
MoveTo(destDirName): перемещает каталог;
Свойство Parent: получение родительского каталога;
Свойство Root: получение корневого каталога.

 Попробуем получить все файлы и папки корневого каталога: 

namespace DirectoryManager
{
   class Program
   {
       static void Main(string[] args)
       {
           GetCatalogs(); //   Вызов метода получения директорий
       }
      
       static void GetCatalogs()
       {
           string dirName = @"/"; // Прописываем путь к корневой директории MacOS (для Windows скорее всего тут будет "C:\\")
           if (Directory.Exists(dirName)) // Проверим, что директория существует
           {
               Console.WriteLine("Папки:"); 
               string[] dirs = Directory.GetDirectories(dirName);  // Получим все директории корневого каталога
              
               foreach (string d in dirs) // Выведем их все
                   Console.WriteLine(d);
              
               Console.WriteLine();
               Console.WriteLine("Файлы:");
               string[] files = Directory.GetFiles(dirName);// Получим все файлы корневого каталога
              
               foreach (string s in files)   // Выведем их все
                   Console.WriteLine(s);
           }
       }
   }
}


Напишите метод, который считает количество файлов и папок в корне вашего диска и выводит итоговое количество объектов.

Подсказка
При правильном решении будет осуществляться либо вывод в виде целого числа (программа показала результат), либо вывод в виде сообщения об ошибке при попытке чтения информации о файлах (например, у программы нет прав доступа).
Посмотреть ответ для самопроверки
try
{
   DirectoryInfo dirInfo = new DirectoryInfo(@"/" /* Или С:\\ для Windows */ );
   if (dirInfo.Exists)
   {
       Console.WriteLine(dirInfo.GetDirectories().Length + dirInfo.GetFiles().Length);
   }
}
catch(Exception e)
{
   Console.WriteLine( e.Message);
}

Еще несколько вариантов использования
Создание новой директории в каталоге текущего пользователя (luft):

DirectoryInfo dirInfo = new DirectoryInfo(@"/Users/luft");
if (!dirInfo.Exists)
   dirInfo.Create();
 
dirInfo.CreateSubdirectory("NewFolder");

Добавьте в метод из задания 8.2.1 создание новой директории в корне вашего диска, а после вновь выведите количество элементов уже после создания нового. 

Убедитесь, что их количество увеличилось, либо корректно вывелось сообщение об ошибке (если у вас нет прав на запись).

Посмотреть ответ для самопроверки
try
{
   DirectoryInfo dirInfo = new DirectoryInfo(@"/" /* Или С:\\ для Windows */ );
   if (dirInfo.Exists)
   {
       Console.WriteLine(dirInfo.GetDirectories().Length + dirInfo.GetFiles().Length);
   }
  
   DirectoryInfo newDirectory = new DirectoryInfo(@"/newDirectory");
   if (!newDirectory.Exists)
       newDirectory.Create();
  
   Console.WriteLine(dirInfo.GetDirectories().Length + dirInfo.GetFiles().Length);
}
catch(Exception e)
{
   Console.WriteLine( e.Message);
}

Получение информации о каталоге: 

Console.WriteLine($"Название каталога: {dirInfo.Name}");
Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
При удалении каталога мы должны явно указать программе, что стоит также удалить все содержимое (файлы и подкаталоги), иначе получим ошибку.

Для этого при вызове метода Delete  передаем флаг true, что означает удаление со всем содержимым:

try
{
   DirectoryInfo dirInfo = new DirectoryInfo(@"/Users/luft/SkillFactory");
   dirInfo.Delete(true); // Удаление со всем содержимым
   Console.WriteLine("Каталог удален");
}
catch (Exception ex)
{
   Console.WriteLine(ex.Message);
}
Задание 8.2.3
Добавьте в задание 8.2.2 удаление вновь созданной директории и проверьте: теперь ваша программа не должна оставлять после себя следов!

Перемещение каталога:

DirectoryInfo dirInfo = new DirectoryInfo("/Users/luft/SkillFactory");
string newPath = "/Users/luft/SkillFactoryNew";

if (dirInfo.Exists && !Directory.Exists(newPath))
  dirInfo.MoveTo(newPath);
При этом нужно убедиться, что такой каталог в настоящий момент не существует.

Задание 8.2.4
Создайте на рабочем столе папку testFolder. Напишите метод, с помощью которого можно будет переместить её в корзину. 

Подсказка
Корзина обычно лежит в каталоге текущего пользователя.
Посмотреть ответ для самопроверки
try
{
   DirectoryInfo dirInfo = new DirectoryInfo(@"/Users/luft/Desktop/testFolder"  );
   string trashPath = "/Users/luft/.Trash/testFolder";
  
   dirInfo.MoveTo(trashPath);
}
catch(Exception e)
{
   Console.WriteLine( e.Message);
}

Что сделает данный код при запуске, если путь указан верно?


var di = new DirectoryInfo("\C:\Documents\");

foreach(FileInfo file in di.GetFiles()) 
{
  file.Delete();
}
foreach(DirectoryInfo dir in di.GetDirectories()) 
{
  dir.Delete(true);
}
удалит все файлы и директории внутри директории Documents.



File.Create: создает файл;
File.Delete: удаляет файл;
File.Copy: копирует файл;
File.Move: перемещает файл;
File.Exist(path): проверяет существование файла.

File и FileInfo — схожие по назначению классы, но между ними есть некоторые различия. Так, все методы класса File — статические, поэтому при файловых операциях с ним не нужно создавать объект. Все его методы требуют указания пути, то есть адресной ссылки внутри файловой системы.

Таким образом, File стоит использовать в большинстве случаев, когда файловая операция — одиночная.
Основные методы  класса File:

File.Create: создает файл;
File.Delete: удаляет файл;
File.Copy: копирует файл;
File.Move: перемещает файл;
File.Exist(path): проверяет существование файла.
Если необходимо выполнять много действий с одним файлом, то удобнее создать один раз объект класса FileInfo и работать через него.
Этот класс обладает более широким функционалом. Кроме аналогичных методов класса File, объект FileInfo имеет ряд полезных свойств: 

Length: получить размер файла;
Directory: получить ссылку на каталог (DirectoryInfo), в котором файл расположен;
DirectoryName: получить путь к родительскому каталогу;
Exist: проверить существование файла;
Extension: получить расширение файла;
Name и FullName: получить имя и полное имя соответственно.

Теперь рассмотрим на примере.

 Допустим, нам нужно создать файл и записать в него информацию, в коде выполним следующие действия: 

Проверим существование файла
Если не существует, создадим его и запишем строку.
Откроем файл и прочитаем ранее записанную строку.
Здесь воспользуемся классом File, так как задача одиночная.

using System;
using System.IO;
class FileWriter
{
   public static void Main()
   {
       string filePath = @"/Users/luft/SkillFactory/Students.txt"; // Укажем путь 
      if (!File.Exists(filePath)) // Проверим, существует ли файл по данному пути
      {
           //   Если не существует - создаём и записываем в строку
          using (StreamWriter sw = File.CreateText(filePath))  // Конструкция Using (будет рассмотрена в последующих юнитах)
           {
               sw.WriteLine("Олег");
               sw.WriteLine("Дмитрий");
               sw.WriteLine("Иван");
           }
       }
       // Откроем файл и прочитаем его содержимое
       using (StreamReader sr = File.OpenText(filePath))
       {
           string str = "";
           while ((str = sr.ReadLine()) != null) // Пока не кончатся строки - считываем из файла по одной и выводим в консоль
           {
               Console.WriteLine(str);
           }
       }
   }
}


сходный код программы — ещё один отличный пример текстового файла. 

Напишите программу, которая выводит свой собственный исходный код в консоль.

Посмотреть ответ для самопроверки
public static void Main()
{
   string filePath = @"/Users/luft/Projects/SkillFactory/workwithfiles/Task2/Program.cs"; // Укажем путь
 
   // Откроем файл и прочитаем его содержимое
   using (StreamReader sr = File.OpenText(filePath))
   {
       string str = "";
       while ((str = sr.ReadLine()) != null)
           Console.WriteLine(str);
   }
}

Далее рассмотрим пример аналогичной задачи, где после операций чтения/записи в файл нам необходимо работать с ним в дальнейшем. Для этого используем функционал класса FileInfo.

using System;
using System.IO;
 
class FileWriter
{
   public static void Main()
   {
       string tempFile = Path.GetTempFileName(); // используем генерацию имени файла.
       var fileInfo = new FileInfo(tempFile); // Создаем объект класса FileInfo.
 
       //Создаем файл и записываем в него.
       using (StreamWriter sw = fileInfo.CreateText())
       {
           sw.WriteLine("Игорь");
           sw.WriteLine("Андрей");
           sw.WriteLine("Сергей");
       }     
 
       //Открываем файл и читаем из него.
       using (StreamReader sr = fileInfo.OpenText())
       {
           string str = "";
           while ((str = sr.ReadLine()) != null)
           {
               Console.WriteLine(str);
           }
       }
 
       try
       {
           string tempFile2 = Path.GetTempFileName();
           var fileInfo2 = new FileInfo(tempFile2);
 
           // Убедимся, что файл назначения точно отсутствует
           fileInfo2.Delete();
 
           // Копируем информацию
           fileInfo.CopyTo(tempFile2);
           Console.WriteLine($"{tempFile} скопирован в файл {tempFile2}."); 
           //Удаляем ранее созданный файл.
           fileInfo.Delete();
           Console.WriteLine($"{tempFile} удален.");       }
       catch (Exception e)
       {
           Console.WriteLine($"Ошибка: {e}");
       }
   }
}
Мы создаем временный файл и пишем в него 3 строки с именами. 
Далее построчно читаем из него и выводим на консоль. И, наконец,
используя уже имеющийся объект класса FileInfo, копируем информацию в
новый файл и удаляем старый.



Сделайте так, чтобы ваша программа из задания 8.3.1 при каждом запуске добавляла в свой исходный код комментарий о времени последнего запуска. 

Для этого самостоятельно изучите документацию класса FileInfo и найдите метод, который позволяет добавлять строки в уже существующий файл. 

Подсказка
Получить текущую дату можно с помощью свойства DateTime.Now.
Посмотреть ответ для самопроверки
using System;
using System.IO;
 
class Task2
{
   public static void Main()
   {
       var fileInfo = new FileInfo("/Users/luft/Projects/SkillFactory/workwithfiles/Task2/Program.cs");
      
       using (StreamWriter sw = fileInfo.AppendText())
       {
           sw.WriteLine($"// Время запуска: {DateTime.Now}");
       }
 
       using (StreamReader sr = fileInfo.OpenText())
       {
           string str = "";
           while ((str = sr.ReadLine()) != null)
               Console.WriteLine(str);
      
       }
   }

StreamReader r = new StreamReader(path);
Console.WriteLine(r.ReadLine());
r.Dispose();
Но в этом случае вы должны вызывать Dispose() самостоятельно.

Чтобы упростить этот процесс в C# предусмотрена более удобная конструкция, которая может это делать за нас автоматически — Using.

Перепишем код выше, использовав её: 

using (StreamReader r = new StreamReader(path))
{
   Console.WriteLine(r.ReadLine());
}
Здесь даже из кода наглядно понятно, что объект StreamReader (назовем его ресурсом) существует ровно столько, сколько нам необходимо для его использования в нижеследующем блоке. 

При использовании данной конструкции в конце блока using метод Dispose() будет вызван автоматически.











Если вы записали двоичные данные в файл через BinaryWriter, для считывания стоит применять  BinaryReader.

Рассмотрим на примере: 

using System;
using System.IO;
 
class BinaryExperiment
{
   const string SettingsFileName = "Settings.cfg";
 
   static void Main()
   {
       // Пишем
       WriteValues();
       // Считываем
       ReadValues();
   }
 
   static void WriteValues()
   {
       // Создаем объект BinaryWriter и указываем, куда будет направлен поток данных
       using (BinaryWriter writer = new BinaryWriter(File.Open(SettingsFileName, FileMode.Create)))
       {
           // записываем данные в разном формате
           writer.Write(20.666F);
           writer.Write(@"Тестовая строка");
           writer.Write(55);
           writer.Write(false);
       }
   }
 
   static void ReadValues()
   {
       float FloatValue;
       string StringValue;
       int IntValue;
       bool BooleanValue;
 
       if (File.Exists(SettingsFileName))
       {
           // Создаем объект BinaryReader и инициализируем его возвратом метода File.Open.
           using (BinaryReader reader = new BinaryReader(File.Open(SettingsFileName, FileMode.Open)))
           {
               // Применяем специализированные методы Read для считывания соответствующего типа данных.
               FloatValue = reader.ReadSingle();
               StringValue = reader.ReadString();
               IntValue = reader.ReadInt32();
               BooleanValue = reader.ReadBoolean();
           }
 
           Console.WriteLine("Из файла считано:");
 
           Console.WriteLine("Дробь: " + FloatValue);
           Console.WriteLine("Строка: " + StringValue);
           Console.WriteLine("Целое: " + IntValue);
           Console.WriteLine("Булево значение " + BooleanValue);
       }
   }
}



Задание 8.4.2
Запишите  в файл из предыдущего задания информацию о доступе к нему с вашей машины. 

Пример вывода, который должен получиться: 

Файл изменен 02.11 14:53 на компьютере Windows 11
Посмотреть ответ для самопроверки
public static void Main()
{
   WriteValues();
   ReadValues();
}
  
static void WriteValues()
{
   using (BinaryWriter writer = new BinaryWriter(File.Open("BinaryFile.bin", FileMode.Open)))
       writer.Write($"Файл изменен {DateTime.Now} на компьютере c ОС {Environment.OSVersion}");
}
 
static void ReadValues()
{
   string StringValue;
 
   if (File.Exists("BinaryFile.bin"))
   {
       using (BinaryReader reader = new BinaryReader(File.Open("BinaryFile.bin", FileMode.Open)))
       {
           StringValue = reader.ReadString();
       }
 
       Console.WriteLine(StringValue);
   }
}


Теперь, когда мы рассмотрели, как сохранять и считывать информацию с текстовых и бинарных файлов, нужно определиться, как эти данные преобразовывать для удобного представления в вашей программе.

Для этого С# предоставляет механизм бинарной сериализации. Она служит для преобразования объекта в поток байтов для последующей удобной записи в файл или хранения в памяти. 

Впоследствии можно выполнить обратный процесс — десериализацию (преобразовать массив байт в ранее сохраненный объект).

Чтобы объект определенного класса можно было сериализовать, этот класс должен иметь  атрибут Serializable:

[Serializable] //   Атрибут сериализации
class Person
{
//     Простая модель класса 
   public string Name { get; set; }
   public int Year { get; set; }
 
// Метод - конструктор
   public Person(string name, int year)
   {
       Name = name;
       Year = year;
   }
}
Если вам нужно, чтобы определенное поле или свойство при сериализации игнорировалось, его вы помечаете атрибутом NonSerialized.

Попробуем применить это на практике: 

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Serialization
{
 
// Описываем наш класс и помечаем его атрибутом для последующей сериализации   
[Serializable]
   class Pet
   {
       public string Name { get; set; }
       public int Age { get; set; }
       public Pet(string name, int age)
       {
           Name = name;
           Age = age;
       }
   }
   
   class Program
   {
       static void Main(string[] args)
       {
           // объект для сериализации
           var person = new Pet("Rex", 2);
           Console.WriteLine("Объект создан");
          
           BinaryFormatter formatter = new BinaryFormatter();
           // получаем поток, куда будем записывать сериализованный объект
           using (var fs = new FileStream("myPets.dat", FileMode.OpenOrCreate))
           {
               formatter.Serialize(fs, person);
               Console.WriteLine("Объект сериализован");
           }
           // десериализация
           using (var fs = new FileStream("myPets.dat", FileMode.OpenOrCreate))
           {
               var newPet = (Pet)formatter.Deserialize(fs);
               Console.WriteLine("Объект десериализован");
               Console.WriteLine($"Имя: {newPet.Name} --- Возраст: {newPet.Age}");
           }
           Console.ReadLine();
       }
   }
}
Вывод в консоль:

Наш простейший класс Pet объявлен с атрибутом Serializable. Это делает возможной сериализацию его объектов. 

В этом примере мы последовательно выполняем операции сериализации и десериализации, для которых нам необходим поток, в который нужно либо сохранять, либо из которого считывать данные.

Поток предоставлен объектом FileStream, с помощью которого мы пишем нужный нам объект Pet в файл myPets.dat. Сериализация с помощью одного метода formatter.Serialize(fs, pet) добавляет все данные об объекте Pet в файл myPets.dat, затем идёт приведение к типу Pet.

Как видно из примера, при использовании сериализации процесс сохранения объектов в бинарном формате значительно проще, чем с использованием связки классов BinaryWriter/BinaryReader.

Хотя в данном примере мы взяли лишь один объект Person, точно так же мы можем использовать и любую коллекцию подобных объектов.

В примерах выше мы рассмотрели основные операции с директориями и файлами. Как вы уже знаете, операции записи/чтения из файла называются операциями ввода-вывода, и поддержка их реализована в пространстве имен System.IO. 
Задание 8.4.3
Дан класс:

public string Name { get; set; }
public long PhoneNumber { get; set; }
public string Email { get; set; }
 
public Contact(string name, long phoneNumber, string email)
{
   Name = name;
   PhoneNumber = phoneNumber;
   Email = email;
}
Доработайте его и сериализуйте в бинарный формат.

Посмотреть ответ для самопроверки
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
 
[Serializable] // Для сериализации студент должен был добавить в класс этот атрибут
class Contact
{
   public string Name { get; set; }
   public long PhoneNumber { get; set; }
   public string Email { get; set; }
   public Contact(string name, long phoneNumber, string email)
   {
       Name = name;
       PhoneNumber = phoneNumber;
       Email = email;
   }
}
 
class Serializer
{
   static void Main(string[] args)
   {
       // создание объекта класса
       var contact = new Contact("Евгений", 79991234567, "example@example.com");
      
       // сериализация
       BinaryFormatter formatter = new BinaryFormatter();
       using (var fs = new FileStream("Contact.bin", FileMode.OpenOrCreate))
       {
           formatter.Serialize(fs, contact);
       }
   }
}