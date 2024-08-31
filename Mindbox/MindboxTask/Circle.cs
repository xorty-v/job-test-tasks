namespace MindboxTask;

public class Circle : Figure
{
    public double Radius { get; private set; }

    public Circle(double radius)
    {
        if (!IsValidCircle(radius))
            throw new ArgumentException("Введите корректное значение радиуса круга");

        Radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.Round(Math.PI * Math.Pow(Radius, 2), 2);
    }

    private bool IsValidCircle(double radius)
    {
        return radius > 0;
    }
}