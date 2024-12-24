// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        //static void Main(string[] args) 
        //{
        //    //Console.WriteLine("Hello, World!");
        //    //Employee employee = new Employee();
        //    //employee.Name = "Ram";

        //    //employee.Validate();

        //    //Manager manager = new Manager();
        //    //manager.Validate();

        //    //Employee m = new Manager();
        //    //m = new SuperVisor();


        //    //Prog prog = new Prog();
        //    //prog.MainProg();
        //}
    }

    class Patient
    {
        public string Name { get; set; }
        public string Adderess { get; set; }
        public Doctor doctor { get; set; }
    }

    class Doctor
    {
        public string Name { get; set; }
    }

    public class Employee
    {
        public string Name { get; set; }
        public string Adderess { get; set; }

        public virtual void Validate()
        {
            CheckName();
            CheckAddress();
        }

        private void CheckName()
        {
        }
        private void CheckAddress()
        {
        }
    }

    public class Manager : Employee 
    { 
        public void Management()
        {

        }

        public override void Validate()
        {
            Management();
        }
    }

    public class SuperVisor : Employee
    {
        public void Depart()
        {

        }
    }

    public class Prog
    {

        public void MainProg()
        {
            //Write Program to find fibonacci series for given number
            for (int i = 0; i < 10; i++)
            {
                Console.Write("{0} ", FibbonacciSeries(i));
            }
            Console.WriteLine(Environment.NewLine);

            Console.Write("Fibbonacci Series of {0} is : {1} ", 10, FibbonacciSeries1(10));

            Console.WriteLine(Environment.NewLine);

            //Write Program to calculate factorial value for given number    
            Console.WriteLine("The Factorial of {0} is: {1} \n", 6, Factorial(6));

            //Write Program to find duplicate in String    
            FindDuplicateCharacterInString("CSharpCorner");

            Console.WriteLine(Environment.NewLine);
            //Write Program to find duplicate in string array    
            FindDuplicateInstringArray();

            Console.WriteLine(Environment.NewLine);
            //Write Program to Remove duplicate in string    
            RemoveDuplicate("CSharpCorner");

            Console.WriteLine(Environment.NewLine);
            //Write Program to find number of character in string    
            FindNumberofCharaterinString();

            Console.WriteLine(Environment.NewLine);
            //Write Program to find number of words in string    
            ReverseStringWords("Jignesh Kumar");

            Console.WriteLine(Environment.NewLine);
            //Write Program to find consicutive character in string    
            char[] charArray = { 'A', 'B', 'B', 'C', 'D', 'D', 'E', 'F', 'F' };
            FindConsicutiveCharacter(charArray);

            Console.WriteLine(Environment.NewLine);
            //Write Program to check string is palindrome or not    
            string[] strArray = { "WOW", "NOON", "ABBA", "ANNA", "BOB", "Jimmy", "Peter" };

            foreach (var item in strArray)
            {
                Console.WriteLine(" {0} Is palindrome {1}", item, IsStringPalindrome(item));
            }

            Console.WriteLine(Environment.NewLine);
            //Write program to check number is palindrome or not    
            int number = 121;
            Console.WriteLine("{0} is Palindrome number {1} ", number, IsNumberPalindrome(number));

            Console.WriteLine(Environment.NewLine);
            //Write program to check number is palindrome or not    
            number = 127;
            Console.WriteLine("{0} is prime number {1} ", number, IsNumberPrime(number));
            number = 128;
            Console.WriteLine("{0} is prime number {1} ", number, IsNumberPrime(number));
            Console.ReadLine();
        }
        private static int FibbonacciSeries(int number)
        {
            int firstValue = 0;
            int secondValue = 1;
            int result = 0;
            if (number == 0)
                return 0;
            if (number == 1)
                return 1;
            for (int i = 2; i <= number; i++)
            {
                result = firstValue + secondValue;
                firstValue = secondValue;
                secondValue = result;
            }
            return result;
        }

        public static string FibbonacciSeries1(int n)
        {
            int a = 0, b = 1, c;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < n; i++)
            {
                sb.Append(a.ToString() + ",");
                c = a + b;
                a = b;
                b = c;
            }
            return sb.ToString().Remove(sb.Length - 1); ;
        }

        private static int Factorial(int number)
        {
            int fact = 1;
            if (number == 0)
                return 0;
            if (number == 1)
                return 1;

            for (int i = 1; i <= number; i++)
            {
                fact = fact * i;
            }
            return fact;
        }

        private static void FindDuplicateCharacterInString(string inPutString)
        {

            if (string.IsNullOrEmpty(inPutString))
            {
                Console.WriteLine("Please enter valid Input");
            }
            else
            {
                var list = new List<char>();
                string result = string.Empty;

                foreach (char item in inPutString)
                {
                    if (list.Contains(item))
                    {
                        if (!result.Contains(item))
                            result += item;
                    }
                    else
                    {
                        list.Add(item);
                    }
                }
                Console.WriteLine("Duplicate Found : {0} ", result);
            }
        }

        public static void FindDuplicateInstringArray()
        {
            string[] strArray = { "Sunday", "Monday", "Tuesday", "Wednesday", "Sunday", "Monday" };
            List<string> lstString = new List<string>();
            StringBuilder sb = new StringBuilder();

            foreach (var str in strArray)
            {
                if (lstString.Contains(str))
                {
                    sb.Append(" " + str);

                }
                else
                {
                    lstString.Add(str);
                }
            }
            Console.WriteLine("Duplicate Found : {0}", sb.ToString());
        }

        public static void RemoveDuplicate(string inputString)
        {
            var list = new List<char>();

            foreach (var item in inputString)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }

            Console.WriteLine("Orignal String {0}, After duplicate removed {1}", inputString, new string(list.ToArray()));
        }

        public static void FindNumberofCharaterinString()
        {
            string StringToCount = "DotNetDeveloper";
            var result = FindOccuranceofCharacterInString(StringToCount);

            Console.WriteLine("Number of occurrences of a character in given string");
            foreach (var count in result)
            {
                Console.WriteLine(" {0} - {1} ", count.Key, count.Value);
            }
        }

        public static SortedDictionary<char, int> FindOccuranceofCharacterInString(string str)
        {
            SortedDictionary<char, int> count = new SortedDictionary<char, int>();

            foreach (var chr in str)
            {
                if (!(count.ContainsKey(chr)))
                {
                    count.Add(chr, 1);
                }
                else
                {
                    count[chr]++;
                }
            }

            return count;
        }

        public static void ReverseStringWords(string inputString)
        {
            string[] seprator = { " " };
            string[] words = inputString.Split(seprator, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Empty;
            for (int i = words.Length - 1; i >= 0; i--)
            {
                result += words[i].ToString();
            }
            Console.WriteLine("Reverse words in string {0}", result);
        }

        public static void FindConsicutiveCharacter(char[] characterArray)
        {
            int len = characterArray.Length - 1;
            List<char> result = new List<char>();
            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j <= len; j++)
                {
                    if (characterArray[i] == characterArray[j])
                    {
                        if (!result.Contains(characterArray[i]))
                            result.Add(characterArray[i]);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("Consicutive Character found {0} ", new string(result.ToArray()));
        }

        public static bool IsStringPalindrome(string inputString)
        {
            int minIdex = 0;
            int maxIdex = inputString.Length - 1;
            while (true)
            {
                if (minIdex > maxIdex)
                {
                    return true;
                }
                char charfromLeft = inputString[minIdex];
                char charfromRight = inputString[maxIdex];
                if (charfromLeft != charfromRight)
                {
                    return false;
                }
                minIdex++;
                maxIdex--;
            }
        }
        public static bool IsNumberPalindrome(int number)
        {
            int reminder, sum = 0;
            int tempNumber;
            tempNumber = number;
            bool IsPalindrome = false;
            while (number > 0)
            {
                reminder = number % 10;
                number = number / 10;
                sum = sum * 10 + reminder;
                if (tempNumber == sum)
                {
                    IsPalindrome = true;
                }
            }
            return IsPalindrome;
        }

        public static bool IsNumberPrime(int number)
        {
            int i;
            for (i = 2; i <= number - 1; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            if (i == number)
            {
                return true;
            }
            return false;
        }
    }


}

#region enum
public enum DaysOfWeek
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday = 5,
    Thursday,
    Friday,
    Saturday
}

