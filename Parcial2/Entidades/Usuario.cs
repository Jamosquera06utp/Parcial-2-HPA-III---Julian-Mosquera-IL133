namespace LabMVP.Entidades;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    // El salt va incluido dentro del hash (formato: iteraciones.salt.hash)
    public string PasswordHash { get; set; }
}
