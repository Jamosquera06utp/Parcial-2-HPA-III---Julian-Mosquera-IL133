using LabMVP.Entidades;

namespace LabMVP.Negocio;

public interface IAutenticacionService
{
    /// <summary>Devuelve el usuario, o null y el mensaje en 'error'.</summary>
    Usuario IniciarSesion(string correo, string password, out string error);

    /// <summary>Devuelve el mensaje de error, o null si se registró bien.</summary>
    string Registrar(string nombre, string correo, string password, string confirmacion);
}
