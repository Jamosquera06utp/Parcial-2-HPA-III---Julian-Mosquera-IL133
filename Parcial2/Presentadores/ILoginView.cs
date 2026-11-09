namespace LabMVP.Presentadores;

public interface ILoginView
{
    string Correo { get; }
    string Password { get; }
    void MostrarError(string mensaje);
    void CerrarConExito();
}
