using System.Collections.Generic;

namespace CustomCellEditLayer
{
    public class Person
    {
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public static List<Person> GetSampleData()
        {
            return new List<Person>() 
            {          
                new Person("Annie", 12),            
                new Person("John", 43),        
                new Person("Lisa", 18),        
                new Person("Benn", 22),     
                new Person("Ron", 31),       
                new Person("Kate", 24),      
                new Person("Karla", 10),
            };
        }
    }
}
