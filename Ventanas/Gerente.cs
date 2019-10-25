using ATMPlus.Database;
using osuTK.Graphics;
using osu.Framework.Screens;
using ATMPlus.Elementos;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.UserInterface;
using ATMPlus.Helpers;
using osu.Framework.Lists;
using osu.Framework.Bindables;
using osuTK;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Allocation;
using System.Linq;

namespace ATMPlus.Ventanas
{
    public class Gerente : VentanaConTitulo
    {
        private CuentaGerente cuenta;
        private readonly SpriteText textoMes;
        private readonly SpriteText totalRetiros;
        private readonly SpriteText totalDepositos;
        private readonly SpriteText totalCajero;
        private readonly SpriteText noElementos;
        private readonly FillFlowContainer<HistorialVisual> flujoHistorial;
        private readonly Dropdown<DummyAccountListing> seleccionCuenta;
        private readonly Dropdown<YearMonthTuple> seleccionMes;
        private readonly SortedList<IHistorial> entradas = new SortedList<IHistorial>();

        public Gerente(CuentaGerente cuenta)
        {
            this.cuenta = cuenta;

            Add(new BasicScrollContainer<Drawable>
            {
                Padding = new MarginPadding() { Top = 110 },
                RelativeSizeAxes = Axes.Both,
                Width = 0.45f,
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
            Add(textoMes = new SpriteText
            {
                Font = new FontUsage(size: 60),
                Position = new Vector2(20, 50),
            });
            Add(totalRetiros = new SpriteText
            {
                Font = new FontUsage(size: 30),
                Position = new Vector2(20, 120),
            });
            Add(totalDepositos = new SpriteText
            {
                Font = new FontUsage(size: 30),
                Position = new Vector2(20, 160),
            });
            Add(totalCajero = new SpriteText
            {
                Text = $"Billetes en el cajero: {DatabaseStore.BilletesRestantes}",
                Font = new FontUsage(size: 30),
                Position = new Vector2(20, 200),
            });
            Add(new SpriteText
            {
                Text = "Movimientos del mes:",
                Font = new FontUsage(size: 60),
                RelativePositionAxes = Axes.X,
                Position = new Vector2(0.55f, 50),
            });
            Add(noElementos = new SpriteText
            {
                Text = "No hay movimientos",
                Font = new FontUsage(size: 30),
                RelativePositionAxes = Axes.Both,
                Anchor = Anchor.BottomRight,
                Origin = Anchor.Centre,
                Position = new Vector2(-0.225f, -0.5f),
                Alpha = 0,
            });
            Add(new SpriteText
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.BottomRight,
                Position = new Vector2(-300, 110),
                Font = new FontUsage(size: 30),
                Text = "Mes:",
            });
            Add(new SpriteText
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.BottomRight,
                Position = new Vector2(-300, 70),
                Font = new FontUsage(size: 30),
                Text = "Cuenta:",
            });
            Add(seleccionMes = new BasicDropdown<YearMonthTuple>
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopLeft,
                Width = 200,
            });
            Add(seleccionCuenta = new BasicDropdown<DummyAccountListing>
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopLeft,
                Width = 200,
            });

            seleccionMes.Current.ValueChanged += _ => RecargarElementos();
            seleccionCuenta.Current.ValueChanged += _ => RecargarElementos();

            ColorFondoTitulo = FrameworkColour.GreenDark;
            Titulo = $"Vista de gerente para {cuenta.Nombre.PrimerNombre}";
        }

        private void RecargarElementos()
        {
            if (seleccionCuenta.Current.Value != null && seleccionMes.Current.Value != null)
            {
                int retiros = 0, depositos = 0;
                double montoRetiros = 0, montoDepositos = 0;
                flujoHistorial.Clear();
                textoMes.Text = $"Estadisticas de {seleccionMes.Current.Value.ToString()} para {(seleccionCuenta.Current.Value.Account == 0 ? "todas las cuentas" : $"la cuenta {seleccionCuenta.Current.Value}")}";
                var ents = entradas
                    .Where(e => e.FechaHora.Year == seleccionMes.Current.Value.Year && e.FechaHora.Month == seleccionMes.Current.Value.Month)
                    .Where(f => seleccionCuenta.Current.Value.Account == 0 ? true : 
                    seleccionCuenta.Current.Value.Account == f.CuentaOrigen || 
                    f is HistorialDeposito && seleccionCuenta.Current.Value.Account == ((HistorialDeposito)f).CuentaDestino);
                foreach (var ent in ents)
                {
                    flujoHistorial.Add(new HistorialVisual(ent.CuentaOrigen, ent, true));
                    switch (ent)
                    {
                        case HistorialDeposito dep:
                            depositos++;
                            montoDepositos += dep.Cantidad;
                            break;
                        case HistorialRetiro ret:
                            montoRetiros += ret.Cantidad;
                            retiros++;
                            break;
                    }
                }

                totalDepositos.Text = $"Depositos: {depositos} {(depositos == 0 ? "" : $"(${montoDepositos:0,0.##})")}";
                totalRetiros.Text = $"Retiros: {retiros} {(retiros == 0 ? "" : $"(${montoRetiros:0,0.##})")}";
                if (ents.Count() == 0)
                    noElementos.Show();
                else
                    noElementos.Hide();
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            seleccionCuenta.AddDropdownItem(new DummyAccountListing(0));
            using (var bd = new DatabaseStore())
            {
                foreach (var h in bd.ObtenerHistorial(cuenta).GroupBy(ent => new { ent.FechaHora.Month, ent.FechaHora.Year }))
                {
                    seleccionMes.AddDropdownItem(new YearMonthTuple(h.Key.Year, h.Key.Month));

                    foreach (var g in h)
                        entradas.Add(g);
                }

                foreach (var acc in bd.ObtenerCuentas(cuenta))
                {
                    seleccionCuenta.AddDropdownItem(new DummyAccountListing(acc));
                }
            }

            seleccionCuenta.Current.Value = seleccionCuenta.Items.First();
            seleccionMes.Current.ValueChanged += _ => RecargarElementos();
            seleccionCuenta.Current.ValueChanged += _ => RecargarElementos();
            seleccionMes.Current.Value = seleccionMes.Items.First();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            seleccionMes.Position = new Vector2(-290, 110 - seleccionMes.Height);
            seleccionCuenta.Position = new Vector2(-290, 70 - seleccionCuenta.Height);
        }
    }
}
