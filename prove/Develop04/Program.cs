using System;
using System.Collections.Generic;
using System.IO;

abstract class Activity
{
    protected string description;
    protected int duration;

    public Activity(string desc)
    {
        description = desc;
    }

    public void Start()
    {
        Console.WriteLine(description);
        duration = GetDuration();
        DoActivity();
        FinishActivity();
    }

    protected abstract void DoActivity();

    protected int GetDuration()
    {
        int dur;
        while (true)
        {
            Console.Write("Enter duration in seconds: ");
            if (int.TryParse(Console.ReadLine(), out dur) && dur > 0)
                return dur;
            Console.WriteLine("Invalid input. Please enter a positive number.");
        }
    }

    protected void PauseWithSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    protected virtual void FinishActivity()
    {
        Console.WriteLine($"Good job! You completed this activity for {duration} seconds.");
        PauseWithSpinner(3);
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity() : base("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.") { }

    protected override void DoActivity()
    {
        Console.WriteLine("Starting Breathing activity for " + duration + " seconds.");

        for (int i = 0; i < duration; i++)
        {
            if (i % 2 == 0)
                Console.WriteLine("Breathe in...");
            else
                Console.WriteLine("Breathe out...");

            Thread.Sleep(1000);
        }
    }
}

class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        // Add more prompts if you want
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        // Add more questions if you want
    };

    public ReflectionActivity() : base("This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.") { }

    protected override void DoActivity()
    {
        Console.WriteLine("Starting Reflection activity for " + duration + " seconds.");

        Random rnd = new Random();
        for (int i = 0; i < duration;)
        {
            string prompt = prompts[rnd.Next(prompts.Length)];
            Console.WriteLine(prompt);

            foreach (string question in questions)
            {
                Console.WriteLine(question);
                PauseWithSpinner(3);
            }

            i += questions.Length;
        }
    }
}

class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        // Add more prompts if you want
    };

    public ListingActivity() : base("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") { }

    protected override void DoActivity()
    {
        Console.WriteLine("Starting Listing activity for " + duration + " seconds.");

        Random rnd = new Random();
        string prompt = prompts[rnd.Next(prompts.Length)];
        Console.WriteLine(prompt);

        Console.WriteLine("Begin listing...");

        Thread.Sleep(3000); 

        Console.WriteLine("Enter items (one per line). Press Enter twice to finish:");

        int itemCount = 0;
        string input;
        do
        {
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                itemCount++;
        } while (!string.IsNullOrWhiteSpace(input) && itemCount < duration);

        Console.WriteLine($"You listed {itemCount} items.");
    }
}


class Log
{
    private Dictionary<string, int> activityCounts;

    public Log()
    {
        activityCounts = new Dictionary<string, int>();
    }

    public void LogActivity(string activity)
    {
        if (activityCounts.ContainsKey(activity))
        {
            activityCounts[activity]++;
        }
        else
        {
            activityCounts.Add(activity, 1);
        }
    }

    public void ShowActivityCounts()
    {
        Console.WriteLine("Activity Log:");
        foreach (var activity in activityCounts)
        {
            Console.WriteLine($"{activity.Key}: {activity.Value} times");
        }
    }

    public void SaveLogToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var activity in activityCounts)
            {
                writer.WriteLine($"{activity.Key}: {activity.Value}");
            }
        }
        Console.WriteLine("Log saved to file.");
    }

    public void LoadLogFromFile(string fileName)
    {
        activityCounts.Clear();
        string line;
        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string activity = parts[0].Trim();
                        int count;
                        if (int.TryParse(parts[1].Trim(), out count))
                        {
                            activityCounts.Add(activity, count);
                        }
                    }
                }
            }
            Console.WriteLine("Log loaded from file.");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Log file not found.");
        }
    }
}

class NewActivity : Activity
{
    public NewActivity() : base("This is a new activity that showcases creativity.") { }

    protected override void DoActivity()
    {
        Console.WriteLine("Starting New Activity for " + duration + " seconds.");
    }
}

class Program
{
    static Log activityLog = new Log();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Activity Menu:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. New Activity");
            Console.WriteLine("5. Show Activity Counts");
            Console.WriteLine("6. Save Log to File");
            Console.WriteLine("7. Load Log from File");
            Console.WriteLine("8. Quit");

            Console.Write("Enter your choice (1-8): ");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (choice)
            {
                case '1':
                    activityLog.LogActivity("Breathing");
                    new BreathingActivity().Start();
                    break;
                case '2':
                    activityLog.LogActivity("Reflection");
                    new ReflectionActivity().Start();
                    break;
                case '3':
                    activityLog.LogActivity("Listing");
                    new ListingActivity().Start();
                    break;
                case '4':
                    activityLog.LogActivity("NewActivity");
                    new NewActivity().Start();
                    break;
                case '5':
                    activityLog.ShowActivityCounts();
                    break;
                case '6':
                    activityLog.SaveLogToFile("activity_log.txt");
                    break;
                case '7':
                    activityLog.LoadLogFromFile("activity_log.txt");
                    break;
                case '8':
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 8.");
                    break;
            }
        }
    }
}


