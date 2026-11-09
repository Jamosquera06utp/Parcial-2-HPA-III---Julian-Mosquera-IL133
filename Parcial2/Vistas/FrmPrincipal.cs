using LabMVP.Negocio;
using LabMVP.Presentadores;

namespace LabMVP.Vistas;

public class FrmPrincipal : Form, IPrincipalView
{
    private readonly PrincipalPresenter _presenter;
    private readonly IAutenticacionService _auth;

    private readonly Panel pnlMenu = new();
    private readonly Panel pnlSuperior = new();
    private readonly Panel pnlUsuario = new();
    private readonly Label lblLogo = new();
    private readonly Label lblHora = new();
    private readonly Label lblFecha = new();
    private readonly Label lblNombre = new();
    private readonly Label lblCorreo = new();
    private readonly System.Windows.Forms.Timer tmrReloj = new() { Interval = 1000 };
    private readonly List<Button> botones = new();
    private bool menuExpandido = true;

    public bool CerrarSesionSolicitado { get; private set; }

    public FrmPrincipal(IAutenticacionService auth)
    {
        _auth = auth;
        _presenter = new PrincipalPresenter(this);

        Text = "LabMVP - Sistema";
        Size = new Size(1100, 680);
        MinimumSize = new Size(800, 500);
        StartPosition = FormStartPosition.CenterScreen;
        IsMdiContainer = true;   // MDI

        foreach (Control c in Controls)
            if (c is MdiClient mdi) mdi.BackColor = Color.White;

        ConstruirBarraSuperior();
        ConstruirMenu();
        // Se agrega el menú de último para que acople primero (ocupa toda la altura a la izquierda)
        Controls.Add(pnlSuperior);
        Controls.Add(pnlMenu);

        tmrReloj.Tick += (s, e) => _presenter.ActualizarHora();
        Load += (s, e) =>
        {
            _presenter.CargarSesion();
            _presenter.ActualizarHora();
            tmrReloj.Start();
            AbrirHijo(new FrmInicio());
        };
        FormClosed += (s, e) => tmrReloj.Stop();
    }

    // ---------- Construcción de la interfaz ----------
    private void ConstruirBarraSuperior()
    {
        pnlSuperior.Dock = DockStyle.Top;
        pnlSuperior.Height = 90;
        pnlSuperior.BackColor = Color.White;

        var btnHamburguesa = new Button
        {
            Text = "☰", Location = new Point(10, 20), Size = new Size(48, 48),
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 18f), Cursor = Cursors.Hand
        };
        btnHamburguesa.FlatAppearance.BorderSize = 0;
        btnHamburguesa.Click += (s, e) => AlternarMenu();

        var pnlReloj = new Panel { Dock = DockStyle.Right, Width = 380 };
        lblFecha.Dock = DockStyle.Fill;
        lblFecha.TextAlign = ContentAlignment.TopCenter;
        lblFecha.Font = new Font("Segoe UI", 12f);
        lblFecha.ForeColor = EstiloUI.Azul;

        lblHora.Dock = DockStyle.Top;
        lblHora.Height = 56;
        lblHora.TextAlign = ContentAlignment.MiddleCenter;
        lblHora.Font = new Font("Segoe UI Light", 32f);
        lblHora.ForeColor = Color.FromArgb(60, 60, 60);

        pnlReloj.Controls.Add(lblFecha);   // Fill primero
        pnlReloj.Controls.Add(lblHora);

