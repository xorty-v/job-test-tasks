using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculatorController : ControllerBase
{
    [HttpGet("add")]
    public ActionResult<double> Add(double x, double y)
    {
        return x + y;
    }

    [HttpGet("subtract")]
    public ActionResult<double> Subtract(double x, double y)
    {
        return x - y;
    }

    [HttpGet("multiply")]
    public ActionResult<double> Multiply(double x, double y)
    {
        return x * y;
    }

    [HttpGet("divide")]
    public ActionResult<double> Divide(double x, double y)
    {
        if (y == 0)
            return BadRequest("Division by zero is not allowed.");

        return x / y;
    }
}