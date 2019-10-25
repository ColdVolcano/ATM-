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
        public HistorialVisual(int cuentaActual, IHistorial entrada, bool mostrarCuenta = false)
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
                    Alpha = 0.75f,
                    Text = $"{entrada.FechaHora:MM/dd/yyyy HH:mm}"
                },
                new SpriteText
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Font = new FontUsage(size: 20),
                    Alpha = 0.75f,
                    Text = $"{(mostrarCuenta ? $"{entrada.CuentaOrigen}" : "")}"
                },
            };

            switch (entrada)
            {
                case HistorialRetiro retiro:
                    textoInforme.Text = $"Retiro de efectivo por ${retiro.Cantidad:0,0.##}";
                    break;
                case HistorialDeposito deposito:
                    textoInforme.Text = $"Deposito {(cuentaActual == deposito.CuentaDestino ? "" : $"a cuenta {(mostrarCuenta ? "" + deposito.CuentaDestino/100 :  "····") + deposito.CuentaDestino % 10:00} ")}por ${deposito.Cantidad:0,0.##}";
                    break;
            }
        }
    }
}
