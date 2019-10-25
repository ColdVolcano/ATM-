using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ATMPlus.Helpers
{
    public class YearMonthTuple : IComparable<YearMonthTuple>
    {
        public YearMonthTuple(int year, int month)
        {
            Year = year;
            Month = month;
        }

        public int Year;
        public int Month;

        public override string ToString()
        {
            return $"{Month}/{Year}";
        }


        public static explicit operator DateTime(YearMonthTuple d) => new DateTime(d.Year, d.Month, 1);

        public int CompareTo([AllowNull] YearMonthTuple other)
        {
            return ((DateTime)this).CompareTo((DateTime)other);
        }
    }
}
