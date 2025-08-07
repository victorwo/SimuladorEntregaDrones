using System.ComponentModel.DataAnnotations;

namespace DroneDeliverySimulator.Models;

public class Pedido
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Ponto Coordenadas { get; set; }

    [Range(0.1, double.MaxValue, ErrorMessage = "Peso deve ser maior que zero.")]
    public double Peso { get; set; }

    [Required]
    public Prioridade Prioridade { get; set; }

    public bool Entregue { get; set; } = false;
}
