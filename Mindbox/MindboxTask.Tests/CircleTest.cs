namespace MindboxTask.Tests;

public class CircleTest
{
    [Fact]
    public void CalculateArea_SingleNumber_ReturnsArea()
    {
        // Arrange
        int radius = 5;
        double expected = 78.54;
        var circle = new Circle(radius);

        // Act
        double actual = circle.CalculateArea();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CalculateArea_NegativeNumber_ArgumentException()
    {
        //Arrange
        double radius = -3;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Circle(radius));
    }
}