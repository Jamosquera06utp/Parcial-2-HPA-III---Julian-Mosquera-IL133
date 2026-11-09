using LabMVP.Datos;
using LabMVP.Entidades;

namespace LabMVP.Negocio;

public class AutenticacionService : IAutenticacionService
{
    private readonly IUsuarioRepositorio _repo;
    private readonly IHashService _hash;
    private readonly IVerificarHashService _verificar;

    public AutenticacionService(IUsuarioRepositorio repo, IHashService hash, IVerificarHashService verificar)
    {
        _repo = repo;
        _hash = hash;
        _verificar = verificar;
    }

    public Usuario IniciarSesion(string correo, string password, out string error)
    {
        error = null;
        if (string.IsNullOrWhiteSpace(correo)) { error = "Ingrese el correo."; return null; }
        if (!Validador.CorreoValido(correo)) { error = "El correo no tiene un formato válido."; return null; }
        if (string.IsNullOrEmpty(password)) { error = "Ingrese la contraseña."; return null; }

        var usuario = _repo.ObtenerPorCorreo(correo);
        if (usuario == null || !_verificar.Verificar(password, usuario.PasswordHash))
        {
            error = "Correo o contraseña incorrectos.";
            return null;
        }
        return usuario;
    }

    public string Registrar(string nombre, string correo, string password, string confirmacion)
    {
        if (string.IsNullOrWhiteSpace(nombre) || nombre.Trim().Length < 3) return "El nombre debe tener al menos 3 caracteres.";
        if (!Validador.CorreoValido(correo)) return "El correo no tiene un formato válido.";

        var errorPass = Validador.ValidarPassword(password);
        if (errorPass != null) return errorPass;
        if (password != confirmacion) return "Las contraseñas no coinciden.";

        bool ok = _repo.Registrar(nombre.Trim(), correo.Trim(), _hash.GenerarHash(password));
        return ok ? null : "Ese correo ya está registrado.";
    }
}
