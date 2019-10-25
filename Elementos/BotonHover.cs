using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;

namespace ATMPlus.Elementos
{
    public class BotonHover : Button
    {
        private readonly Box hover;

        public BotonHover()
        {
            Add(hover = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Alpha = 0,
            });
            Enabled.ValueChanged += v =>
            {
                if (v.NewValue && IsHovered)
                    hover.FadeTo(0.2f, 150);
                else
                    hover.FadeOut(150);
            };
        }

        protected override bool OnHover(HoverEvent e)
        {
            if(Enabled.Value)
            hover.FadeTo(0.2f, 150);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            hover.FadeOut(150);
            base.OnHoverLost(e);
        }
    }
}
