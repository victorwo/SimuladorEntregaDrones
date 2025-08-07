# Simulador de Entregas com Drones

Este projeto simula um sistema de logística com entregas feitas por drones em uma cidade 2D. O objetivo é alocar pedidos nos drones com o menor número de viagens possível, respeitando regras de peso, distância e prioridade de entrega.

## Tecnologias Utilizadas

- C# / .NET 8
- ASP.NET Core Web API
- Swagger (Swashbuckle)
- xUnit (testes automatizados)
- JSON como fonte de dados
- HTML (Dashboard visual)

## Como Executar

### 1. Clone o repositório

```
git clone https://github.com/victorwo/SimuladorEntregaDrones.git
cd simuladorEntregaDrones
cd entrega 
```

### 2. Restaure os pacotes e rode o projeto

```
dotnet restore
dotnet run
```

A aplicação estará disponível em:

- http://localhost:5000/swagger → Swagger (API)
- http://localhost:5000/dashboard → Dashboard HTML



## Endpoints da API

| Método | Rota               | Descrição                           |
|--------|--------------------|-------------------------------------|
| POST   | /pedidos           | Cria um novo pedido                 |
| GET    | /pedidos           | Lista todos os pedidos              |
| GET    | /drones            | Lista os drones disponíveis         |
| POST   | /simular           | Roda a simulação das entregas       |
| GET    | /dashboard         | Mostra o dashboard em HTML          |

## Lógica de Otimização

A alocação de pacotes é feita com um algoritmo guloso que considera:

1. Prioridade da entrega (Alta > Média > Baixa)
2. Distância até o destino
3. Capacidade e alcance do drone

Objetivo: maximizar o uso do drone por viagem, respeitando os limites operacionais.

## Funcionalidades Extras

- Algoritmo guloso com priorização
- Validação de pedidos (peso, prioridade, coordenadas)
- Rejeição de pacotes inválidos
- Simulação de estados dos drones (Idle → EmVoo → Entregando → Retornando)
- Dashboard visual com:
  - Entregas realizadas
  - Tempo médio por entrega
  - Drone mais eficiente
  - Visualização por ícones de drones

## Testes Automatizados

Rodar os testes com:

```
dotnet test
```

Testes cobrem:

- Validação de pedidos
- Simulação de alocação de pacotes
- Limites de peso e alcance dos drones

## Estrutura de Arquivos

```
├── Controllers/
│   ├── PedidoController.cs
│   └── DroneController.cs
├── Models/
│   ├── Pedido.cs
│   ├── Drone.cs
│   ├── Ponto.cs
│   └── RelatorioDrone.cs
├── Enums/
│   ├── Prioridade.cs
│   └── DroneStatus.cs
├── Services/
│   ├── EntregaService.cs
│   ├── PedidoService.cs
│   └── DroneService.cs
├── pedidos.json
├── drones.json
├── Program.cs
├── README.md
└── DroneDeliverySimulator.Tests/
    └── EntregaServiceTests.cs
```

## Como foram usadas ferramentas de IA

Durante o desenvolvimento foram utilizados recursos de IA (ChatGPT) para:

- Estruturar a arquitetura do projeto
- Escrever partes do algoritmo guloso
- Criar mensagens de validação e respostas de erro
- Refatorar o código para legibilidade e boas práticas


## Requisitos atendidos

- Alocação de pedidos com menor número de viagens
- Regras de capacidade e alcance
- Validação e mensagens claras
- Otimização inteligente por peso, prioridade e distância
- Relatório HTML simples com visual
- Testes unitários

Feito por Victor Wilson.
