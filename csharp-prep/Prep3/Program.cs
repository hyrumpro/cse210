using System;

namespace GuessMyNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int magicNumber = random.Next(1, 101);
            int numberOfGuesses = 0;

            Console.WriteLine("Welcome to the Guess My Number game!");
            Console.WriteLine("I have picked a number between 1 and 100. Try to guess it.");

            int guess = 0;
            while (true)
            {
                Console.Write("What is your guess? ");
                guess = Convert.ToInt32(Console.ReadLine());
                numberOfGuesses++;

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    break;
                }
            }

            Console.WriteLine($"You made {numberOfGuesses} guesses.");
        }
    }
}

