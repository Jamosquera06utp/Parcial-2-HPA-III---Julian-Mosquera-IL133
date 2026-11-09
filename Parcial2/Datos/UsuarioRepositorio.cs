using System.Data;
using LabMVP.Entidades;
using MySql.Data.MySqlClient;

namespace LabMVP.Datos;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    public Usuario ObtenerPorCorreo(string correo)
    {
        using var cn = ConexionBD.Crear();
        using var cmd = new MySqlCommand("CALL sp_usuario_obtener_por_correo(@correo)", cn);
        cmd.Parameters.AddWithValue("@correo", correo);
        cn.Open();

        using var rd = cmd.ExecuteReader();
        if (!rd.Read()) return null;

        return new Usuario
        {
            Id = rd.GetInt32("id"),
            Nombre = rd.GetString("nombre"),
            Correo = rd.GetString("correo"),
            PasswordHash = rd.GetString("password_hash")
        };
    }

    public bool Registrar(string nombre, string correo, string passwordHash)
    {
        try
        {
            using var cn = ConexionBD.Crear();
            using var cmd = new MySqlCommand("CALL sp_usuario_registrar(@nombre, @correo, @hash)", cn);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@correo", correo);
            cmd.Parameters.AddWithValue("@hash", passwordHash);
            cn.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (MySqlException ex) when (ex.Number == 1062) // correo duplicado
        {
            return false;
        }
    }
}
