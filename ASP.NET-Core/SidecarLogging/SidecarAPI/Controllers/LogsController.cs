using Microsoft.AspNetCore.Mvc;
using SidecarAPI.Infrastructure;
using SidecarAPI.Models;

namespace SidecarAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    private readonly IElasticSearchClientService _elasticSearchClientService;
    private readonly ILogger<LogsController> _logger;
    public LogsController(IElasticSearchClientService elasticSearchClientService,
        ILogger<LogsController> logger)
    {
        _elasticSearchClientService = elasticSearchClientService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<LogMessage>>> Get()
    {
        try
        {
            var logs = await _elasticSearchClientService.GetAllLogsAsync();
            return Ok(logs.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch logs from Elasticsearch");
            return StatusCode(500);
        }
    }
}
