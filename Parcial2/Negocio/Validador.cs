using System.Text.RegularExpressions;

namespace LabMVP.Negocio;

public static class Validador
{
    private static readonly Regex PatronCorreo = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public static bool CorreoValido(string correo) =>
        !string.IsNullOrWhiteSpace(correo) && PatronCorreo.IsMatch(correo);

    /// <summary>Devuelve el mensaje de error, o null si la contraseña es válida.</summary>
    public static string ValidarPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return "Ingrese la contraseña.";
        if (password.Length < 8) return "La contraseña debe tener al menos 8 caracteres.";
        if (!password.Any(char.IsUpper)) return "La contraseña debe tener al menos una mayúscula.";
        if (!password.Any(char.IsDigit)) return "La contraseña debe tener al menos un número.";
        return null;
    }
}