//class Program2
//{
//    static void Main()
//    {
//        DaysOfWeek today = DaysOfWeek.Wednesday;
//        Console.WriteLine("Today is: " + today);

//        // Output the numeric value of the enum
//        Console.WriteLine("Numeric value of today: " + (int)today);
//    }
//}
#endregion

#region Factory design pattern
public interface ICar
{
    void Start();
}

public class FourSeater : ICar
{
    public void Start()
    {
        Console.WriteLine("4 seater");
    }
}

public class SevenSeater : ICar
{
    public void Start()
    {
        Console.WriteLine("7 seater");
    }
}

public class CarFactory
{
    public ICar GetCar(string type)
    {
        ICar car = new FourSeater();
        ICar car2 = new SevenSeater();

        switch (type)
        {
            case "FourSeater":
                ICar carh = new FourSeater();
                carh.Start();
                return new FourSeater();
            case "SevenSeater":
                return new SevenSeater();
        }
        return null;
    }
}

public class Prog
{
    //public static void Main()
    //{
    //    //ICar car = new FourSeater();
    //    //ICar car2 = new SevenSeater();
    //    //car2.Start();
    //    //CarFactory carFactory = new CarFactory();
    //    //ICar four = carFactory.GetCar("FourSeater");
    //    //ICar seven = carFactory.GetCar("SevenSeater");

