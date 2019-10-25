using ATMPlus.Database;
using ATMPlus.Elementos;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace ATMPlus.Ventanas
{
    public class DepositoUsuario : VentanaConOpciones
    {
        private readonly CuentaCliente cuenta;
        public DepositoUsuario(CuentaCliente cuenta)
        {
            Titulo = "Realiza un deposito";
            ColorFondoTitulo = Color4.MediumSlateBlue;
            this.cuenta = cuenta;
            FlujoBotones.AddRange(new BotonOpcion[]
            {
                new BotonOpcion
                {
                    Icono = FontAwesome.Solid.User,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = FrameworkColour.Blue,
                    Text = "Propia cuenta",
                    Action = () => LoadComponentAsync(new DepositoPropioUsuario(cuenta), this.Push),
                },
                new BotonOpcion
                {
                    Icono = FontAwesome.Solid.Users,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = FrameworkColour.Blue,
                    Text = "Otra cuenta",
                    Action = () => LoadComponentAsync(new DepositoCuentaUsuario(cuenta), this.Push),
                },
                new BotonOpcion
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = Color4.OrangeRed,
                    Icono = FontAwesome.Solid.ArrowCircleLeft,
                    Text = "Regresar",
                    Action = this.Exit,
                }
            });
        }
    }
}
