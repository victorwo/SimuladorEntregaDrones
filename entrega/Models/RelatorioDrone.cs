public class RelatorioDrone
{
    public Guid DroneId { get; set; }
    public int QuantidadeEntregas { get; set; }
    public int TempoTotal { get; set; } // minutos
    public List<Guid> PedidosEntregues { get; set; } = new();
    public List<Ponto> Posições { get; set; } = new();
}
