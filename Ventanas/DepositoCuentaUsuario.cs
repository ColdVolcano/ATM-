using ATMPlus.Database;
using ATMPlus.Elementos;
using osu.Framework.Graphics;

namespace ATMPlus.Ventanas
{
    public class DepositoCuentaUsuario : DepositoPropioUsuario
    {
        protected override int CuentaDestino => int.Parse(accbox.Text);
        protected override bool BotonDisponible => accbox.Text.Length == 6 && base.BotonDisponible;
        private NumberBox accbox;
        public DepositoCuentaUsuario(CuentaCliente cuenta) : base(cuenta)
        {
            Add(accbox = new NumberBox
            {
                PlaceholderText = "Numero de cuenta",
                Height = 30,
                Width = 250,
                Anchor = Anchor.Centre,
                Origin = Anchor.BottomCentre,
                LengthLimit = 6,
                Y = -30 / 2f * 3 - 10,
            });

            accbox.Current.ValueChanged += _ => CheckButton();
        }
    }
}
