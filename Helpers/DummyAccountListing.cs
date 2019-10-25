using System;
using System.Diagnostics.CodeAnalysis;

namespace ATMPlus.Helpers
{
    public sealed class DummyAccountListing : IComparable<DummyAccountListing>
    {
        public DummyAccountListing(int account)
        {
            Account = account;
        }

        public int Account;

        public override string ToString()
        {
            return Account == 0 ? "Todas" : Account.ToString();
        }

        public int CompareTo([AllowNull] DummyAccountListing other)
        {
            return Account.CompareTo(other.Account);
        }
    }
}