    //    ABC aBC = new ABC();
    //    aBC.Maint();
    //}
}
#endregion

#region overriding
//public class baseC
//{
//    public virtual string emp()
//    {
//        return "virtual";
//    }
//}

//public class deriveC : baseC
//{
//    public override string emp()
//    {
//        Console.WriteLine("override");
//        return "override";
//    }
//}

//public class progh
//{
//    static void Main()
//    {
//        deriveC deriveC = new deriveC();
//        deriveC.emp();
//    }
//}

#endregion

#region polymorphism
//public class P0
//{
//    public virtual void M0()
//    {
//        Console.WriteLine("P0");
//    }

//}

//public class P1 : P0
//{
//    public override void M0()
//    {
//        Console.WriteLine("P1");
//    }
//}

//public class P2 : P0
//{
//    public override void M0()
//    {
//        Console.WriteLine("P2");
//    }
//}

//public class P3
//{
//    public static void Main()
//    {
//        P0 p = new P1();
//        p.M0();
//        p = new P2();
//        p.M0();

//    }
//}
#endregion

#region Genrics class and method
//public class Gen
//{
//    public static void Main()
//    {
//        bool r1 = Equal<int>.AreEqual(4, 4);

//        bool r2 = Equal<string>.AreEqual("hi", "hi");

//        Console.WriteLine(r1);
//        Console.WriteLine(r2);
//    }
//}

////generic class
//public class Equal<T>
//{
//    //generic method
//    //public static bool AreEqual<T>(T x, T y)
//    //{
//    //    return x.Equals(y);
//    //}

//    public static bool AreEqual(T x, T y)
//    {
//        return x.Equals(y);
//    }
//}
#endregion

#region OOPS
//public class Employeee
//{
//    public string Name { get; set; }
//    public string Address { get; set; }

//    public Manager Manager { get; set; }
//}

//public class Manager
//{
//    public string Name { get; set; }
//}

//public class j
//{
//    public static void Main()
//    {
//        Employeee employeee = new Employeee();
//        employeee.Name = "EName";
//        employeee.Address = "Address";

//        Manager manager = new Manager();
//        manager.Name = "Manager";


//        Console.WriteLine(employeee.Name);

//        Console.ReadLine();

//        Console.WriteLine(employeee.Address);
//        Console.WriteLine(manager.Name);

//    }
//}
#endregion

#region out ref
//out
//class Program
//{
//    static void Main()
//    {
//        int result; // Variable to store the output
//        int res;

//        // Calling the method with out parameter
//        AddNumbers(5, 3, out result, out res);

//        // Printing the result obtained from the method
//        Console.WriteLine("Result: " + result); // Output will be 8
//        Console.WriteLine("Res: " + res); // Output will be 8
//    }

//    // Method with out parameter
//    static void AddNumbers(int a, int b, out int sum, out int sub)
//    {
//        // Assigning the sum of 'a' and 'b' to the 'sum' parameter
//        sum = a + b;
//        sub = a - b;
//    }
//}


//ref
//class Program
//{
//    static void Main()
//    {
//        int number = 10;
//        int n = 2;

//        // Before calling the method
//        Console.WriteLine("Before calling the method: " + number); // Output: 10

//        // Calling the method with ref parameter
//        MultiplyByTwo(5, ref number, ref n);

//        // After calling the method
//        Console.WriteLine("After calling the method: " + number); // Output: 20
//        Console.WriteLine("After calling the method2: " + n); // Output: 20
//    }

//    // Method with ref parameter
//    static void MultiplyByTwo(int i, ref int x, ref int y)
//    {
//        x *= 2; // Multiply the parameter by 2
//        y *= 2; // Multiply the parameter by 2
//    }
//}
#endregion

