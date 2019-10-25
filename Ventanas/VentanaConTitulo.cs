using osu.Framework.Graphics.Containers;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using System;
using ATMPlus.Elementos;

namespace ATMPlus.Ventanas
{
    public class VentanaConTitulo : Screen
    {
        private const float text_pad = 25f;
        private const double fade_duration = 300;
        private const double text_duration = 400;

        private bool alertAutoExit;
        private bool alertAutoHide;

        private ConditionalInputContainer contentChild;
        private Container alertContainer;
        private TextFlowContainer alertText;

        private Box colorBox;
        private SpriteText greetText;

        protected Action OnAlertHide;
        protected string Titulo { get { return greetText.Text; } set { greetText.Text = value; } }
        protected Color4 ColorFondoTitulo { get { return colorBox.Colour; } set { colorBox.Colour = value; } }
        public VentanaConTitulo()
        {
            AddInternal(contentChild = new ConditionalInputContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Padding = new MarginPadding { Top = 100 },
                RelativeSizeAxes = Axes.Both,
            });

            AddInternal(new Container
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.X,
                Height = 100,
                Children = new Drawable[]
                {
                    colorBox = new Box
                    {
                        Alpha = 0,
                        Colour = Color4.DeepSkyBlue,
                        RelativeSizeAxes = Axes.Both,
                    },
                    greetText = new SpriteText
                    {
                        Alpha = 0,
                        Font = new FontUsage(size: 100 - text_pad*2),
                        Padding = new MarginPadding { Vertical = text_pad, Left = text_pad },
                        X = -text_pad
                    },
                }
            });

            AddInternal(alertContainer = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Padding = new MarginPadding { Top = 100 },
                RelativeSizeAxes = Axes.Both,
                Alpha = 0,
                Children = new Drawable[]
                {
                    new Box
                    {
                        Colour = Color4.Black,
                        Alpha = 0.5f,
                        RelativeSizeAxes = Axes.Both
                    },
                    alertText = new TextFlowContainer
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        AutoSizeAxes = Axes.Both,
                        TextAnchor = Anchor.Centre,
                        ParagraphSpacing = 10,
                    }
                }
            });
        }

        protected void Add(Drawable drawable)
        {
            contentChild.Add(drawable);
        }

        protected void MostrarAlerta(string texto, bool autoExit = false, bool autoHide = false)
        {
            contentChild.ShouldHandleInput = false;
            alertText.Text = "";
            alertText.AddText(texto, font => { font.Font = new FontUsage(size: 50); font.Shadow = true; font.ShadowColour = Color4.Black; });
            alertContainer.FadeIn(200);
            if (autoHide)
                Scheduler.AddDelayed(() => handleAlert(false), 4000);
            alertAutoExit = autoExit;
            alertAutoHide = autoHide;
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);

            this.FadeIn();

            greetText
                .Delay(fade_duration)
                .MoveTo(Direction.Horizontal, 0, text_duration, Easing.OutQuart)
                .FadeIn(text_duration, Easing.OutQuint);

            contentChild
                .ScaleTo(1.25f)
                .Then()
                .ScaleTo(1f, fade_duration, Easing.OutQuint)
                .FadeInFromZero(fade_duration, Easing.OutQuint);

            colorBox
                .FadeIn(fade_duration);
        }

        public override void OnSuspending(IScreen next)
        {
            base.OnSuspending(next);

            greetText
                .MoveTo(Direction.Horizontal, -text_pad, fade_duration, Easing.OutQuart)
                .FadeOut(fade_duration, Easing.OutQuint);

            contentChild
                .ScaleTo(0.75f, fade_duration, Easing.OutQuint)
                .FadeOut(fade_duration, Easing.OutQuint);

            this.Delay(fade_duration).FadeOut();
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);

            this.FadeIn();

            greetText
                .Delay(fade_duration)
                .MoveTo(Direction.Horizontal, 0, text_duration, Easing.OutQuart)
                .FadeIn(text_duration, Easing.OutQuint);

            contentChild
                .ScaleTo(1f, fade_duration, Easing.OutQuint)
                .FadeIn(fade_duration, Easing.OutQuint);
        }

        public override bool OnExiting(IScreen next)
        {
            greetText
                .MoveTo(Direction.Horizontal, -text_pad, fade_duration, Easing.OutQuart)
                .FadeOut(fade_duration, Easing.OutQuint);

            contentChild
                .ScaleTo(1.25f, fade_duration, Easing.OutQuint)
                .FadeOut(fade_duration, Easing.OutQuint);

            colorBox
                .FadeOut(fade_duration);

            alertContainer.ClearTransforms();
            alertContainer.FadeOut(200);

            this.Delay(fade_duration)
                .FadeOut();


            return base.OnExiting(next);
        }

        private bool handleAlert(bool input = true)
        {
            if (alertContainer.Alpha > 0)
            {
                if (!alertAutoHide || !input)
                {
                    alertContainer.ClearTransforms();
                    alertContainer.FadeOut(200);
                    contentChild.ShouldHandleInput = true;
                }
                if (alertAutoExit)
                    this.Exit();
                if (!alertAutoHide || !input)
                {
                    OnAlertHide?.Invoke();
                }
                return true;
            }
            return false;
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            return handleAlert() ? true : base.OnMouseDown(e);
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            return handleAlert() ? true : base.OnKeyDown(e);
        }
    }
}
