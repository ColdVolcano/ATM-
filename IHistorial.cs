using System;
namespace ATMPlus
{
    public interface IHistorial
    {
        int ID { get; set; }

        int CuentaOrigen { get; set; }

        DateTime FechaHora { get; set; }

    }
}
