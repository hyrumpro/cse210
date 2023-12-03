class Program

{
    static void Main(string[] args)
    {

        Assignment simpleAssignment = new Assignment("Samuel Bennett", "Multiplication");
        string simpleSummary = simpleAssignment.GetSummary();
        Console.WriteLine(simpleSummary);

        MathAssignment mathAssignment = new MathAssignment("Roberto Rodriguez", "Fractions", "Section 7.3", "Problems 8-19");
        string mathSummary = mathAssignment.GetSummary();
        Console.WriteLine(mathSummary);

        string homeworkList = mathAssignment.GetHomeworkList();
        Console.WriteLine(homeworkList);

        WritingAssignment e4 = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
        Console.WriteLine(e4.GetSummary());
        Console.WriteLine(e4.GetWritingInformation());

    }
}



