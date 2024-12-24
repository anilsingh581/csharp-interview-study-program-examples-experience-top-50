using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayArrayList
{
    public partial class Class2
    {
        public class Animal
        {
            static Animal()
            {
                Console.WriteLine("I am animal constructor.");
            }

            protected int getAnimalId()
            {
                return 1;
            }
        }
        public class Dog : Animal
        {
            static Dog()
            {
                Console.WriteLine("I am dog constructor.");
            }

            protected int getDogId()
            {
                return 1;
            }

            static void Main(string[] args)
            {
                Dog DOG = new Dog();
                DOG.getAnimalId();
                DOG.getDogId();
            }
        }     

    

    }
}
