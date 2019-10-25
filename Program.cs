using System;
using osu.Framework;
using osu.Framework.Platform;

namespace ATMPlus
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost(@"ATMPlus"))
            using (Game game = new ATMWindow())
                host.Run(game);
        }
    }
}
