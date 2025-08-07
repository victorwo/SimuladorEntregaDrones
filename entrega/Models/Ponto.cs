public class Ponto
{
    public double X { get; set; }
    public double Y { get; set; }

    public double DistanciaAte(Ponto outro)
    {
        return Math.Sqrt(Math.Pow(X - outro.X, 2) + Math.Pow(Y - outro.Y, 2));
    }
}