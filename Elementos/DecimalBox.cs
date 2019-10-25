using System.Linq;

namespace ATMPlus.Elementos
{
    public class DecimalBox : NumberBox
    {
        protected override bool CanAddCharacter(char character) => (character == '.' && Text.Count(c => c == '.') < 1) || base.CanAddCharacter(character);
    }
}
