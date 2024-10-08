using System;
using System.Collections.Generic;
using System.IO;

// Abstract class
abstract class JournalEntry
{
    public DateTime Date { get; set; }
    public abstract string DisplayEntry();
    public abstract string ToFileString(); // For saving to file
}

class TextEntry : JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }

    public override string DisplayEntry()
    {
        return $"{Date.ToShortDateString()} - {Prompt}: {Response}";
    }

    public override string ToFileString()
    {
        return $"TextEntry|{Date}|{Prompt}|{Response}";
    }
}

class ImageEntry : JournalEntry
{
    public string ImagePath { get; set; }
    public string Description { get; set; }

    public override string DisplayEntry()
    {
        return $"{Date.ToShortDateString()} - Image: {Description} (Path: {ImagePath})";
    }

    public override string ToFileString()
    {
        return $"ImageEntry|{Date}|{ImagePath}|{Description}";
    }
}

class Program
{
    static List<JournalEntry> journalEntries = new List<JournalEntry>();
    static readonly string[] prompts = new[]
     {
        "What was the most enjoyable item you bought recently?",
        "Describe a shopping experience that made you happy.",
        "What item are you currently saving up for, and why?",
        "Which store do you enjoy shopping at the most?",
        "What’s the best deal you’ve ever found while shopping?"
    };
    const string filePath = "journal.txt";

    static void Main()
    {
        LoadJournal(); // Load entries from file at startup

        while (true)
        {
            Console.WriteLine("\n--- Journal Menu ---");
            Console.WriteLine("1. Write a new text entry");
            Console.WriteLine("2. Write a new image entry");
            Console.WriteLine("3. Display journal");
            Console.WriteLine("4. Save journal");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteTextEntry();
                    break;
                case "2":
                    WriteImageEntry();
                    break;
                case "3":
                    DisplayJournal();
                    break;
                case "4":
                    SaveJournal();
                    break;
                case "5":
                    Console.WriteLine("Exiting the program.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void WriteTextEntry()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        JournalEntry entry = new TextEntry
        {
            Prompt = prompt,
            Response = response,
            Date = DateTime.Now
        };
        journalEntries.Add(entry);
        Console.WriteLine("Text entry saved!");
    }

    static void WriteImageEntry()
    {
        Console.Write("Enter the image path: ");
        string imagePath = Console.ReadLine();
        Console.Write("Enter a description for the image: ");
        string description = Console.ReadLine();

        JournalEntry entry = new ImageEntry
        {
            ImagePath = imagePath,
            Description = description,
            Date = DateTime.Now
        };
        journalEntries.Add(entry);
        Console.WriteLine("Image entry saved!");
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
            Console.WriteLine(entry.DisplayEntry());
        }
    }

    static void SaveJournal()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var entry in journalEntries)
            {
                writer.WriteLine(entry.ToFileString());
            }
        }
        Console.WriteLine("Journal saved to file!");
    }

    static void LoadJournal()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                JournalEntry entry = parts[0] switch
                {
                    "TextEntry" => new TextEntry
                    {
                        Date = DateTime.Parse(parts[1]),
                        Prompt = parts[2],
                        Response = parts[3]
                    },
                    "ImageEntry" => new ImageEntry
                    {
                        Date = DateTime.Parse(parts[1]),
                        ImagePath = parts[2],
                        Description = parts[3]
                    },
                    _ => null
                };
                if (entry != null)
                {
                    journalEntries.Add(entry);
                }
            }
            Console.WriteLine("Journal loaded from file!");
        }
        else
        {
            Console.WriteLine("No previous journal found. Starting a new journal.");
        }
    }
}