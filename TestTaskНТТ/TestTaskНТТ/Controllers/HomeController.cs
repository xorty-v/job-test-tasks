using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskНТТ.Persistence;

namespace TestTaskНТТ.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public HomeController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public async Task<IActionResult> GetData()
    {
        var result = await _dbContext.Products
            .Include(p => p.Category)
            .AsNoTracking()
            .ToListAsync();

        return View(result);
    }
}