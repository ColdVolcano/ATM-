using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace ATMPlus.Elementos
{
    public class BotonOpcion : BotonHover
    {
        public IconUsage Icono
        {
            set
            {
                icon.Icon = value;
            }
        }

        private readonly SpriteIcon icon;

        public BotonOpcion()
        {
            Size = new Vector2(180);

            Add(icon = new SpriteIcon
            {
                Icon = FontAwesome.Regular.QuestionCircle,
                Size = new Vector2(60),
                Colour = Color4.WhiteSmoke,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }

        protected override SpriteText CreateText()
        {
            return new SpriteText()
            {
                Origin = Anchor.BottomCentre,
                Anchor = Anchor.BottomCentre,
                Font = FrameworkFont.Regular.With(size: 30),
                Colour = Color4.White,
                Margin = new MarginPadding { Bottom = 20},
            };
        }
    }
}
