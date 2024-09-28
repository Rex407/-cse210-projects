using System;

class Program
{
    static void Main(string[] args)
    {
  // Generate a random magic number between 1 and 100
        Random random = new Random();
        int magicNumber = random.Next(1, 101); // 101 is exclusive

        Console.WriteLine("A magic number has been generated between 1 and 100.");

        int guess = -1; // Initialize guess outside the loop

        // Loop until the guess matches the magic number
        while (guess != magicNumber)
        {
            // Ask the user for a guess
            Console.Write("Guess the magic number: ");
            string guessInput = Console.ReadLine();

            if (int.TryParse(guessInput, out guess))
            {
                // Determine if the guess is correct, too high, or too low
                if (guess == magicNumber)
                {
                    Console.WriteLine("Congratulations! You guessed it!");
                }
                else if (guess < magicNumber)
                {
                    Console.WriteLine("Your guess is too low. Try again!");
                }
                else
                {
                    Console.WriteLine("Your guess is too high. Try again!");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number for your guess.");
            }
        }
    }
}