using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using DroneDeliverySimulator.Controllers;
using DroneDeliverySimulator.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona controllers + enum como string + validações personalizadas
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var erros = context.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return new BadRequestObjectResult(new
            {
                mensagem = "Erro de validação",
                erros
            });
        };
    })
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

// Carrega dados do JSON (pedidos e drones)
new PedidoService().lerPedidosDoArquivo();
new DroneService().lerDronesDoArquivo();

// Endpoint de simulação de entregas
app.MapPost("/simular", () =>
{
    var service = new EntregaService(DroneController.Drones, PedidoController.Pedidos);
    service.AlocarPedidosAsync();

    var relatorio = DroneController.Drones.Select(d => new
    {
        droneId = d.Id,
        status = d.Status.ToString()
    });

    return Results.Ok(new
    {
        mensagem = "Simulação concluída!",
        pedidosEntregues = PedidoController.Pedidos.Count(p => p.Entregue),
        pedidosPendentes = PedidoController.Pedidos.Count(p => !p.Entregue),
        drones = relatorio
    });
});

// Endpoint de dashboard visual em HTML
app.MapGet("/dashboard", () =>
{
    var service = new EntregaService(DroneController.Drones, PedidoController.Pedidos);
    service.AlocarPedidosAsync();

    var relatorio = service.RelatorioEntregas;
    var totalEntregas = relatorio.Sum(r => r.QuantidadeEntregas);
    var tempoTotal = relatorio.Sum(r => r.TempoTotal);
    var tempoMedio = totalEntregas > 0 ? (double)tempoTotal / totalEntregas : 0;
    var droneMaisEficiente = relatorio.OrderByDescending(r => r.QuantidadeEntregas).FirstOrDefault();

    var entregasPorDrone = relatorio
        .Select(r => $"""
            <div class="drone-card">
                <span class="drone-icon"></span>
                <div>
                    <p><strong>ID:</strong> {r.DroneId}</p>
                    <p><strong>Entregas:</strong> {r.QuantidadeEntregas}</p>
                </div>
            </div>
        """);

    var dronesHtml = string.Join("\n", entregasPorDrone);

    var html = $"""
        <!DOCTYPE html>
        <html>
        <head>
            <title>Dashboard das Entregas</title>
            <link rel="stylesheet" href="css/estilo.css">
        </head>
        <body>
            <h1>Dashboard das Entregas</h1>

            <div class="card">
                <p><strong> Entregas realizadas:</strong> {totalEntregas}</p>
                <p><strong> Tempo médio por entrega:</strong> {tempoMedio:0.00} min</p>
                <p><strong> Drone mais eficiente:</strong> {droneMaisEficiente?.DroneId}</p>
            </div>

            <h2>Entregas por Drone</h2>
            <div class="drone-list">
                {dronesHtml}
            </div>
        </body>
        </html>
    """;

    return Results.Content(html, "text/html");
});

app.Run();
