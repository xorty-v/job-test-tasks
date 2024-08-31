namespace MindboxTask.Tests;

public class TriangleTest
{
    [Fact]
    public void CalculateArea_Numbers_ReturnsArea()
    {
        //Arrange
        double a = 3;
        double b = 4;
        double c = 5;
        double expected = 6;
        Triangle triangle = new Triangle(a, b, c);

        //Act
        double actual = triangle.CalculateArea();

        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IsRightTriangle_Numbers_ReturnsTrue()
    {
        //Arrange
        double a = 4;
        double b = 5;
        double c = 3;
        bool expected = true;
        Triangle triangle = new Triangle(a, b, c);

        //Act
        bool actual = triangle.IsRightTriangle();

        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IsRightTriangle_Numbers_ReturnsFalse()
    {
        //Arrange
        double a = 8;
        double b = 15;
        double c = 16;
        bool expected = false;
        Triangle triangle = new Triangle(a, b, c);

        //Act
        bool actual = triangle.IsRightTriangle();

        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateArea_InvalidData_ArgumentException()
    {
        //Arrange
        double a = 1;
        double b = 1;
        double c = 7;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));
    }
}