using System;
using System.Collections.Generic;

class Word
{
    public string Text { get; private set; }

    public Word(string text)
    {
        Text = text;
    }

    public void Hide()
    {
        Text = "";
    }

    public bool IsHidden()
    {
        return string.IsNullOrEmpty(Text);
    }
}

class Reference
{
    public int Chapter { get; private set; }
    public int StartVerse { get; private set; }
    public int EndVerse { get; private set; }

    public Reference(int chapter, int startVerse)
    {
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = startVerse;
    }

    public Reference(int chapter, int startVerse, int endVerse)
    {
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        if (StartVerse == EndVerse)
        {
            return $"{Chapter}:{StartVerse}";
        }
        else
        {
            return $"{Chapter}:{StartVerse}-{EndVerse}";
        }
    }
}

class Scripture
{
    private Reference reference;
    private List<Word> words;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = new List<Word>();
        string[] wordArray = text.Split(new char[] { ' ', ',', '.', '!', '?', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in wordArray)
        {
            words.Add(new Word(word));
        }
    }

    public void DisplayScripture()
    {
        Console.Clear();
        Console.WriteLine($"{reference}\n{GetVisibleText()}");
    }

    public void HideWords()
    {
        Random random = new Random();
        int wordsHidden = 0;

        while (wordsHidden < words.Count)
        {
            DisplayScripture();
            Console.WriteLine("\nPress Enter to reveal more words or type 'quit' to exit.");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "quit")
                break;

            int wordIndex = random.Next(words.Count);
            if (!words[wordIndex].IsHidden())
            {
                words[wordIndex].Hide();
                wordsHidden++;
            }
        }

        Console.WriteLine("\nAll words have been hidden. Press any key to exit.");
        Console.ReadKey();
    }

    public Reference GetReference()
    {
        return reference;
    }

    private string GetVisibleText()
    {
        List<string> visibleWords = new List<string>();
        foreach (Word word in words)
        {
            if (!word.IsHidden())
            {
                visibleWords.Add(word.Text);
            }
            else
            {
                visibleWords.Add("___"); 
            }
        }
        return string.Join(" ", visibleWords);
    }
}

class ScriptureLibrary
{
    private List<Scripture> scriptures;
    private Random random;

    public ScriptureLibrary()
    {
        scriptures = new List<Scripture>();
        random = new Random();
        InitializeScriptures();
    }

    private void InitializeScriptures()
    {
        Reference ref1 = new Reference(3, 16);
        Reference ref2 = new Reference(3, 5, 6);
        string text = "For God so loved the world that He gave His only begotten Son, that whoever believes in Him should not perish but have everlasting life.";
        string text2 = "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.";
        
        Scripture scripture1 = new Scripture(ref1, text);
        Scripture scripture2 = new Scripture(ref2, text2);

        scriptures.Add(scripture1);
        scriptures.Add(scripture2);
    }

    public void PresentRandomScripture()
    {
        int index = random.Next(scriptures.Count);
        Scripture randomScripture = scriptures[index];

        randomScripture.HideWords();
    }

    public void DisplayAllScriptures()
    {
        foreach (Scripture scripture in scriptures)
        {
            scripture.DisplayScripture();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    public void ScriptureQuiz()
    {
        Console.WriteLine("Welcome to the Scripture Quiz!");
        Console.WriteLine("Guess the scripture reference:");
        int index = random.Next(scriptures.Count);
        Scripture quizScripture = scriptures[index];

        string guessedReference;
        do
        {
            quizScripture.DisplayScripture();
            Console.Write("Your guess: ");
            guessedReference = Console.ReadLine();

            if (guessedReference.ToLower() == "quit")
                break;

            Reference actualReference = quizScripture.GetReference();
            if (guessedReference.ToLower() != actualReference.ToString().ToLower())
            {
                Console.WriteLine("Incorrect! Try again or type 'quit' to exit.");
            }
            else
            {
                Console.WriteLine("Correct! Well done!");
                break;
            }
        } while (guessedReference.ToLower() != "quit");
    }
}

class Program
{
    static void Main()
    {
        ScriptureLibrary library = new ScriptureLibrary();

        Console.WriteLine("Press 1 to Hide Words, 2 to Present Random Scripture, 3 to Display All Scriptures, 4 for Scripture Quiz, or 'quit' to exit:");
        string userInput = Console.ReadLine().ToLower();

        while (userInput != "quit")
        {
            switch (userInput)
            {
                case "1":
                    library.PresentRandomScripture();
                    break;
                case "2":
                    library.PresentRandomScripture();
                    break;
                case "3":
                    library.DisplayAllScriptures();
                    break;
                case "4":
                    library.ScriptureQuiz();
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again.");
                    break;
            }

            Console.WriteLine("\nPress 1 to Hide Words, 2 to Present Random Scripture, 3 to Display All Scriptures, 4 for Scripture Quiz, or 'quit' to exit:");
            userInput = Console.ReadLine().ToLower();
        }
    }
}

