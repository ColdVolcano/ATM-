using ATMPlus.Database;
using ATMPlus.Elementos;
using osu.Framework.Graphics;
using osuTK.Graphics;
using osuTK;
using osu.Framework.Screens;
using ATMPlus.Helpers;
using osu.Framework.Graphics.Sprites;

namespace ATMPlus.Ventanas
{
    public class DepositoPropioUsuario : VentanaConTitulo
    {
        private CuentaCliente cuenta;
        private DecimalBox textbox;
        private BotonHover submit;
        private double tiempoDeposito;

        private PendingDeposit salida;

        private bool enableBut = true;

        private double buff;

        protected virtual int CuentaDestino => cuenta.NumeroCuenta;
        protected virtual bool BotonDisponible => !string.IsNullOrEmpty(textbox.Text) && double.TryParse(textbox.Text, out buff) && enableBut;

        private bool buttonEnabled
        {
            set
            {
                enableBut = value;
                CheckButton();
            }
        }

        private readonly Color4 enabledColor = new Color4(90, 120, 198, 255);
        private readonly Color4 disabledColor = new Color4(69, 84, 112, 255);
        public DepositoPropioUsuario(CuentaCliente cuenta)
        {
            ColorFondoTitulo = Color4.MediumSlateBlue;
            Titulo = "Configure su deposito";
            OnAlertHide = guardarRetiro;
            this.cuenta = cuenta;
            Add(textbox = new DecimalBox
            {
                PlaceholderText = "Cantidad",
                Height = 30,
                Width = 250,
                Anchor = Anchor.Centre,
                Origin = Anchor.BottomCentre,
                Y = -30 / 2f - 10,
            });
            Add(submit = new BotonHover
            {
                Size = new Vector2(250, 30),
                BackgroundColour = disabledColor,
                Text = "Entrar",
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
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

            submit.Action += revisarRetiro;
            submit.Enabled.Value = false;

            textbox.Current.ValueChanged += _ => CheckButton();
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            textbox.Text = "";
            buttonEnabled = true;
        }

        private void revisarRetiro()
        {
            buttonEnabled = false;
            using (var db = new DatabaseStore())
            {
                var res = db.DepositoUsuario(cuenta, double.Parse(textbox.Text), CuentaDestino, out salida);
                switch (res)
                {
                    case ResultadoOperacion.Correcto:
                        tiempoDeposito = Time.Current;
                        MostrarAlerta("Introduzca su efectivo usando cualquier tecla\nTiene 2 minutos para esta operación.", false, false);
                        Scheduler.AddDelayed(depositoFallido, 120000);
                        break;
                    case ResultadoOperacion.NoCuenta:
                        OnAlertHide = null;
                        MostrarAlerta("Esta cuenta no existe o no permite depositos", true, true);
                        break;
                    default:
                        break;
                }
            }
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);

            textbox.Text = "";
            buttonEnabled = true;
        }

        protected void CheckButton()
        {
            submit.Enabled.Value = BotonDisponible;
            submit.BackgroundColour = submit.Enabled.Value ? enabledColor : disabledColor;
        }

        private void guardarRetiro()
        {
            if (Time.Current - tiempoDeposito < 120000)
            {
                tiempoDeposito = -1;
                cuenta.DepositosPendientes.Add(salida);
                OnAlertHide = null;
                MostrarAlerta("Deposito correcto", true, true);
            }
        }

        private void depositoFallido()
        {
            if (tiempoDeposito != -1)
            {
                OnAlertHide = null;
                MostrarAlerta("No ingresaste el dinero a tiempo\nVolviendo al menú principal", true, true);
            }
        }
    }
}
