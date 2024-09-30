using System;
using System.Collections.Generic;

class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} - {Prompt}: {Response}";
    }
}

class Program
{
    static List<Entry> journalEntries = new List<Entry>();
    static readonly string[] prompts = new[]
    {
        "What was the most enjoyable item you bought recently?",
        "Describe a shopping experience that made you happy.",
        "What item are you currently saving up for, and why?",
        "Which store do you enjoy shopping at the most?",
        "What’s the best deal you’ve ever found while shopping?"
    };

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n--- Journal Menu ---");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    Console.WriteLine("Exiting the program.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void WriteEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Entry entry = new Entry
        {
            Prompt = prompt,
            Response = response,
            Date = DateTime.Now
        };
        journalEntries.Add(entry);
        Console.WriteLine("Entry saved!");
    }

    static void DisplayJournal()
    {
        if (journalEntries.Count == 0)
        {
            Console.WriteLine("Your journal is empty.");
            return;
        }

        Console.WriteLine("\n--- Journal Entries ---");
        foreach (var entry in journalEntries)
        {
            Console.WriteLine(entry);
        }
    }
}