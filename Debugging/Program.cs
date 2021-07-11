using System;
using System.Text.Json;

namespace Debugging
{
    public class Program
    {
        public static void Main()
        {
            Student student  = new Student
            {
                FirstName = "Artur",
                SecondName = "Denisov",
                BirthDate = new DateTime(1987,12,21)
            };

            string jsonString = JsonSerializer.Serialize(student);

            Console.WriteLine(jsonString);
        }
    }
}
