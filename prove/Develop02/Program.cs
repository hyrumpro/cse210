using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public JournalEntry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();

    public void WriteNewEntry()
    {
        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        Random random = new Random();
        string randomPrompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine($"Prompt: {randomPrompt}");
        Console.Write("Your Response: ");
        string response = Console.ReadLine();

        string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        entries.Add(new JournalEntry(randomPrompt, response, currentDate));
    }

    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveJournalToFile()
    {
        Console.Write("Enter the filename to save the journal: ");
        string filename = Console.ReadLine();

        using (StreamWriter sw = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                sw.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }

        Console.WriteLine("Journal saved successfully!");
    }

    public void LoadJournalFromFile()
    {
        Console.Write("Enter the filename to load the journal: ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            entries.Clear(); // Clear existing entries before loading from the file

            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    string[] entryData = sr.ReadLine().Split(',');
                    entries.Add(new JournalEntry(entryData[1], entryData[2], entryData[0]));
                }
            }

            Console.WriteLine("Journal loaded successfully!");
        }
        else
        {
            Console.WriteLine("File not found. Please check the filename and try again.");
        }
    }
}

class Program
{
    static void Main()
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice (1-5): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.WriteNewEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    journal.SaveJournalToFile();
                    break;
                case "4":
                    journal.LoadJournalFromFile();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;
            }
        }
    }
}
