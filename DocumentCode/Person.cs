using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCode
{
    [Document("This is a human being")]
    internal class Person
    {

        [Document("Refers to how long this person has been on earth", "Takes in an integer")]
        public int Age { get; set; }

        [Document("Obvious biological traits associated with this person", "Takes in an enum")]
        public GenderEnum Gender { get; set; }

        public Person()
        {

        }

        public Person(int age, GenderEnum gender)
        {
            Age = age;
            Gender = gender;

        }

        [Document("Provides valid gender options a person can be")]
        internal enum GenderEnum
        {
            Male,
            Female
        }

        [Document("Makes a rather obvious sentence with the parameters provided", "Age and gender", "Outputs a sentence, otherwise known as string")]
        public void MakeSentenceWithPerson(int age, GenderEnum gender)
        {
            this.Gender = gender;
            this.Age = age;

            if (GenderEnum.Male == gender) Console.WriteLine("We have a {0} year old male", age);
            else Console.WriteLine("We have a {0} year old female", age);
        }
    }
}
