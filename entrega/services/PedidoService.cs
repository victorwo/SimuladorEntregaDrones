using System.Text.Json;
using System.Text.Json.Serialization; // Add this namespace
using DroneDeliverySimulator.Models;
using DroneDeliverySimulator.Controllers;

namespace DroneDeliverySimulator.Services;

public class PedidoService
{
    private const string caminho = "json/pedidos.json"; // Ensure path is correct
    

    public void lerPedidosDoArquivo()
    {
        if (File.Exists(caminho))
        {
            var json = File.ReadAllText(caminho);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Match JSON property names case-insensitively
                Converters = { new JsonStringEnumConverter() } // Convert enum strings to enum values
            };

            var pedidos = JsonSerializer.Deserialize<List<Pedido>>(json, options);

            if (pedidos != null)
            {
                PedidoController.Pedidos = pedidos;
            }
        }
    }
    
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