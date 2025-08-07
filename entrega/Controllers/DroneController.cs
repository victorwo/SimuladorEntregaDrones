using Microsoft.AspNetCore.Mvc;
using DroneDeliverySimulator.Models;

namespace DroneDeliverySimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class DroneController : ControllerBase
{
    public static List<Drone> Drones { get; set; } = new();

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(Drones);
    }
}