#region partial class
//class Program
//{
//    public static void Main()
//    {
//        Calculation cal = new Calculation();
//        int a = cal.Sum(5, 3);
//        int b = cal.Sub(5,3);

//        Console.WriteLine(a);
//        Console.WriteLine(b);
//    }
//}

//partial class Calculation
//{
//    public int Sum(int a, int b)
//    {
//        return a + b;
//    }
//}

//partial class Calculation
//{
//    public int Sub(int a, int b) 
//    {
//        return a - b;
//    }
//}

#endregion

#region multiple inheritance and class
//interface I1
//{
//    void Start();
//}

//interface I2
//{
//    void Stop();
//}

//class C1 : C2, I1, I2
//{
//    public void Start()
//    {

//    }

//    public void Stop()
//    {

//    }
//}

//class C2
//{
//    public void Go()
//    {

//    }
//}

#endregion

#region Singleton
public sealed class Singleton1
{
    private Singleton1() { }

    private static Singleton1 _instance1;

    public static Singleton1 Getinstance1()
    {
        if (_instance1 == null)
        {
            _instance1 = new Singleton1();
        }
        return _instance1;
    }

}

public class Prog1
{
    //public static void Main(string[] args)
    //{
    //    Singleton1 s1 = Singleton1.Getinstance1();
    //    Singleton1 s2 = Singleton1.Getinstance1();

    //    if (s1 == s2)
    //    {
    //        Console.WriteLine("Singleton works, both variables contain the same instance.");
    //    }
    //    else
    //    {
    //        Console.WriteLine("Singleton failed, variables contain different instances.");
    //    }
    //}
}
#endregion

#region Abstract class
public abstract class Customer
{
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal Amount { get; set; }

    public abstract decimal Discount();
    //{
    //    throw new NotImplementedException("Will be done in chlid class");
    //}
}

public class GoldCustomer : Customer
{
    public override decimal Discount()
    {
        return Amount - 10;
    }
}

public class SilverCustomer : Customer
{
    public override decimal Discount()
    {
        return Amount - 5;
    }
}

public class prog2
{
    //static void Main(string[] args)
    //{
    //    // Create instances of GoldCustomer and SilverCustomer
    //    GoldCustomer goldCustomer = new GoldCustomer
    //    {
    //        Name = "John Doe",
    //        Address = "123 Gold Street",
    //        Amount = 100
    //    };

    //    SilverCustomer silverCustomer = new SilverCustomer
    //    {
    //        Name = "Jane Smith",
    //        Address = "456 Silver Avenue",
    //        Amount = 100
    //    };

    //    // Display discount information
    //    Console.WriteLine($"Gold Customer: {goldCustomer.Name}, Address: {goldCustomer.Address}, Amount: {goldCustomer.Amount}, Discounted Amount: {goldCustomer.Discount()}");
    //    Console.WriteLine($"Silver Customer: {silverCustomer.Name}, Address: {silverCustomer.Address}, Amount: {silverCustomer.Amount}, Discounted Amount: {silverCustomer.Discount()}");
    //}
}
#endregion

#region call by value and call by ref function

public class CallByRef
{
    public void RefMethod(ref int a)
    {
        a = a * a;
        Console.WriteLine("RefMethod:" + a);
    }

    public void ValueMethod(int a)
    {
        a = a * a;
        Console.WriteLine("ValueMethod:" + a);
    }

    //public static void Main()
    //{
    //    int a = 10;
    //    CallByRef callByRef = new CallByRef();
    //    callByRef.ValueMethod(a);
    //    Console.WriteLine("Call by value:" + a);

    //    callByRef.RefMethod(ref a);
    //    Console.WriteLine("Call by ref:" + a);
    //}
}


#endregion

#region RemoveDuplicateWords
public class RemoveDuplicateWords
{
    //public static void Main()
    //{
    //    //input
    //    string input = "Hello world Hello world My favourite person is me";

    //    //break the input string by space and insert in words array
    //    string[] words = input.Split(' ');

    //    //we can identify the unique in words array and insert in distinctWords array
    //    string[] distinctWords = words.Distinct().ToArray();

    //    //Join is used to join the array to string and each character is seperated by space
    //    string output = string.Join(" ", distinctWords);
        
    //    //output
    //    Console.WriteLine(output);
    //}
}
#endregion

#region RepaceVowel
public class RepaceVowel
{
    //public static void Main()
    //{
    //    //input
    //    string input = "Hello world Hello world My favourite person is me";

