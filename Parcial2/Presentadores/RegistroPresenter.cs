using LabMVP.Negocio;

namespace LabMVP.Presentadores;

public class RegistroPresenter
{
    private readonly IRegistroView _vista;
    private readonly IAutenticacionService _auth;

    public RegistroPresenter(IRegistroView vista, IAutenticacionService auth)
    {
        _vista = vista;
        _auth = auth;
    }

    public void Registrar()
    {
        try
        {
            string error = _auth.Registrar(_vista.Nombre, _vista.Correo, _vista.Password, _vista.Confirmacion);
            if (error != null) _vista.MostrarError(error);
            else _vista.MostrarExito("Usuario registrado correctamente.");
        }
        catch (Exception ex)
        {
            _vista.MostrarError("No se pudo conectar con la base de datos: " + ex.Message);
        }
    }
}
