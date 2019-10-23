using System;
using ATMPlus.Database;
using osu.Framework;
using osu.Framework.Platform;

namespace ATMPlus
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using(var db = new DatabaseStore())
            {
                foreach (var h in db.ObtenerHistorial())
                {
                    switch (h)
                    {
                        case HistorialDeposito deposito:
                            Console.WriteLine($"Deposito: {deposito.CuentaOrigen}->{deposito.CuentaDestino} (Cantidad: {deposito.Cantidad}, Fecha: {deposito.FechaHora:MM/dd/yyyy HH:mm})");
                            break;
                        case HistorialRetiro retiro:
                            Console.WriteLine($"Deposito: {retiro.CuentaOrigen} (Cantidad: {retiro.Cantidad}, Fecha: {retiro.FechaHora:MM/dd/yyyy HH:mm})");
                            break;
                    }
                }
            }
            using (GameHost host = Host.GetSuitableHost(@"ATMPlus"))
            using (Game game = new ATMWindow())
                host.Run(game);
        }
    }
}
