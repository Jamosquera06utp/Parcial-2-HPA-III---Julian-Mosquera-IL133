using LabMVP.Negocio;
using LabMVP.Presentadores;

namespace LabMVP.Vistas;

public class FrmRegistro : Form, IRegistroView
{
    private readonly RegistroPresenter _presenter;

    private readonly TextBox txtNombre = EstiloUI.Caja(40, 135, 300);
    private readonly TextBox txtCorreo = EstiloUI.Caja(40, 205, 300);
    private readonly TextBox txtPassword = EstiloUI.Caja(40, 275, 300, true);
    private readonly TextBox txtConfirmacion = EstiloUI.Caja(40, 345, 300, true);
    private readonly Label lblMensaje = new()
    {
        Location = new Point(40, 385),
        Size = new Size(300, 44),
        Font = new Font("Segoe UI", 9f),
        ForeColor = Color.Firebrick
    };

    public string Nombre => txtNombre.Text;
    public string Correo => txtCorreo.Text.Trim();
    public string Password => txtPassword.Text;
    public string Confirmacion => txtConfirmacion.Text;

    public FrmRegistro(IAutenticacionService auth)
    {
        _presenter = new RegistroPresenter(this, auth);

        Text = "Registrar usuario";
        ClientSize = new Size(380, 500);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        BackColor = Color.White;

        var header = new Panel { Dock = DockStyle.Top, Height = 90, BackColor = EstiloUI.Azul };
        header.Controls.Add(new Label
        {
            Text = "Crear cuenta",
            ForeColor = Color.White,
            AutoSize = true,
            Font = new Font("Segoe UI", 20f, FontStyle.Bold),
            Location = new Point(36, 24)
        });

        var btnRegistrar = EstiloUI.Boton("REGISTRAR", EstiloUI.Azul, 40, 440, 300, 42);
        btnRegistrar.Click += (s, e) => _presenter.Registrar();

        Controls.AddRange(new Control[]
        {
            header,
            EstiloUI.Etiqueta("Nombre completo", 40, 110), txtNombre,
            EstiloUI.Etiqueta("Correo", 40, 180), txtCorreo,
            EstiloUI.Etiqueta("Contraseña (mín. 8, 1 mayúscula, 1 número)", 40, 250), txtPassword,
            EstiloUI.Etiqueta("Confirmar contraseña", 40, 320), txtConfirmacion,
            lblMensaje, btnRegistrar
        });
    }

    public void MostrarError(string mensaje)
    {
        lblMensaje.ForeColor = Color.Firebrick;
        lblMensaje.Text = mensaje;
    }

    private void InitializeComponent()
    {

    }

    public void MostrarExito(string mensaje)
    {
        MessageBox.Show(mensaje, "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        DialogResult = DialogResult.OK;
        Close();
    }
}
