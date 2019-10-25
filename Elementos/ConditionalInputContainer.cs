using osu.Framework.Graphics.Containers;

namespace ATMPlus.Elementos
{
    public class ConditionalInputContainer : Container
    {
        public bool ShouldHandleInput = true;

        public override bool HandlePositionalInput => base.HandlePositionalInput && ShouldHandleInput;

        public override bool HandleNonPositionalInput => base.HandleNonPositionalInput && ShouldHandleInput;

        public override bool PropagateNonPositionalInputSubTree => base.PropagateNonPositionalInputSubTree && ShouldHandleInput;

        public override bool PropagatePositionalInputSubTree => base.PropagatePositionalInputSubTree && ShouldHandleInput;
    } 
}
