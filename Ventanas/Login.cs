using ATMPlus.Database;
using ATMPlus.Elementos;
using ATMPlus.Helpers;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using System.Threading.Tasks;

namespace ATMPlus.Ventanas
{
    public class Login : VentanaConTitulo
    {
        private SpriteText textUser;
        private NumberBox textbox;
        private PasswordTextBox passbox;
        private BotonHover submit;

        private ICuenta cuentaLogin
        {
            set
            {
                if (value == null)
                {
                    textbox.Text = "";
                    passbox.Text = "";
                    textUser
                        .FadeIn()
                        .Delay(1500)
                        .FadeOut(1000, Easing.InCubic);
                }
                else
                {
                    switch (value)
                    {
                        case CuentaCliente cuentaCliente:   //Crear usuario
                            LoadComponentAsync(new Usuario(cuentaCliente), this.Push);
                            break;
                        case CuentaGerente cuentaGerente:   //Crear gerente
                            break;
                    }
                }
            }
        }

        private readonly Color4 enabledColor = new Color4(90, 120, 198, 255);
        private readonly Color4 disabledColor = new Color4(69, 84, 112, 255);

        [BackgroundDependencyLoader]
        private void load()
        {
            Titulo = "Ingresa con tu cuenta";
            Add(textbox = new NumberBox
            {
                PlaceholderText = "Numero de cuenta",
                Height = 30,
                Width = 250,
                Anchor = Anchor.Centre,
                Origin = Anchor.BottomCentre,
                LengthLimit = 6,
                Y = -30 / 2f * 3 - 10,
            });
            Add(passbox = new PasswordTextBox
            {
                PlaceholderText = "NIP/Contraseña",
                Height = 30,
                Width = 250,
                Anchor = Anchor.Centre,
                Origin = Anchor.BottomCentre,
                Y = -30 / 2f - 5
            });
            Add(submit = new BotonHover
            {
                Size = new Vector2(250, 30),
                BackgroundColour = disabledColor,
                Text = "Entrar",
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
            Add(textUser = new SpriteText
            {
                Text = "Cuenta o PIN incorrectos",
                Font = new FontUsage(size: 30),
                Anchor = Anchor.Centre,
                Origin = Anchor.TopCentre,
                Y = 30 / 2f * 3 + 10,
                Colour = Color4.OrangeRed,
                Alpha = 0f,
            });
            submit.Action += () => Task.Run(obtenerCuenta);
            submit.Enabled.Value = false;

            passbox.Current.ValueChanged += _ => checkButton();
            textbox.Current.ValueChanged += _ => checkButton();
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            textbox.Text = "";
            passbox.Text = "";
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);

            textbox.Text = "";
            passbox.Text = "";
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);

            textUser.FadeOut(500, Easing.OutQuint);
        }

        private void checkButton()
        {
            submit.Enabled.Value = !string.IsNullOrEmpty(textbox.Text) && !string.IsNullOrEmpty(passbox.Text) && textbox.Text.Length == 6;
            submit.BackgroundColour = submit.Enabled.Value ? enabledColor : disabledColor;
        }

        private void obtenerCuenta()
        {
            using (var db = new DatabaseStore())
            {
                cuentaLogin = db.InicioSesion(int.Parse(textbox.Text), Hasher.EncryptSHA1(passbox.Text));
            }
        }
    }
}
