using Microsoft.AspNetCore.Mvc;
using DroneDeliverySimulator.Models;
using DroneDeliverySimulator.Enums;

namespace DroneDeliverySimulator.Controllers;

[ApiController]
[Route("[controller]")]
public class PedidoController : ControllerBase
{
    public static List<Pedido> Pedidos { get; set; } = new();
    private readonly ILogger<PedidoController> _logger;

    public PedidoController(ILogger<PedidoController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(Pedidos);
    }

    [HttpPost]
    public IActionResult Criar([FromBody] Pedido pedido)
    {
        if (pedido == null)
            return BadRequest("Pedido inválido.");

        if (pedido.Peso <= 0)
            return BadRequest("O peso deve ser maior que zero.");

        if (pedido.Coordenadas == null)
            return BadRequest("Coordenadas são obrigatórias.");

        if (!Enum.IsDefined(typeof(Prioridade), pedido.Prioridade))
            return BadRequest("Prioridade inválida.");

        Pedidos.Add(pedido);
        _logger.LogInformation($"Pedido criado: {pedido.Id}");

        return Ok(new { mensagem = "Pedido criado com sucesso", pedidoId = pedido.Id });
    }
}