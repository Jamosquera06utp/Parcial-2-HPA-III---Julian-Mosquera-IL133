using System.Security.Cryptography;

namespace LabMVP.Negocio;

/// <summary>
/// Implementación con PBKDF2-SHA256 + salt aleatorio (sin paquetes extra).
/// Si mañana quieren BCrypt/Argon2, solo se cambia esta clase.
/// </summary>
public class Pbkdf2PasswordService : IHashService, IVerificarHashService
{
    private const int TamSalt = 16;
    private const int TamHash = 32;
    private const int Iteraciones = 100_000;

    public string GenerarHash(string textoPlano)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(TamSalt);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(textoPlano, salt, Iteraciones, HashAlgorithmName.SHA256, TamHash);
        return $"{Iteraciones}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    public bool Verificar(string textoPlano, string hashAlmacenado)
    {
        try
        {
            var partes = hashAlmacenado.Split('.');
            if (partes.Length != 3 || !int.TryParse(partes[0], out int iter)) return false;

            byte[] salt = Convert.FromBase64String(partes[1]);
            byte[] esperado = Convert.FromBase64String(partes[2]);
            byte[] calculado = Rfc2898DeriveBytes.Pbkdf2(textoPlano, salt, iter, HashAlgorithmName.SHA256, esperado.Length);

            return CryptographicOperations.FixedTimeEquals(calculado, esperado);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
