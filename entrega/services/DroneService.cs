using System.Text.Json;
using DroneDeliverySimulator.Models;
using DroneDeliverySimulator.Controllers;

namespace DroneDeliverySimulator.Services;

public class DroneService
{
    private const string caminho = "json/drones.json";

    public void lerDronesDoArquivo()
    {
        if (File.Exists(caminho))
        {
            var json = File.ReadAllText(caminho);
            var drones = JsonSerializer.Deserialize<List<Drone>>(json);

            if (drones != null)
            {
                DroneController.Drones = drones;
            }
        }
    }
}
