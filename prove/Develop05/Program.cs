using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public enum GoalType
{
    Simple,
    Eternal,
    Checklist
}


public class Goal
{
    public string Name { get; set; }
    public GoalType Type { get; set; }
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }
    public int Value { get; set; }
    public int Bonus { get; set; }
    public bool Completed { get; set; }

    public Goal(string name, GoalType type, int value, int bonus = 0, int targetCount = 0)
    {
        Name = name;
        Type = type;
        Value = value;
        Bonus = bonus;
        TargetCount = targetCount;
        Completed = false;
    }
}

public class QuestManager
{
    private List<Goal> goals = new List<Goal>();
    private int totalScore = 5;

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordEvent(Goal goal)
    {
        Goal foundGoal = goals.Find(g => g == goal && !g.Completed);

        if (foundGoal != null)
        {
            switch (foundGoal.Type)
            {
                case GoalType.Simple:
                    foundGoal.Completed = true;
                    totalScore = 10;
                    break;
                case GoalType.Eternal:
                    totalScore += foundGoal.Value; 
                    break;
                case GoalType.Checklist:
                    foundGoal.CurrentCount++;
                    totalScore += foundGoal.Value;

                    if (foundGoal.CurrentCount >= foundGoal.TargetCount)
                    {
                        totalScore += foundGoal.Bonus;
                        foundGoal.Completed = true;
                    }
                    break;
            }
        }
    }

    

