using osu.Framework;
using ATMPlus.Ventanas;
using osu.Framework.Screens;
using osu.Framework.Graphics;

namespace ATMPlus
{
    public class ATMWindow : Game
    {
        private Login loginContainer;
        private ScreenStack stack;

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Add(stack = new ScreenStack() { RelativeSizeAxes = Axes.Both });
            LoadComponent(loginContainer = new Login());
            stack.Push(loginContainer);
        }
    }
}
