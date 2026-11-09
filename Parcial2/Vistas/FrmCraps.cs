using LabMVP.Negocio;
using LabMVP.Presentadores;

namespace LabMVP.Vistas;

public class FrmCraps : Form, ICrapsView
{
    private readonly CrapsPresenter _presenter;

    private readonly Panel pnl = new() { Size = new Size(520, 440) };
    private readonly Label lblDado1 = CrearDado(110);
    private readonly Label lblDado2 = CrearDado(290);
    private readonly Label lblSuma = new();
    private readonly Label lblPunto = new();
    private readonly Label lblMensaje = new();
    private readonly Label lblMarcador = new();

    public FrmCraps()
    {
        _presenter = new CrapsPresenter(this, new JuegoCrapsService());
        BackColor = EstiloUI.Fondo;

        var lblTitulo = Centrado("Juego de Craps", 0, 40, new Font("Segoe UI", 22f, FontStyle.Bold), EstiloUI.Azul);
        var lblReglas = Centrado("Primer tiro: 7 u 11 ganas · 2, 3 o 12 pierdes · otro número = tu punto", 42, 24,
            new Font("Segoe UI", 9.5f), Color.DimGray);

        lblSuma.SetBounds(0, 205, 520, 28);
        lblSuma.TextAlign = ContentAlignment.MiddleCenter;
        lblSuma.Font = new Font("Segoe UI", 13f);
        lblPunto.SetBounds(0, 235, 520, 28);
        lblPunto.TextAlign = ContentAlignment.MiddleCenter;
        lblPunto.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
        lblMensaje.SetBounds(20, 270, 480, 56);
        lblMensaje.TextAlign = ContentAlignment.MiddleCenter;
        lblMensaje.Font = new Font("Segoe UI", 13f, FontStyle.Bold);

        var btnLanzar = EstiloUI.Boton("🎲  LANZAR DADOS", EstiloUI.Azul, 160, 340, 200, 48);
        btnLanzar.Click += (s, e) => _presenter.Lanzar();

        lblMarcador.SetBounds(0, 398, 520, 28);
        lblMarcador.TextAlign = ContentAlignment.MiddleCenter;
        lblMarcador.Font = new Font("Segoe UI", 10.5f);
        lblMarcador.Text = "Ganadas: 0   |   Perdidas: 0";

        pnl.Controls.AddRange(new Control[]
        {
            lblTitulo, lblReglas, lblDado1, lblDado2, lblSuma, lblPunto, lblMensaje, btnLanzar, lblMarcador
        });
        Controls.Add(pnl);

        Resize += (s, e) => Centrar();
        Load += (s, e) => Centrar();
    }

    private void Centrar() =>
        pnl.Location = new Point(Math.Max(0, (ClientSize.Width - pnl.Width) / 2),
                                 Math.Max(0, (ClientSize.Height - pnl.Height) / 2));

    private static Label CrearDado(int x) => new()
    {
        Location = new Point(x, 85), Size = new Size(120, 110),
        TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI Symbol", 64f),
        BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Text = "?"
    };

    private static Label Centrado(string texto, int y, int alto, Font fuente, Color color) => new()
    {
        Text = texto, Font = fuente, ForeColor = color, TextAlign = ContentAlignment.MiddleCenter,
        Bounds = new Rectangle(0, y, 520, alto)
    };

    // ---------- ICrapsView ----------
    public void MostrarDados(int d1, int d2)
    {
        lblDado1.Text = ((char)(0x2680 + d1 - 1)).ToString();
        lblDado2.Text = ((char)(0x2680 + d2 - 1)).ToString();
        lblSuma.Text = $"{d1} + {d2} = {d1 + d2}";
    }

    public void MostrarPunto(int? punto) =>
        lblPunto.Text = punto.HasValue ? $"Punto: {punto}" : "";

    public void MostrarMensaje(string texto, bool? gano)
    {
        lblMensaje.Text = texto;
        lblMensaje.ForeColor = gano == true ? Color.SeaGreen : gano == false ? Color.Firebrick : Color.DimGray;
    }

    public void MostrarMarcador(int ganadas, int perdidas) =>
        lblMarcador.Text = $"Ganadas: {ganadas}   |   Perdidas: {perdidas}";
}
