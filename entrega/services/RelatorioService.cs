// Services/RelatorioHelper.cs

using DroneDeliverySimulator.Models;

namespace DroneDeliverySimulator.Services;

public static class Relatorio
{
    public static string GerarMapa(List<Ponto> posicoes)
    {
        const int largura = 20;
        const int altura = 10;
        char[,] mapa = new char[altura, largura];

        for (int y = 0; y < altura; y++)
            for (int x = 0; x < largura; x++)
                mapa[y, x] = '.';

        foreach (var p in posicoes)
        {
            int x = Math.Clamp((int)p.X, 0, largura - 1);
            int y = Math.Clamp((int)p.Y, 0, altura - 1);
            mapa[y, x] = 'X';
        }

        var linhas = new List<string>();
        for (int y = 0; y < altura; y++)
        {
            string linha = "";
            for (int x = 0; x < largura; x++)
            {
                linha += mapa[y, x];
            }
            linhas.Add(linha);
        }

        return string.Join("\n", linhas);
    }
}
