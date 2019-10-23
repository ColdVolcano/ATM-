using ATMPlus.Database;
using osuTK.Graphics;
using osu.Framework.Screens;
using ATMPlus.Elementos;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Allocation;

namespace ATMPlus.Ventanas
{
    public class Usuario : VentanaConOpciones
    {
        private readonly CuentaCliente cuenta;
        public Usuario(CuentaCliente cuenta)
        {
            this.cuenta = cuenta;
            FlujoBotones.AddRange(new BotonOpcion[]
            {
                new BotonOpcion
                {
                    Icono = FontAwesome.Solid.Search,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = FrameworkColour.Blue,
                    Text = "Consulta",
                    Action = this.Exit,
                },
                new BotonOpcion
                {
                    Icono = FontAwesome.Solid.MoneyBill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = FrameworkColour.Blue,
                    Text = "Retiro",
                    Action = this.Exit,
                },
                new BotonOpcion
                {
                    Icono = FontAwesome.Solid.Save,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = FrameworkColour.Blue,
                    Text = "Deposito",
                    Action = this.Exit,
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {

            Titulo = $"Nos alegra tenerte aquí, {cuenta.Nombre.PrimerNombre}";
            ColorFondoTitulo = Color4.MediumSlateBlue;
        }
    }
}
