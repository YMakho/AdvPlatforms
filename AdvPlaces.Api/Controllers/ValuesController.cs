using AdvPlaces.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace AdvPlaces.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAdvPlatformService _service;

        public ValuesController(IAdvPlatformService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<string> Create([FromQuery] string path)
        {
            await _service.Upload(path);
            return path;
        }
        [HttpGet]
        public ConcurrentBag<string> Search([FromQuery] string location)
        {
            return _service.FindPlatformsByLocation(location); 
        }
    }
}