    public Goal GetGoalByName(string goalName)
    {
    return goals.Find(g => g.Name == goalName);
    }


    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {totalScore}");
    }

    public void DisplayGoals()
    {
        foreach (var goal in goals)
        {
            string completionStatus = goal.Completed ? "[X]" : "[ ]";
            string checklistProgress = goal.Type == GoalType.Checklist ? $"Completed {goal.CurrentCount}/{goal.TargetCount} times" : "";

            Console.WriteLine($"{goal.Name} - {completionStatus} {checklistProgress}");
        }
    }

    public void SaveGoals(string fileName)
    {
    try
    {
        string fileExtension = Path.GetExtension(fileName);

        if (fileExtension == ".json")
        {
            string jsonString = JsonSerializer.Serialize(goals);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine("Goals saved successfully as JSON.");
        }
        else if (fileExtension == ".txt")
        {
            List<string> lines = new List<string>();
            foreach (var goal in goals)
            {
                string line = $"{goal.Name},{goal.Type},{goal.Value},{goal.Bonus},{goal.TargetCount},{goal.Completed}";
                lines.Add(line);
            }

            File.WriteAllLines(fileName, lines);
            Console.WriteLine("Goals saved successfully as text file.");
        }
        else
        {
            Console.WriteLine("Unsupported file format.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving goals: {ex.Message}");
    }
    }
    public void LoadGoals(string fileName)
    {
    try
    {
        if (File.Exists(fileName))
        {
            string fileExtension = Path.GetExtension(fileName);

            if (fileExtension == ".json")
            {
                string jsonString = File.ReadAllText(fileName);
                goals = JsonSerializer.Deserialize<List<Goal>>(jsonString);
                Console.WriteLine("Goals loaded successfully from JSON.");
            }
            else if (fileExtension == ".txt")
            {
                List<Goal> loadedGoals = new List<Goal>();

                string[] lines = File.ReadAllLines(fileName);
                foreach (var line in lines)
                {
                    string[] data = line.Split(',');

                    if (data.Length >= 6 &&
                        Enum.TryParse(data[1], out GoalType type) &&
                        int.TryParse(data[2], out int value) &&
                        int.TryParse(data[3], out int bonus) &&
                        int.TryParse(data[4], out int targetCount))
                    {
                        Goal goal = new Goal(data[0], type, value, bonus, targetCount);
                        goal.Completed = bool.Parse(data[5]);
                        loadedGoals.Add(goal);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid data format in the file: {fileName}");
                        return;
                    }
                }

                goals = loadedGoals;
                Console.WriteLine("Goals loaded successfully from text file.");
            }
            else
            {
                Console.WriteLine("Unsupported file format.");
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading goals: {ex.Message}");
    }
   }

}





public class Activity
{
    public string Name { get; set; }
    public int Value { get; set; }
    public bool Completed { get; set; }

    public virtual void CompleteActivity()
    {
        Completed = true;
    }

    public virtual string GetActivityStatus()
    {
        return Completed ? "Completed" : "Incomplete";
    }
}

public class SimpleActivity : Activity
{
    public SimpleActivity(string name, int value)
    {
        Name = name;
        Value = value;
    }
}

public class EternalActivity : Activity
{
    public EternalActivity(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public override void CompleteActivity()
    {
        base.CompleteActivity();

    }
}


public class ChecklistActivity : Activity
{
    public int TargetCount { get; set; }
    public int CurrentCount { get; set; }

    public ChecklistActivity(string name, int value, int targetCount)
    {
        Name = name;
        Value = value;
        TargetCount = targetCount;
        Completed = false;
    }

    public override void CompleteActivity()
    {
        base.CompleteActivity();

    }

    public override string GetActivityStatus()
    {
        return $"{base.GetActivityStatus()} - Completed {CurrentCount}/{TargetCount} times";
    }
}





class Program
{
    static void Main(string[] args)
    {
        QuestManager manager = new QuestManager();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Goals");
            Console.WriteLine("4. Display Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Exit");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        AddGoal(manager);
                        break;
                    case 2:
                        RecordEvent(manager);
                        break;
                    case 3:
                        manager.DisplayGoals();
                        break;
                    case 4:
                        manager.DisplayScore();
                        break;
                    case 5:
                        Console.WriteLine("Select the format to save (JSON or TXT):");
                        string formatChoiceSave = Console.ReadLine().ToUpper();

                        Console.WriteLine("Enter the file name to save:");
                        string fileNameSave = Console.ReadLine();

                        switch (formatChoiceSave)
                        {
                            case "JSON":
                                manager.SaveGoals(fileNameSave + ".json");
                                break;
                            case "TXT":
                                manager.SaveGoals(fileNameSave + ".txt");
                                break;
                            default:
                                Console.WriteLine("Invalid format. Please choose JSON or TXT.");
                                break;
                        }
                        break;
                    case 6:
                        Console.WriteLine("Enter the file name to load:");
                        string fileNameLoad = Console.ReadLine();
                        manager.LoadGoals(fileNameLoad);
                        break;

                    case 7:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    static void AddGoal(QuestManager manager)
    {
        Console.WriteLine("Enter the goal name:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the goal type (Simple, Eternal, Checklist):");
        if (Enum.TryParse(Console.ReadLine(), out GoalType type))
        {
            int value = 0, bonus = 0, targetCount = 0;

            switch (type)
            {
                case GoalType.Simple:
                    Console.WriteLine("Enter the value for this goal:");
                    int.TryParse(Console.ReadLine(), out value);
                    break;
                case GoalType.Eternal:
                    Console.WriteLine("Enter the value for each completion:");
                    int.TryParse(Console.ReadLine(), out value);
                    break;
                case GoalType.Checklist:
                    Console.WriteLine("Enter the value for each completion:");
                    int.TryParse(Console.ReadLine(), out value);

                    Console.WriteLine("Enter the bonus value for achieving the goal:");
                    int.TryParse(Console.ReadLine(), out bonus);

                    Console.WriteLine("Enter the target count:");
                    int.TryParse(Console.ReadLine(), out targetCount);
                    break;
            }

            Goal goal = new Goal(name, type, value, bonus, targetCount);
            manager.AddGoal(goal);
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
        }
    }


    static void RecordEvent(QuestManager manager)
    {
    Console.WriteLine("Enter the event details (goal name):");
    string goalName = Console.ReadLine();

    Goal selectedGoal = manager.GetGoalByName(goalName);

    if (selectedGoal != null)
    {
        manager.RecordEvent(selectedGoal); 
    }
    else
    {
        Console.WriteLine("Goal not found.");
    }
    } 




}



