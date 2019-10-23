using ATMPlus.Database;
using osu.Framework;
using osu.Framework.Allocation;
using ATMPlus.Ventanas;
using osu.Framework.Screens;
using osu.Framework.Graphics;

namespace ATMPlus
{
    public class ATMWindow : Game
    {
        private Login loginContainer;
        private ScreenStack stack;

        [BackgroundDependencyLoader]
        private void load()
        {
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Add(stack = new ScreenStack() { RelativeSizeAxes = Axes.Both });
            LoadComponent(loginContainer = new Login());
            stack.Push(loginContainer);
        }
    }
}
