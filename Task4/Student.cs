using System;
using System.Collections.Generic;
using System.Text;

namespace Task4
{
    //[Serializable]
    class Student
    {
        public string Name { get; set;}
        public string Group { get; set;}
        public long DataOfBirth { get; set;}
        public decimal Number { get; set;}
        public Student(string name, string group, long dataOfBirth, decimal number)
        {
            Name = name;
            Group = group;
            DataOfBirth = dataOfBirth;
            Number = number;
        }
    }
}
