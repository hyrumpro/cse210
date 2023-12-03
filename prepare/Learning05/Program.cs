class Program
{
    static void Main(string[] args)
    {

        Shape shape = new Shape("Red");
        Console.WriteLine($"Shape Color: {shape.Color}");
        double area = shape.GetArea();
        Console.WriteLine($"Shape Area: {area}");


        Square square = new Square("Blue", 5.0);
        Console.WriteLine($"Square Color: {square.Color}");
        double squareArea = square.GetArea();
        Console.WriteLine($"Square Area: {squareArea}");


        Rectangle rectangle = new Rectangle("Green", 4.0, 6.0);
        Console.WriteLine($"Rectangle Color: {rectangle.Color}");
        double rectangleArea = rectangle.GetArea();
        Console.WriteLine($"Rectangle Area: {rectangleArea}");


        Circle circle = new Circle("Yellow", 3.0);
        Console.WriteLine($"Circle Color: {circle.Color}");
        double circleArea = circle.GetArea();
        Console.WriteLine($"Circle Area: {circleArea}");

    }
}