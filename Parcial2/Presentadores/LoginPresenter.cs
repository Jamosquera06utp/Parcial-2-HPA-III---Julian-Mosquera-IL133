using LabMVP.Entidades;
using LabMVP.Negocio;

namespace LabMVP.Presentadores;

public class LoginPresenter
{
    private readonly ILoginView _vista;
    private readonly IAutenticacionService _auth;

    public LoginPresenter(ILoginView vista, IAutenticacionService auth)
    {
        _vista = vista;
        _auth = auth;
    }

    public void IniciarSesion()
    {
        try
        {
            var usuario = _auth.IniciarSesion(_vista.Correo, _vista.Password, out string error);
            if (usuario == null) { _vista.MostrarError(error); return; }

            SesionUsuario.Actual = usuario;
            _vista.CerrarConExito();
        }
        catch (Exception ex)
        {
            _vista.MostrarError("No se pudo conectar con la base de datos: " + ex.Message);
        }
    }
}
