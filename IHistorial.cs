using osu.Framework.Graphics.Sprites;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ATMPlus
{
    public interface IHistorial : IComparable<IHistorial>
    {
        int ID { get; set; }

        int CuentaOrigen { get; set; }

        DateTime FechaHora { get; set; }
        int IComparable<IHistorial>.CompareTo([AllowNull] IHistorial other)
        {
            return FechaHora > other.FechaHora ? -1 : 1;
        }
    }
}
