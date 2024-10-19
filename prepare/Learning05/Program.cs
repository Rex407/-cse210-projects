using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static bool stopSpinner = true;

    static void Main(string[] args)
    {
        MainMenu();
    }

    static void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Activity Menu:");
            Console.WriteLine("1. Jumping Jacks");
            Console.WriteLine("2. Push-ups");
            Console.WriteLine("3. Squats");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StartActivity("Jumping Jacks", "A great cardio exercise to get your heart pumping.");
                    break;
                case "2":
                    StartActivity("Push-ups", "An excellent upper body strength exercise.");
                    break;
                case "3":
                    StartActivity("Squats", "A fundamental exercise for building lower body strength.");
                    break;
                case "4":
                    Console.WriteLine("Exiting the program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    static void StartActivity(string activityName, string description)
    {
        Console.Clear();
        Console.WriteLine($"Starting {activityName}...");
        Console.WriteLine(description);
        
        Console.Write("Set the duration for this activity in seconds: ");
        if (!int.TryParse(Console.ReadLine(), out int duration))
        {
            Console.WriteLine("Invalid duration. Please enter a number.");
            return;
        }
        
        Console.WriteLine("Prepare to begin...");
        
        // Start spinner animation in a separate task
        stopSpinner = false;
        Task spinnerTask = Task.Run(() => SpinnerAnimation());

        Thread.Sleep(3000); // Simulating preparation time
        Console.WriteLine("\nStarting now!");
        
        Thread.Sleep(duration * 1000); // Simulate the activity duration
        
        // Stop spinner animation
        stopSpinner = true;
        spinnerTask.Wait();

        Console.WriteLine("\nGood job!");
        Thread.Sleep(2000); // Pause before showing completion message
        Console.WriteLine($"You have completed {activityName} for {duration} seconds.");
        Thread.Sleep(3000); // Final pause before finishing
    }

    static void SpinnerAnimation()
    {
        var spinner = new[] { '|', '/', '-', '\\' };
        while (!stopSpinner)
        {
            foreach (var symbol in spinner)
            {
                Console.Write($"\r{symbol} Loading...");
                Thread.Sleep(100);
            }
        }
    }
}