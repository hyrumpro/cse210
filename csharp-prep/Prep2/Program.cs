using System;

namespace GradeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your grade percentage: ");
            string input = Console.ReadLine();
            int gradePercentage;

            while (!int.TryParse(input, out gradePercentage))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer value: ");
                input = Console.ReadLine();
            }

            char gradeLetter;
            char sign = ' ';

            if (gradePercentage >= 90)
            {
                gradeLetter = 'A';
            }
            else if (gradePercentage >= 80)
            {
                gradeLetter = 'B';
            }
            else if (gradePercentage >= 70)
            {
                gradeLetter = 'C';
            }
            else if (gradePercentage >= 60)
            {
                gradeLetter = 'D';
            }
            else
            {
                gradeLetter = 'F';
            }

            if (gradePercentage >= 70)
            {
                Console.WriteLine($"Congratulations! You passed the course with a {gradeLetter} grade.");
            }
            else
            {
                Console.WriteLine($"You did not pass the course. Don't worry, you can try again next time!");
            }

            if (gradeLetter == 'A' && gradePercentage % 10 >= 7)
            {
                sign = '+';
            }
            else if (gradeLetter == 'A' || gradeLetter == 'F')
            {
                Console.WriteLine($"No sign");
            }
            else if (gradePercentage % 10 < 3)
            {
                sign = '-';
            }

            if ((gradeLetter == 'A' || gradeLetter == 'F') && (sign == '+' || sign == '-'))
            {
                Console.WriteLine($"There is no {gradeLetter}{sign} grade. Your final grade is {gradeLetter}.");
            }
            else
            {
                Console.WriteLine($"Your letter grade is {gradeLetter}{sign}.");
            }
        }
    }
}