    //    // make a list and map to replaced word
    //    var replaceWord = new Dictionary<char, char>
    //    {
    //        { 'a', 'e' },
    //        { 'e', 'i' },
    //        { 'i', 'o' },
    //        { 'o', 'u' },
    //        { 'u', 'a' },
    //        { 'A', 'E' },
    //        { 'E', 'I' },
    //        { 'I', 'O' },
    //        { 'O', 'U' },
    //        { 'U', 'A' }
    //    };

    //    //for output result
    //    string output = "";

    //    // using foreach loop to check or manipulate each character
    //    foreach (char item in input)
    //    {
    //        // Check if the character is a vowel and replace it
    //        if (replaceWord.ContainsKey(item))
    //        {
    //            output = output + replaceWord[item];
    //        }
    //        else
    //        {
    //            output = output + item;
    //        }

    //    }
       
    //    //output
    //    Console.WriteLine(output);
    //}
}
#endregion

#region MergeArrayAndFind5th
public class MergeArrayAndFind5th
{
    //public static void Main()
    //{
    //    int[] arr1 = { 9, 8, 5, 5, 5, 3, 2, 1, 4, 6 };
    //    int[] arr2 = { 9, 8, 5, 5, 5, 3, 2, 1, 4, 6 };
    //    int[] mergeArr = arr1.Concat(arr2).Distinct().ToArray();

    //    int fifthVal = mergeArr[4];


    //}
}
#endregion

#region UnitTesting

public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    //public static void Main()
    //{

    //}
}

#endregion

#region Factorial
public class Factorial
{
    //public static void Main() 
    //{
    //    int n = 10;
    //    int factorial = 1;
    //    for (int i = 1; i <= n; i++)
    //    {
    //        factorial *= i;
    //    }
    //    Console.WriteLine(factorial);
    //}
}
#endregion

#region array
public class Pg1
{
    //public static void Main()
    //{
    //    string s = "Hello world Hello world My favourite person is me";
    //    string[] splitArr = s.Split(' ');
    //    string[] distinct = splitArr.Distinct().ToArray();
    //    string output = string.Join(" ", distinct);
    //    Console.WriteLine(output);

    //    //var replace = new Dictionary<char, char>
    //    //{
    //    //    {'a', 'b' },
    //    //};
    //    //string output = "";
    //    //foreach(char item in s)
    //    //{
    //    //    if (replace.ContainsKey(item))
    //    //    {
    //    //        output = output + replace[item];
    //    //    }
    //    //    else
    //    //    {
    //    //        output = output + item;
    //    //    }
    //    //}
    //    //Console.WriteLine(output);

    //    //int[] arr1 = { 9, 8, 5, 5, 5, 3, 2, 1, 4, 6 };
    //    //int[] arr2 = { 9, 8, 5, 5, 5, 3, 2, 1, 4, 6 };
    //    //int[] merge = arr1.Concat(arr2).Distinct().ToArray();
    //    //Console.WriteLine(merge[4]);

    //    int[] inputArr = { 1, 1, 0, 0, 1, 0, 1, 2, 0, 1, 2, 0, 1, 2, 1, 0 };
    //    int[] outputArr = { 1, 0, 2 };
    //    string result = "";
    //    foreach(var item in outputArr)
    //    {
    //        var list = inputArr.Where(x => x == item);
    //        string combined = string.Join("", list);
    //        result = result + combined;
    //    }
    //    Console.WriteLine(result);
    //}
}
#endregion

#region Delegate
public class DelegateClass
{
    //public delegate void Sum(int a, int b);

    //public void AddSum(int a, int b)
    //{
    //    Console.WriteLine(a + b);
    //}
    //public static void Main()
    //{
    //    DelegateClass delegateClass = new DelegateClass();

    //    Sum del = new Sum(delegateClass.AddSum);
    //    del(1, 2);
    //}
}
#endregion

#region Multicast delegate
public class MulticastDelegate
{
    //public delegate void Sum(int a, int b);
    //public delegate void Sub(int a, int b);

    //public void AddSum(int a, int b)
    //{
    //    Console.WriteLine(a + b);
    //}
    //public void Subtract(int a, int b)
    //{
    //    Console.WriteLine(a - b);
    //}

    //public static void Main()
    //{
    //    MulticastDelegate multicastDelegate = new MulticastDelegate();

    //    Sum del = new Sum(multicastDelegate.AddSum);

    //    del += multicastDelegate.Subtract;

    //    del.Invoke(3, 2);
    //}
}
#endregion


