using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace HealthTracker.Vitalsservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VitalsController : Controller
    {
        private readonly ILogger<VitalsController> _logger;
        private readonly HttpClient _httpClient;

        public VitalsController(ILogger<VitalsController> logger, HttpClient httpClient )
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> PostVitals([FromBody] VitalData data)
        {
            Console.WriteLine($"Received vitals: {JsonSerializer.Serialize(data)}");

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7020/api/alerts", data);

            if (!response.IsSuccessStatusCode)
            {
                var alertMessages = await response.Content.ReadAsStringAsync();
                return BadRequest(alertMessages);
            }

            return Ok("Vitals processed successfully");
        }
    }

    public class VitalData
    {
        public int HeartRate { get; set; }
        public int Oxygen { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
    }
}