        pnlSuperior.Controls.Add(btnHamburguesa);
        pnlSuperior.Controls.Add(pnlReloj);
    }

    private void ConstruirMenu()
    {
        pnlMenu.Dock = DockStyle.Left;
        pnlMenu.Width = 230;
        pnlMenu.BackColor = EstiloUI.Azul;

        lblLogo.Text = "LabMVP";
        lblLogo.ForeColor = Color.White;
        lblLogo.Font = new Font("Segoe UI", 20f, FontStyle.Bold);
        lblLogo.AutoSize = true;
        lblLogo.Location = new Point(20, 22);
        pnlMenu.Controls.Add(lblLogo);

        var items = new (string texto, Action accion)[]
        {
            ("🏠  Inicio",            () => AbrirHijo(new FrmInicio())),
            ("🎲  Juego de Craps",    () => AbrirHijo(new FrmCraps())),
            ("👤  Registrar usuario", () => { using var f = new FrmRegistro(_auth); f.ShowDialog(this); }),
            ("🚪  Cerrar sesión",     CerrarSesion),
        };

        int y = 100;
        foreach (var (texto, accion) in items)
        {
            var b = new Button
            {
                Text = texto, Tag = texto, Location = new Point(0, y), Size = new Size(230, 48),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat, ForeColor = Color.White, BackColor = EstiloUI.Azul,
                TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(15, 0, 0, 0),
                Font = new Font("Segoe UI", 11f), Cursor = Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = EstiloUI.AzulOscuro;
            b.Click += (s, e) => accion();
            botones.Add(b);
            pnlMenu.Controls.Add(b);
            y += 50;
        }

        // Sesión del usuario en la parte INFERIOR del menú
        pnlUsuario.Dock = DockStyle.Bottom;
        pnlUsuario.Height = 90;
        pnlUsuario.BackColor = EstiloUI.AzulOscuro;

        var lblAvatar = new Label
        {
            Text = "👤", ForeColor = Color.White, Font = new Font("Segoe UI Emoji", 26f),
            AutoSize = true, Location = new Point(8, 20)
        };
        lblNombre.ForeColor = Color.White;
        lblNombre.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
        lblNombre.Location = new Point(72, 24);
        lblNombre.Size = new Size(150, 22);
        lblNombre.AutoEllipsis = true;
        lblCorreo.ForeColor = Color.White;
        lblCorreo.Font = new Font("Segoe UI", 8.5f);
        lblCorreo.Location = new Point(72, 48);
        lblCorreo.Size = new Size(150, 20);
        lblCorreo.AutoEllipsis = true;

        pnlUsuario.Controls.AddRange(new Control[] { lblAvatar, lblNombre, lblCorreo });
        pnlMenu.Controls.Add(pnlUsuario);
    }

    // ---------- Comportamiento de la vista ----------
    private void AlternarMenu()
    {
        menuExpandido = !menuExpandido;
        pnlMenu.Width = menuExpandido ? 230 : 64;
        foreach (var b in botones)
        {
            var completo = (string)b.Tag;
            b.Text = menuExpandido ? completo : completo.Split(' ')[0];
            b.TextAlign = menuExpandido ? ContentAlignment.MiddleLeft : ContentAlignment.MiddleCenter;
            b.Padding = menuExpandido ? new Padding(15, 0, 0, 0) : Padding.Empty;
        }
        lblLogo.Visible = lblNombre.Visible = lblCorreo.Visible = menuExpandido;
    }

    private void AbrirHijo(Form hijo)
    {
        foreach (var h in MdiChildren) h.Close();
        hijo.MdiParent = this;
        hijo.FormBorderStyle = FormBorderStyle.None;
        hijo.Dock = DockStyle.Fill;
        hijo.Show();
    }

    private void CerrarSesion()
    {
        var r = MessageBox.Show("¿Seguro que deseas cerrar sesión?", "Cerrar sesión",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (r != DialogResult.Yes) return;

        LabMVP.Entidades.SesionUsuario.Actual = null;
        CerrarSesionSolicitado = true;
        Close();
    }

    // ---------- IPrincipalView ----------
    public void MostrarHora(string hora, string fecha)
    {
        lblHora.Text = hora;
        lblFecha.Text = fecha;
    }

    public void MostrarUsuario(string nombre, string correo)
    {
        lblNombre.Text = nombre;
        lblCorreo.Text = correo;
    }
}
