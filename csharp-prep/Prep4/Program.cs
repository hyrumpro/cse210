using System;
using System.Collections.Generic;
using System.Linq;

namespace ListCalculations
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            int number;
            
            Console.WriteLine("Enter a list of numbers, type 0 when finished.");
            
            do
            {
                Console.Write("Enter number: ");
                number = Convert.ToInt32(Console.ReadLine());
                numbers.Add(number);
            } while (number != 0);

            numbers.Remove(0);

            int sum = numbers.Sum();
            double average = numbers.Average();
            int max = numbers.Max();
            int minPositive = numbers.Where(n => n > 0).DefaultIfEmpty(0).Min();

            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {max}");
            Console.WriteLine($"The smallest positive number is: {minPositive}");

            numbers.Sort();
            Console.WriteLine("The sorted list is:");
            foreach (var num in numbers)
            {
                Console.WriteLine(num);
            }
        }
    }
}
