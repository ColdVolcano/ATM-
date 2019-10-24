using ATMPlus.Database;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace ATMPlus.Elementos
{
    public class HistorialVisual : Container
    {
        protected IHistorial Entrada;
        private readonly SpriteText textoInforme;
        public HistorialVisual(IHistorial entrada)
        {
            RelativeSizeAxes = Axes.X;
            Height = 110;
            Anchor = Anchor.TopRight;
            Origin = Anchor.TopRight;
            Children = new Drawable[]
            {
                new SpriteIcon
                {
                    Size = new Vector2(80),
                    Margin = new MarginPadding(10),
                    Icon = (entrada is HistorialRetiro) ? FontAwesome.Solid.SignOutAlt : FontAwesome.Solid.SignInAlt,
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    Colour = new Color4(40, 40, 40, 255),
                },
                textoInforme = new SpriteText
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Font = new FontUsage(size: 30),
                },
                new SpriteText
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Font = new FontUsage(size: 20),
                    Text = $"{entrada.FechaHora:MM/dd/yyyy HH:mm}"
                }
            };

            switch (entrada)
            {
                case HistorialRetiro retiro:
                    textoInforme.Text = $"Retiro de efectivo por ${retiro.Cantidad}";
                    break;
                case HistorialDeposito deposito:
                    textoInforme.Text = $"Deposito {(deposito.CuentaOrigen == deposito.CuentaDestino ? "a cuenta propia" : $"a cuenta {"····" + deposito.CuentaDestino % 10:00}")} por ${deposito.Cantidad:0.##}";
                    break;
            }
        }
    }
}
