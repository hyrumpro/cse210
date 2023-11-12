using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Job job1 = new Job("Software Engineer", "Microsoft", 2019, 2022);
        Job job2 = new Job("Manager", "Apple", 2022, 2023);

        job1.Display();
        job2.Display();

        Resume myResume = new Resume("Allison Rose");

        myResume.AddJob(job1);
        myResume.AddJob(job2);

        myResume.Display();
    }
}
