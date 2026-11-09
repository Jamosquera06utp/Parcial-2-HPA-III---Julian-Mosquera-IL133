using LabMVP.Negocio;
using LabMVP.Presentadores;

namespace LabMVP.Vistas;

public class FrmLogin : Form, ILoginView
{
    private readonly IAutenticacionService _auth;
    private readonly LoginPresenter _presenter;

    private readonly TextBox txtCorreo = EstiloUI.Caja(40, 170, 300);
    private readonly TextBox txtPassword = EstiloUI.Caja(40, 240, 300, true);
    private readonly Label lblError = new()
    {
        Location = new Point(40, 280), Size = new Size(300, 44),
        ForeColor = Color.Firebrick, Font = new Font("Segoe UI", 9f)
    };

    public string Correo => txtCorreo.Text.Trim();
    public string Password => txtPassword.Text;

    public FrmLogin(IAutenticacionService auth)
    {
        _auth = auth;
        _presenter = new LoginPresenter(this, auth);

        Text = "Iniciar sesión";
        ClientSize = new Size(380, 460);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        BackColor = Color.White;

        var header = new Panel { Dock = DockStyle.Top, Height = 110, BackColor = EstiloUI.Azul };
        header.Controls.Add(new Label
        {
            Text = "LabMVP", ForeColor = Color.White, AutoSize = true,
            Font = new Font("Segoe UI", 24f, FontStyle.Bold), Location = new Point(36, 22)
        });
        header.Controls.Add(new Label
        {
            Text = "Inicia sesión para continuar", ForeColor = Color.White, AutoSize = true,
            Font = new Font("Segoe UI", 10f), Location = new Point(42, 72)
        });

        var btnIngresar = EstiloUI.Boton("INGRESAR", EstiloUI.Azul, 40, 335, 300, 42);
        var lnkRegistro = new LinkLabel { Text = "¿No tienes cuenta? Regístrate", Location = new Point(40, 395), AutoSize = true };

        btnIngresar.Click += (s, e) => _presenter.IniciarSesion();
        lnkRegistro.LinkClicked += (s, e) => { using var f = new FrmRegistro(_auth); f.ShowDialog(this); };
        AcceptButton = btnIngresar;

        Controls.AddRange(new Control[]
        {
            header,
            EstiloUI.Etiqueta("Correo", 40, 145), txtCorreo,
            EstiloUI.Etiqueta("Contraseña", 40, 215), txtPassword,
            lblError, btnIngresar, lnkRegistro
        });
    }

    public void MostrarError(string mensaje) => lblError.Text = mensaje;

    public void CerrarConExito()
    {
        DialogResult = DialogResult.OK;
        Close();
    }
}
