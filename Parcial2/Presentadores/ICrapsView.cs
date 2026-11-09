namespace LabMVP.Presentadores;

public interface ICrapsView
{
    void MostrarDados(int d1, int d2);
    void MostrarPunto(int? punto);
    /// <param name="gano">true = victoria, false = derrota, null = partida en curso</param>
    void MostrarMensaje(string texto, bool? gano);
    void MostrarMarcador(int ganadas, int perdidas);
}
