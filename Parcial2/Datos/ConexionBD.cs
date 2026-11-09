using MySql.Data.MySqlClient;

namespace LabMVP.Datos;

public static class ConexionBD
{
    private const string Cadena =
        "Server=localhost;Port=3306;Database=labmvp_db;Uid=labmvp_app;Pwd=Isayalejo1.;SslMode=None;AllowPublicKeyRetrieval=True;";

    public static MySqlConnection Crear() => new MySqlConnection(Cadena);
}