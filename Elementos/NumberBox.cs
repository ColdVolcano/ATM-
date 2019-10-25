using osu.Framework.Graphics.UserInterface;

namespace ATMPlus.Elementos
{
    public class NumberBox : TextBox
    {
        protected override bool CanAddCharacter(char character) => char.IsNumber(character);
    }
}
