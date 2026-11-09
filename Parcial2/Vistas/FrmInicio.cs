using LabMVP.Entidades;

namespace LabMVP.Vistas;

public class FrmInicio : Form
{
    public FrmInicio()
    {
        BackColor = Color.White;
        Controls.Add(new Label
        {
            Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 22f), ForeColor = EstiloUI.Azul,
            Text = $"Bienvenido, {SesionUsuario.Actual?.Nombre}\n\nUsa el menú de la izquierda."
        });
    }
}
