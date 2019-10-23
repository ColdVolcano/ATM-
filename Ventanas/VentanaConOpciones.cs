using osu.Framework.Allocation;
using osuTK.Graphics;
using osu.Framework.Screens;
using ATMPlus.Elementos;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;

namespace ATMPlus.Ventanas
{
    public class VentanaConOpciones : VentanaConTitulo
    {
        protected readonly FillFlowContainer<BotonOpcion> FlujoBotones;

        public VentanaConOpciones()
        {
            Add(FlujoBotones = new FillFlowContainer<BotonOpcion>()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(10, 0),
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            FlujoBotones.Add(new BotonOpcion
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                BackgroundColour = Color4.OrangeRed,
                Icono = FontAwesome.Solid.SignOutAlt,
                Text = "Salir",
                Action = this.Exit,
            });
        }
    }
}
