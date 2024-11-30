using KIPServiceTestTask.DTOs;
using KIPServiceTestTask.Entities;
using KIPServiceTestTask.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KIPServiceTestTask.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public ReportController(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    [HttpPost("user_statistics")]
    public async Task<IActionResult> Post([FromBody] UserStatisticRequest? model)
    {
        if (model == null || string.IsNullOrWhiteSpace(model.UserId.ToString()) || model.TimeIn >= model.TimeOut)
            return NotFound("ArgumentException");

        QueryModel query = new QueryModel()
        {
            QueryId = Guid.NewGuid(),
            UserData = new UserStatisticModel { Id = model.UserId, TimeIn = model.TimeIn, TimeOut = model.TimeOut },
            RequestLocalTime = DateTime.UtcNow,
        };

        await _dbContext.Queries.AddAsync(query);
        await _dbContext.SaveChangesAsync();

        return Ok(query.QueryId);
    }

    [HttpGet("info")]
    public async Task<IActionResult> Get(Guid queryId)
    {
        if (string.IsNullOrWhiteSpace(queryId.ToString()))
            return NotFound($"ArgumentException: {queryId}");

        var query = await _dbContext.Queries
            .AsNoTracking()
            .Include(q => q.UserData)
            .FirstOrDefaultAsync(q => q.QueryId == queryId);

        if (query == null)
            return NotFound("QueryIdNotFound");

        int percent = CalculatePercent(query.RequestLocalTime);

        QueryResponse response = new QueryResponse(query.QueryId, percent,
            percent == 100 ? new UserInfo(query.UserData.Id, query.Id) : null);

        return Ok(response);
    }

    private int CalculatePercent(DateTime startTime)
    {
        int maxProcessingTime = _configuration.GetValue<int>("MaxProcessingTime");

        var timeSpend = (int)(DateTime.UtcNow - startTime).TotalMilliseconds;
        var percent = Math.Min(100, (timeSpend * 100) / maxProcessingTime);

        return percent;
    }
}