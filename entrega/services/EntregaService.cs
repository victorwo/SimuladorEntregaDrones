using DroneDeliverySimulator.Models;

namespace DroneDeliverySimulator.Services;

public class EntregaService
{
    private readonly List<Drone> _drones;
    private readonly List<Pedido> _pedidos;
    public List<RelatorioDrone> RelatorioEntregas { get; private set; } = new();


    public EntregaService(List<Drone> drones, List<Pedido> pedidos)
    {
        _drones = drones;
        _pedidos = pedidos;
    }

    public async Task AlocarPedidosAsync(){
        while (_pedidos.Any(p => !p.Entregue))
        {
            foreach (var drone in _drones)
            {
                if (drone.Status != DroneStatus.Idle) continue;

                var pedidosPendentes = _pedidos
                    .Where(p => !p.Entregue)
                    .OrderByDescending(p => p.Prioridade)
                    .ThenBy(p => p.Peso)
                    .ThenBy(p => Distancia(new Ponto { X = 0, Y = 0 }, p.Coordenadas))
                    .ToList();

                var carga = new List<Pedido>();
                double pesoTotal = 0;

                foreach (var pedido in pedidosPendentes.ToList())
                {
                    double distanciaIdaVolta = 2 * Distancia(drone.LocalAtual, pedido.Coordenadas);

                    if (pesoTotal + pedido.Peso <= drone.CapacidadeMaxKg &&
                        distanciaIdaVolta <= drone.AlcanceMaxKm)
                    {
                        carga.Add(pedido);
                        pesoTotal += pedido.Peso;
                        pedidosPendentes.Remove(pedido);
                    }
                }

                if (carga.Any())
                {
                    drone.Status = DroneStatus.Carregando;
                    //await Task.Delay(500); // simula carregamento

                    drone.PacotesAtual = carga;
                    drone.Status = DroneStatus.EmVoo;
                    //await Task.Delay(1000); // simula voo até entrega

                    foreach (var p in carga)
                        p.Entregue = true;

                    RelatorioEntregas.Add(new RelatorioDrone
                    {
                        DroneId = drone.Id,
                        QuantidadeEntregas = carga.Count,
                        TempoTotal = carga.Count * 15,
                        PedidosEntregues = carga.Select(p => p.Id).ToList(),
                        Posições = carga.Select(p => p.Coordenadas).ToList()
                    });

                    drone.Status = DroneStatus.Entregando;
                    //await Task.Delay(1000); // simula entrega

                    drone.Status = DroneStatus.Retornando;
                    //await Task.Delay(1000); // simula retorno
                    drone.LocalAtual = new Ponto { X = 0, Y = 0 };

                    drone.Status = DroneStatus.Idle;
                    drone.PacotesAtual.Clear();
                }
            }
        }
    }


    private double Distancia(Ponto a, Ponto b)
    {
        return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
    }
}
