
using System.IO;
using GOF.Domain.Configurations;
using GOF.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GOF.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DiagnosticsController> _logger;

        public DiagnosticsController(IConfiguration configuration, ILogger<DiagnosticsController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet("check-db")]
        public IActionResult CheckDatabaseFile()
        {
            // Obtém a connection string do appsettings.json
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Loga a connection string
            _logger.LogInformation($"ConnectionString: {connectionString}");

            var dbPath = "/app/data";
            if (Directory.Exists(dbPath))
            {
                var files = Directory.GetFiles(dbPath);
                return Ok(new
                {
                    ConnectionString = connectionString,
                    Files = files
                });
            }

            return NotFound(new { Message = "Directory not found.", ConnectionString = connectionString });
        }

        [HttpGet("list-tables")]
        public IActionResult ListTables([FromServices] SQLiteDbContext context)
        {
            var tables = context.Database.ExecuteSqlRaw("SELECT name FROM sqlite_master WHERE type='table';");
            return Ok(tables);
        }
    }
}