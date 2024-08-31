namespace MindboxTask;

public class Triangle : Figure
{
    public double A { get; private set; }
    public double B { get; private set; }
    public double C { get; private set; }

    public Triangle(double a, double b, double c)
    {
        if (!IsValidTriangle(a, b, c))
            throw new ArgumentException("Введите корректные значение сторон треугольника");

        A = a;
        B = b;
        C = c;
    }

    public override double CalculateArea()
    {
        double p = (A + B + C) / 2;
        double s = Math.Round(Math.Sqrt(p * (p - A) * (p - B * (p - C))), 2);

        return s;
    }

    public bool IsRightTriangle()
    {
        double[] sides = { A, B, C };
        Array.Sort(sides);

        double a = sides[0];
        double b = sides[1];
        double c = sides[2];

        return c * c == (a * a + b * b);
    }

    private bool IsValidTriangle(double a, double b, double c)
    {
        return a + b > c && a + c > b && b + c > a;
    }
}