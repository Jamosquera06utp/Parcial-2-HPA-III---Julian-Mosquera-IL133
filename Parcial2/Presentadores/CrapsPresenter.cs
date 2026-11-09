using LabMVP.Negocio;

namespace LabMVP.Presentadores;

public class CrapsPresenter
{
    private readonly ICrapsView _vista;
    private readonly JuegoCrapsService _juego;

    public CrapsPresenter(ICrapsView vista, JuegoCrapsService juego)
    {
        _vista = vista;
        _juego = juego;
    }

    public void Lanzar()
    {
        var (d1, d2) = _juego.Lanzar();
        _vista.MostrarDados(d1, d2);

        switch (_juego.Estado)
        {
            case EstadoCraps.Gano:
                _vista.MostrarMensaje($"¡Ganaste! (sacaste {d1 + d2})", true);
                _vista.MostrarPunto(null);
                break;
            case EstadoCraps.Perdio:
                _vista.MostrarMensaje($"Perdiste (sacaste {d1 + d2})", false);
                _vista.MostrarPunto(null);
                break;
            default:
                _vista.MostrarPunto(_juego.Punto);
                _vista.MostrarMensaje($"Tu punto es {_juego.Punto}. Sigue lanzando: sacas {_juego.Punto} y ganas, sacas 7 y pierdes.", null);
                break;
        }
        _vista.MostrarMarcador(_juego.Ganadas, _juego.Perdidas);
    }
}
