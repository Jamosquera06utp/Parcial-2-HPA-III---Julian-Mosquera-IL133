namespace LabMVP.Negocio;

/// <summary>Contrato 1: su ÚNICA responsabilidad es generar el hash.</summary>
public interface IHashService
{
    string GenerarHash(string textoPlano);
}
