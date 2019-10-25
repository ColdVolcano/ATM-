using System;

namespace ATMPlus.Helpers
{
    public struct PendingDeposit
    {
        public DateTime Time;
        public int Destination;
        public double Ammount;

        public PendingDeposit(DateTime time, int destinationAccouunt, double ammount)
        {
            Time = time;
            Destination = destinationAccouunt;
            Ammount = ammount;
        }
    }
}
