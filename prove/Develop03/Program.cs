using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string reference = "Psalm 23:4";
        string text = "Even though I walk through the valley of the shadow of death, I will fear no evil, for you are with me; your rod and your staff comfort me.";
        
        List<string> words = new List<string>(text.Split(' '));
        HashSet<int> hiddenIndices = new HashSet<int>();
        
        while (true)
        {
            Console.Clear();
            DisplayScripture(reference, words, hiddenIndices);
            Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input?.ToLower() == "quit")
                break;

            if (hiddenIndices.Count < words.Count)
            {
                Random random = new Random();
                int index;
                do
                {
                    index = random.Next(words.Count);
                } while (hiddenIndices.Contains(index));
                
                hiddenIndices.Add(index);
            }

            if (hiddenIndices.Count == words.Count)
            {
                Console.Clear();
                DisplayScripture(reference, words, hiddenIndices);
                Console.WriteLine("\nAll words are now hidden. Exiting program.");
                break;
            }
        }
    }

    static void DisplayScripture(string reference, List<string> words, HashSet<int> hiddenIndices)
    {
        Console.WriteLine(reference);
        foreach (var (word, index) in words.Select((word, index) => (word, index)))
        {
            Console.Write(hiddenIndices.Contains(index) ? "_____" : word);
            Console.Write(" ");
        }
    }
}