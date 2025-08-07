using Xunit;
using DroneDeliverySimulator.Models;
using DroneDeliverySimulator.Enums;
using DroneDeliverySimulator.Services;
using System.Collections.Generic;

namespace entregaSimulator.Tests;

public class EntregaServiceTests
{
    [Fact]
    public void Deve_Alocar_Pedidos_Com_Sucesso()
    {
        // Arrange
        var drones = new List<Drone>
        {
            new Drone
            {
                Id = Guid.NewGuid(),
                CapacidadeMaxKg = 10,
                AlcanceMaxKm = 50,
                LocalAtual = new Ponto { X = 0, Y = 0 },
                Status = DroneStatus.Idle
            }
        };

        var pedidos = new List<Pedido>
        {
            new Pedido
            {
                Id = Guid.NewGuid(),
                Peso = 5,
                Coordenadas = new Ponto { X = 3, Y = 4 },
                Prioridade = Prioridade.Alta,
                Entregue = false
            }
        };

        var service = new EntregaService(drones, pedidos);

        // Act
        service.AlocarPedidosAsync();

        // Assert
        Assert.True(pedidos[0].Entregue);
        Assert.Equal(DroneStatus.Idle, drones[0].Status); 
    }
}


