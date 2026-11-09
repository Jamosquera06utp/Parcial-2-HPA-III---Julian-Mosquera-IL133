using LabMVP.Entidades;

namespace LabMVP.Datos;

public interface IUsuarioRepositorio
{
    Usuario ObtenerPorCorreo(string correo);

    /// <summary>Devuelve false si el correo ya existe.</summary>
    bool Registrar(string nombre, string correo, string passwordHash);
}
