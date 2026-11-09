namespace LabMVP.Presentadores;

public interface IPrincipalView
{
    void MostrarHora(string hora, string fecha);
    void MostrarUsuario(string nombre, string correo);
}
