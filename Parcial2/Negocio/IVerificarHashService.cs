namespace LabMVP.Negocio;

/// <summary>Contrato 2: su ÚNICA responsabilidad es verificar texto plano contra un hash almacenado.</summary>
public interface IVerificarHashService
{
    bool Verificar(string textoPlano, string hashAlmacenado);
}
