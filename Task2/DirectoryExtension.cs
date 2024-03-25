using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Task2
{
   public static class DirectoryExtension
    {
        public static long DirSize(DirectoryInfo d) 
        {
            long size = 0;

            FileInfo[] fis = d.GetFiles();
            foreach(FileInfo fi in fis)
            {
                size += fi.Length;
            }

            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
