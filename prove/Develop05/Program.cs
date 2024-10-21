using System;
using System.Collections.Generic;
using System.Threading;

abstract class Activity
{
    protected string Name { get; }
    protected string Description { get; }
    protected int Duration { get; private set; }

    protected Activity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void SetDuration()
    {
        Console.Write("Enter the duration of the activity in seconds: ");
        Duration = int.Parse(Console.ReadLine());
    }

    public void StartMessage()
    {
        Console.WriteLine($"\nStarting {Name}.");
        Console.WriteLine(Description);
        SetDuration();
        Console.WriteLine("Get ready to begin...");
        Pause(3);
    }

    public void EndMessage()
    {
        Console.WriteLine("Good job!");
        Pause(3);
        Console.WriteLine($"You completed the {Name} for {Duration} seconds.");
        Pause(3);
    }

    protected void Pause(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".", Console.CursorLeft);
            Thread.Sleep(1000);
        }
        Console.WriteLine(); // New line after dots
    }

    public abstract void Start();
}

class PushUpsActivity : Activity
{
    public PushUpsActivity() 
        : base("Push-Ups Activity", "This activity will help you improve your strength through a series of push-ups.")
    {
    }

    public override void Start()
    {
        StartMessage();
        int endTime = (int)DateTime.Now.AddSeconds(Duration).Ticks;

        while (DateTime.Now.Ticks < endTime)
        {
            Console.WriteLine("Performing a push-up...");
            Pause(2); // Pause for 2 seconds to simulate doing a push-up
        }

        EndMessage();
    }
}

class ReflectionActivity : Activity
{
    private readonly List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private readonly List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() 
        : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    public override void Start()
    {
        StartMessage();
        Random rand = new Random();
        string selectedPrompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(selectedPrompt);
        Pause(5);

        int endTime = (int)DateTime.Now.AddSeconds(Duration).Ticks;

        while (DateTime.Now.Ticks < endTime)
        {
            string question = questions[rand.Next(questions.Count)];
            Console.WriteLine(question);
            Pause(5);
        }

        EndMessage();
    }
}

class ListingActivity : Activity
{
    private readonly List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() 
        : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void Start()
    {
        StartMessage();
        Random rand = new Random();
        string selectedPrompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(selectedPrompt);
        Pause(5);

        List<string> items = new List<string>();
        int endTime = (int)DateTime.Now.AddSeconds(Duration).Ticks;

        while (DateTime.Now.Ticks < endTime)
        {
            Console.Write("List an item (type 'done' to finish): ");
            string item = Console.ReadLine();
            if (item.ToLower() == "done") break;
            items.Add(item);
        }

        Console.WriteLine($"You listed {items.Count} items.");
        EndMessage();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, Activity> activities = new Dictionary<string, Activity>
        {
            { "1", new PushUpsActivity() },
            { "2", new ReflectionActivity() },
            { "3", new ListingActivity() }
        };

        while (true)
        {
            Console.WriteLine("\nChoose an activity:");
            Console.WriteLine("1. Push-Ups Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();
            if (activities.ContainsKey(choice))
            {
                activities[choice].Start();
            }
            else if (choice == "4")
            {
                Console.WriteLine("Exiting the program.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}