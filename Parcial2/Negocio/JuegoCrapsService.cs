namespace LabMVP.Negocio;

public enum EstadoCraps { Inicio, Punto, Gano, Perdio }

public class JuegoCrapsService
{
    private readonly Random _azar = new();

    public EstadoCraps Estado { get; private set; } = EstadoCraps.Inicio;
    public int Punto { get; private set; }
    public int Ganadas { get; private set; }
    public int Perdidas { get; private set; }

    public (int d1, int d2) Lanzar()
    {
        if (Estado == EstadoCraps.Gano || Estado == EstadoCraps.Perdio)
            Estado = EstadoCraps.Inicio;   // empieza una partida nueva

        int d1 = _azar.Next(1, 7), d2 = _azar.Next(1, 7), suma = d1 + d2;

        if (Estado == EstadoCraps.Inicio)
        {
            if (suma == 7 || suma == 11) Estado = EstadoCraps.Gano;
            else if (suma == 2 || suma == 3 || suma == 12) Estado = EstadoCraps.Perdio;
            else { Punto = suma; Estado = EstadoCraps.Punto; }
        }
        else // ya hay punto
        {
            if (suma == Punto) Estado = EstadoCraps.Gano;
            else if (suma == 7) Estado = EstadoCraps.Perdio;
        }

        if (Estado == EstadoCraps.Gano) Ganadas++;
        if (Estado == EstadoCraps.Perdio) Perdidas++;
        return (d1, d2);
    }
}
