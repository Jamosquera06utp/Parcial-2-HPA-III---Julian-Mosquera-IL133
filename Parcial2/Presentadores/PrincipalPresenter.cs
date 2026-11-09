using System.Globalization;
using LabMVP.Entidades;

namespace LabMVP.Presentadores;

public class PrincipalPresenter
{
    private readonly IPrincipalView _vista;
    private static readonly CultureInfo Cultura = new("es-PA");

    public PrincipalPresenter(IPrincipalView vista) => _vista = vista;

    public void ActualizarHora()
    {
        var ahora = DateTime.Now;
        _vista.MostrarHora(ahora.ToString("HH:mm:ss"), ahora.ToString("dddd, d 'de' MMMM 'de' yyyy", Cultura));
    }

    public void CargarSesion()
    {
        var u = SesionUsuario.Actual;
        if (u != null) _vista.MostrarUsuario(u.Nombre, u.Correo);
    }
}
