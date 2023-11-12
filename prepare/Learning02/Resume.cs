using System;
using System.Collections.Generic;

public class Resume
{
    private string _personName;
    private List<Job> _jobs;

    public Resume(string personName)
    {
        _personName = personName;
        _jobs = new List<Job>();
    }

    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }

    public void Display()
    {
        Console.WriteLine($"Name: {_personName}");
        Console.WriteLine("Jobs:");
        foreach (Job job in _jobs)
        {
            job.Display();
        }
    }
}
