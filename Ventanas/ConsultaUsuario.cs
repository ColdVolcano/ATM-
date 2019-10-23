using ATMPlus.Database;
using osuTK.Graphics;
using osu.Framework.Screens;
using ATMPlus.Elementos;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Containers;

namespace ATMPlus.Ventanas
{
    public class ConsultaUsuario : VentanaConTitulo
    {
        public ConsultaUsuario(CuentaCliente cuenta)
        {
            ColorFondoTitulo = Color4.MediumSlateBlue;
        }
    }
}
