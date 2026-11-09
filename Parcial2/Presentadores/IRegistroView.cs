namespace LabMVP.Presentadores;

public interface IRegistroView
{
    string Nombre { get; }
    string Correo { get; }
    string Password { get; }
    string Confirmacion { get; }
    void MostrarError(string mensaje);
    void MostrarExito(string mensaje);
}
