using LabMVP.Datos;
using LabMVP.Negocio;
using LabMVP.Vistas;

namespace LabMVP;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Composición: aquí se "enchufan" las implementaciones a los contratos.
        IUsuarioRepositorio repo = new UsuarioRepositorio();
        var hash = new Pbkdf2PasswordService();               // implementa los DOS contratos
        IAutenticacionService auth = new AutenticacionService(repo, hash, hash);

        while (true)
        {
            using var login = new FrmLogin(auth);
            if (login.ShowDialog() != DialogResult.OK) break;

            using var principal = new FrmPrincipal(auth);
            Application.Run(principal);
            if (!principal.CerrarSesionSolicitado) break;
        }
    }
}
