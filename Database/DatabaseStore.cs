using Microsoft.EntityFrameworkCore;
using osu.Framework.Lists;
using System.Linq;

namespace ATMPlus.Database
{
    public class DatabaseStore : DbContext
    {
#pragma warning disable IDE1006 // Estilos de nombres
        DbSet<Cuenta> Cuenta { get; set; }
        DbSet<CuentaGerente> CuentaGerente { get; set; }
        DbSet<CuentaCliente> CuentaCliente { get; set; }
        public DbSet<HistorialConsulta> HistorialConsulta { get; set; }
        public DbSet<HistorialRetiro> HistorialRetiro { get; set; }
        public DbSet<HistorialDeposito> HistorialDeposito { get; set; }


#pragma warning restore IDE1006 // Estilos de nombres
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              @"Data Source=ColdVolcano\MSSQLSERVER01;" +
              "Initial Catalog=ATM_Data;" +
              "Integrated Security=true;" +
              "MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public SortedList<IHistorial> ObtenerHistorial(int cuenta = 0)
        {
            //CuentaCliente.Where(c => cuenta == 0 ? true : cuenta == c.NumeroCuenta).Select(c => c.NumeroCuenta)
            var historial = new SortedList<IHistorial>((obj1, obj2) => obj1.FechaHora > obj2.FechaHora ? 1 : -1);
            historial.AddRange(HistorialDeposito.Where(c => cuenta == 0 ? true : cuenta == c.CuentaOrigen));
            historial.AddRange(HistorialRetiro.Where(c => cuenta == 0 ? true : cuenta == c.CuentaOrigen));
            return historial;
        }

        public ICuenta InicioSesion(int numCuenta, string pinPass)
        {
            var cuenta = Cuenta.FirstOrDefault(c => c.NumeroCuenta == numCuenta && c.PinPass == pinPass);
            if (cuenta == null)
                return null;
            if (cuenta.TipoCuenta == TipoCuenta.Gerente)
            {
                var cuentaGerente = CuentaGerente.First(c => cuenta.NumeroCuenta == c.NumeroCuenta);
                cuentaGerente.Nombre = new Nombre(cuenta);
                return cuentaGerente;
            }
            else if (cuenta.TipoCuenta == TipoCuenta.Cliente)
            {
                var cuentaGerente = CuentaCliente.First(c => cuenta.NumeroCuenta == c.NumeroCuenta);
                cuentaGerente.Nombre = new Nombre(cuenta);
                return cuentaGerente;
            }
            return null;
        }
    }
}
