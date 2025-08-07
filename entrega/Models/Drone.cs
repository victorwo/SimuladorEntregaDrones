using DroneDeliverySimulator.Enums;

namespace DroneDeliverySimulator.Models;

public class Drone
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public double CapacidadeMaxKg { get; set; }
    public double AlcanceMaxKm { get; set; }
    public DroneStatus Status { get; set; } = DroneStatus.Idle;
    public Ponto LocalAtual { get; set; } = new Ponto { X = 0, Y = 0 };
    public List<Pedido> PacotesAtual { get; set; } = new();
}
