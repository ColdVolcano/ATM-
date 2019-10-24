using ATMPlus.Database;
using osuTK.Graphics;
using osu.Framework.Screens;
using ATMPlus.Elementos;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;
using System.Linq;
using osuTK;

namespace ATMPlus.Ventanas
{
    public class ConsultaUsuario : VentanaConTitulo
    {
        private CuentaCliente cuenta;
        private readonly FillFlowContainer<HistorialVisual> flujoHistorial;
        public ConsultaUsuario(CuentaCliente cuenta)
        {
            this.cuenta = cuenta;
            Add(new SpriteText
            {
                Text = "Tu saldo:",
                Font = new FontUsage(size: 60),
                Position = new Vector2(20, 50),
            });
            Add(new SpriteText
            {
                Text = $"${cuenta.Saldo:0.##}",
                Font = new FontUsage(size: 100),
                Position = new Vector2(20, 120),
            });
            Add(new SpriteText
            {
                Text = "Tus movimientos:",
                Font = new FontUsage(size: 60),
                RelativePositionAxes = Axes.X,
                Position = new Vector2(0.5f, 50),
            });
            Add(new BasicScrollContainer<Drawable>
            {
                Height = 600,
                RelativeSizeAxes = Axes.X,
                Width = 0.5f,
                Anchor = Anchor.BottomRight,
                Origin = Anchor.BottomRight,
                Child = flujoHistorial = new FillFlowContainer<HistorialVisual>
                {
                    Spacing = new Vector2(0, 10),
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Direction = FillDirection.Vertical,
                }
            });
            Add(new BotonOpcion
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft,
                BackgroundColour = Color4.OrangeRed,
                Icono = FontAwesome.Solid.ArrowCircleLeft,
                Text = "Regresar",
                Margin = new MarginPadding(20),
                Action = this.Exit,
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            using (var bd = new DatabaseStore())
            {
                foreach (var h in bd.ObtenerHistorial(cuenta.NumeroCuenta))
                {
                    flujoHistorial.Add(new HistorialVisual(h));
                }
            }

            ColorFondoTitulo = Color4.MediumSlateBlue;
            Titulo = "Tus cuenta";
        }
    }
}
