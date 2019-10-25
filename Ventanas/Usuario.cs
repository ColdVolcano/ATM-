using ATMPlus.Database;
using osuTK.Graphics;
using osu.Framework.Screens;
using ATMPlus.Elementos;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;

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
                    Action = () => LoadComponentAsync(new ConsultaUsuario(cuenta), this.Push),
                },
                new BotonOpcion
                {
                    Icono = FontAwesome.Solid.MoneyBill,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = FrameworkColour.Blue,
                    Text = "Retiro",
                    Action = () => LoadComponentAsync(new RetiroUsuario(cuenta), this.Push),
                },
                new BotonOpcion
                {
                    Icono = FontAwesome.Solid.Save,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = FrameworkColour.Blue,
                    Text = "Deposito",
                    Action = () => MostrarAlerta("Hola xd")
                },
                new BotonOpcion
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BackgroundColour = Color4.OrangeRed,
                    Icono = FontAwesome.Solid.SignOutAlt,
                    Text = "Salir",
                    Action = Salir,
                }
        });

            Titulo = $"Nos alegra tenerte aquí, {cuenta.Nombre.PrimerNombre}";
            ColorFondoTitulo = Color4.MediumSlateBlue;
        }

        private void Salir()
        {
            MostrarAlerta("Gracias por usar el cajero automatico", true, true);
            using (var db = new DatabaseStore())
            {
                db.CerrarSesion(cuenta);
            }
            this.Exit();
        }
    }
}
