namespace LabMVP.Vistas;

public static class EstiloUI
{
    public static readonly Color Azul = Color.FromArgb(0, 112, 192);
    public static readonly Color AzulOscuro = Color.FromArgb(0, 88, 158);
    public static readonly Color Fondo = Color.FromArgb(245, 247, 250);

    public static Button Boton(string texto, Color fondo, int x, int y, int w, int h)
    {
        var b = new Button
        {
            Text = texto, BackColor = fondo, ForeColor = Color.White, FlatStyle = FlatStyle.Flat,
            Location = new Point(x, y), Size = new Size(w, h),
            Font = new Font("Segoe UI", 10f, FontStyle.Bold), Cursor = Cursors.Hand
        };
        b.FlatAppearance.BorderSize = 0;
        return b;
    }

    public static Label Etiqueta(string texto, int x, int y) =>
        new Label { Text = texto, Location = new Point(x, y), AutoSize = true, Font = new Font("Segoe UI", 9.5f) };

    public static TextBox Caja(int x, int y, int w, bool password = false) =>
        new TextBox { Location = new Point(x, y), Width = w, Font = new Font("Segoe UI", 11f), UseSystemPasswordChar = password };
}
