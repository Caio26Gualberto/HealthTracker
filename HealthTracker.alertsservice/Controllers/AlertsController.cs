using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    [HttpPost]
    public IActionResult PostVitals([FromBody] VitalData data)
    {
        var alerts = CheckAlerts(data);

        if (alerts.Any())
        {
            foreach (var alert in alerts)
            {
                Console.WriteLine(alert);
            }
            return BadRequest(string.Join("; ", alerts));
        }

        return Ok("Vitals are normal");
    }

    private List<string> CheckAlerts(VitalData data)
    {
        var alerts = new List<string>();

        if (data.HeartRate < 40 || data.HeartRate > 120)
            alerts.Add($"Abnormal heart rate detected: {data.HeartRate}");

        if (data.Oxygen < 90)
            alerts.Add($"Low oxygen level detected: {data.Oxygen}");

        if (data.Systolic < 90 || data.Systolic > 140)
            alerts.Add($"Abnormal systolic pressure detected: {data.Systolic}");

        if (data.Diastolic < 60 || data.Diastolic > 90)
            alerts.Add($"Abnormal diastolic pressure detected: {data.Diastolic}");

        return alerts;
    }
}

public class VitalData
{
    public int HeartRate { get; set; }
    public int Oxygen { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
}
