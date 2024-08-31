using Calculator.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Tests;

public class CalculatorControllerTests
{
    private readonly CalculatorController _calculatorController = new();

    [Theory]
    [InlineData(5, 5, 10)]
    [InlineData(-5, 5, 0)]
    [InlineData(-15, -5, -20)]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreDoubles(double x, double y, double expected)
    {
        var result = _calculatorController.Add(x, y);

        result.Value.Should().Be(expected);
    }

    [Theory]
    [InlineData(5, 5, 0)]
    [InlineData(15, 5, 10)]
    [InlineData(-5, -5, 0)]
    public void Subtract_ShouldSubtractTwoNumber_WhenTwoNumbersAreDoubles(double x, double y, double expected)
    {
        var result = _calculatorController.Subtract(x, y);

        result.Value.Should().Be(expected);
    }

    [Theory]
    [InlineData(5, 5, 25)]
    [InlineData(50, 0, 0)]
    [InlineData(-5, 5, -25)]
    public void Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreDoubles(double x, double y, double expected)
    {
        var result = _calculatorController.Multiply(x, y);

        result.Value.Should().Be(expected);
    }

    [Theory]
    [InlineData(5, 5, 1)]
    [InlineData(15, 5, 3)]
    [InlineData(121, 11, 11)]
    public void Divide_ShouldDivideTwoNumbers_WhenTwoNumbersAreDoubles(double x, double y, double expected)
    {
        var result = _calculatorController.Divide(x, y);

        result.Value.Should().Be(expected);
    }

    [Fact]
    public void Divide_ShouldReturnError_WhenDivisorIsZero()
    {
        var result = _calculatorController.Divide(6, 0);

        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }
}