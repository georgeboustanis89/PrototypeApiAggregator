using Microsoft.AspNetCore.Mvc;
using PrototypeApiAggregator.Models;
using PrototypeApiAggregator.Services;
using System.Text.Json;

namespace PrototypeApiAggregator.Controllers
{
    [ApiController]
    public class AggregatorController : ControllerBase
    {
        private readonly AggregateService _aggregateService;

        public AggregatorController(AggregateService aggregateService)
        {
            _aggregateService = aggregateService;
        }


        [HttpGet("/all/{country}/{id}")]
        public async Task<ActionResult> Get(string country, int id, CancellationToken ct)
            => Ok(await _aggregateService.GetAll(id,country,ct));
    }
}
