using System;
using System.Collections.Generic;
using System.Linq;

// Word class to encapsulate the behavior and state of a word
class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public string GetDisplayText()
    {
        return _isHidden ? "_____" : _text;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden => _isHidden;
}

// Scripture class to encapsulate the behavior and data of a scripture
class Scripture
{
    private string _reference;
    private List<Word> _words;

    public Scripture(string reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public string GetDisplayText()
    {
        return _reference + "\n" + string.Join(" ", _words.Select(w => w.GetDisplayText()));
    }

    public void HideRandomWord(Random random)
    {
        var hiddenWords = _words.Where(w => w.IsHidden).ToList();
        if (hiddenWords.Count < _words.Count)
        {
            int index;
            do
            {
                index = random.Next(_words.Count);
            } while (_words[index].IsHidden);

            _words[index].Hide();
            Console.WriteLine($"The word '{_words[index].GetDisplayText()}' is now hidden.");
        }
    }

    public bool AllWordsHidden => _words.All(w => w.IsHidden);
}

// ScriptureGame class to manage the game logic
class ScriptureGame
{
    private Scripture _scripture;
    private Random _random;

    public ScriptureGame(string reference, string text)
    {
        _scripture = new Scripture(reference, text);
        _random = new Random();
    }

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(_scripture.GetDisplayText());
            Console.WriteLine("\nPress Enter to hide a word or type 'quit' to exit.");

            string input = Console.ReadLine();
            if (input?.ToLower() == "quit") break;

            _scripture.HideRandomWord(_random);

            if (_scripture.AllWordsHidden)
            {
                Console.Clear();
                Console.WriteLine(_scripture.GetDisplayText());
                Console.WriteLine("\nAll words are now hidden. Exiting program.");
                break;
            }
        }
    }
}

// Main program entry point
class Program
{
    static void Main()
    {
        string reference = "Psalm 23:4";
        string text = "Even though I walk through the valley of the shadow of death, I will fear no evil, for you are with me; your rod and your staff comfort me.";

        ScriptureGame game = new ScriptureGame(reference, text);
        game.Start();
    }
}