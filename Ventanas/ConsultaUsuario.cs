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
using osu.Framework.Lists;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using ATMPlus.Helpers;
using osu.Framework.Bindables;

namespace ATMPlus.Ventanas
{
    public class ConsultaUsuario : VentanaConTitulo
    {
        private CuentaCliente cuenta;
        private readonly FillFlowContainer<HistorialVisual> flujoHistorial;
        private readonly Dropdown<YearMonthTuple> seleccionMes;
        private readonly SortedList<IHistorial> entradas = new SortedList<IHistorial>();

        public ConsultaUsuario(CuentaCliente cuenta)
        {
            this.cuenta = cuenta;

            Add(new BasicScrollContainer<Drawable>
            {
                Padding = new MarginPadding() { Top = 110 },
                RelativeSizeAxes = Axes.Both,
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
            Add(new Box
            {
                Colour = Color4.Black,
                RelativeSizeAxes = Axes.X,
                Height = 110
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
            Add(new SpriteText
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.BottomRight,
                Position = new Vector2(-300, 110),
                Font = new FontUsage(size: 30),
                Text = "Mes:",
            });
            Add(seleccionMes = new BasicDropdown<YearMonthTuple>
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopLeft,
                Width = 200,
            });

            seleccionMes.Current.ValueChanged += RecargarElementos;

            ColorFondoTitulo = Color4.MediumSlateBlue;
            Titulo = "Tu cuenta";
        }

        private void RecargarElementos(ValueChangedEvent<YearMonthTuple> seleccion)
        {
            flujoHistorial.Clear();

            foreach (var ent in entradas.Where(e => e.FechaHora.Year == seleccion.NewValue.Year && e.FechaHora.Month == seleccion.NewValue.Month))
                flujoHistorial.Add(new HistorialVisual(cuenta, ent));
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            using (var bd = new DatabaseStore())
            {
                foreach (var h in bd.ObtenerHistorial(cuenta).GroupBy(ent => new { ent.FechaHora.Month, ent.FechaHora.Year }))
                {
                    seleccionMes.AddDropdownItem(new YearMonthTuple(h.Key.Year, h.Key.Month));

                    foreach (var g in h)
                        entradas.Add(g);
                }
            }

            seleccionMes.Current.Value = seleccionMes.Items.First();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            seleccionMes.Position = new Vector2(-290, 110 - seleccionMes.Height);
        }
    }
}
