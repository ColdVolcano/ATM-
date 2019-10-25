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
using System;
using osu.Framework.Lists;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using ATMPlus.Helpers;
using osu.Framework.Bindables;

namespace ATMPlus.Ventanas
{
    public class RetiroUsuario : VentanaConTitulo
    {
        private CuentaCliente cuenta;

        private BotonHover submit20;
        private BotonHover submit40;
        private BotonHover submit60;
        private BotonHover submit100;
        private BotonHover submit200;

        public RetiroUsuario(CuentaCliente cuenta)
        {
            this.cuenta = cuenta;

            Add(submit20 = new BotonHover
            {
                Size = new Vector2(250, 30),
                BackgroundColour = new Color4(90, 120, 198, 255),
                Text = "$20",
                Anchor = Anchor.Centre,
                Origin = Anchor.CentreRight,
                Position = new Vector2(-5, -35),
                Action = () => retirar(20)
            });
            Add(submit40 = new BotonHover
            {
                Size = new Vector2(250, 30),
                BackgroundColour = new Color4(90, 120, 198, 255),
                Text = "$40",
                Anchor = Anchor.Centre,
                Origin = Anchor.CentreLeft,
                Position = new Vector2(5, -35),
                Action = () => retirar(40)
            });
            Add(submit60 = new BotonHover
            {
                Size = new Vector2(250, 30),
                BackgroundColour = new Color4(90, 120, 198, 255),
                Text = "$60",
                Anchor = Anchor.Centre,
                Origin = Anchor.CentreRight,
                Action = () => retirar(60),
                X = -5
            });
            Add(submit100 = new BotonHover
            {
                Size = new Vector2(250, 30),
                BackgroundColour = new Color4(90, 120, 198, 255),
                Text = "$100",
                Anchor = Anchor.Centre,
                Origin = Anchor.CentreLeft,
                Action = () => retirar(100),
                X = 5
            });
            Add(submit200 = new BotonHover
            {
                Size = new Vector2(250, 30),
                BackgroundColour = new Color4(90, 120, 198, 255),
                Text = "$200",
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Action = () => retirar(200),
                Y = 35
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
            ColorFondoTitulo = Color4.MediumSlateBlue;
            Titulo = "Realiza un retiro";
        }

        public override void OnResuming(IScreen last)
        {
            this.Exit();
        }

        private void retirar(double cantidad)
        {
            using (var db = new DatabaseStore())
            {
                switch (db.RetiroUsuario(cuenta, cantidad))
                {
                    case ResultadoOperacion.Correcto:
                        MostrarAlerta("Por favor tome su dinero al presionar una tecla\nRetiro correcto", true, false);
                        break;
                    case ResultadoOperacion.NoDinero:
                        MostrarAlerta("El cajero no tiene suficientes billetes para realizar su operación\nIntente otra cantidad", false, true);
                        break;
                    case ResultadoOperacion.NoSaldo:
                        MostrarAlerta("Usted no tiene suficiente saldo para realizar esta operación\nIntente una cantidad menor", false, true);
                        break;
                }
            }
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            ColorFondoTitulo = Color4.MediumSlateBlue;
            Titulo = "Realiza un retiro";
        }

    }
}
