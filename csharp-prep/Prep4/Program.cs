using System;

class Program
{
    static void Main(string[] args)
    {
   List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        
        // Collect numbers from user
        while (true)
        {
            Console.Write("Enter number: ");
            string input = Console.ReadLine();
            
            if (int.TryParse(input, out int number))
            {
                if (number == 0)
                {
                    break; // Exit the loop if the user enters 0
                }
                numbers.Add(number); // Add the number to the list
            }
            else
            {
                Console.WriteLine("Please enter a valid integer.");
            }
        }

        // Print the collected numbers for verification
        Console.WriteLine("You entered: " + string.Join(", ", numbers));

        // Calculate the sum, average, and largest number
        int sum = 0;
        int largest = int.MinValue;
        int? smallestPositive = null;

        foreach (int num in numbers)
        {
            sum += num; // Calculate sum
            
            if (num > largest)
            {
                largest = num; // Find largest number
            }

            if (num > 0) // Find the smallest positive number
            {
                if (smallestPositive == null || num < smallestPositive)
                {
                    smallestPositive = num;
                }
            }
        }

        // Output the results
        Console.WriteLine("The sum is: " + sum);
        Console.WriteLine("The average is: " + (numbers.Count > 0 ? (double)sum / numbers.Count : 0));
        Console.WriteLine("The largest number is: " + (largest != int.MinValue ? largest.ToString() : "None"));
        
        if (smallestPositive.HasValue)
        {
            Console.WriteLine("The smallest positive number is: " + smallestPositive.Value);
        }
        else
        {
            Console.WriteLine("There are no positive numbers in the list.");
        }

        // Sort the list
        numbers.Sort();
        
        // Print the sorted list
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}