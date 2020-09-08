using System;
namespace Prestamos
{
    public class Cuota
    {

        #region Cosntructores

        public Cuota(double monto, int mesesPlazo, double taza)
        {
            this.Nro = 1;
            this.valor = CalcularCuota(monto, mesesPlazo, taza);
            this.totalAmortizado = CalcularTotalAmortizado(monto, mesesPlazo, 1, taza);
            this.amortizacion = totalAmortizado;
            this.saldo = CalcularSaldo(monto, mesesPlazo, 1, taza);
        }

        public Cuota(int nro, double monto, int mesesPlazo, double taza)
        {
            this.Nro = nro;
            this.valor = CalcularCuota(monto, mesesPlazo, taza);
            this.totalAmortizado = CalcularTotalAmortizado(monto, mesesPlazo, 1, taza);
            this.amortizacion = totalAmortizado;
            this.saldo = CalcularSaldo(monto, mesesPlazo, 1, taza);
        }

        public Cuota(int nro, double monto, int mesesPlazo, int periodoActual, double taza)
        {
            this.Nro = nro;
            this.valor = CalcularCuota(monto, mesesPlazo, taza);
            this.totalAmortizado = CalcularTotalAmortizado(monto, mesesPlazo, periodoActual, taza);
            this.amortizacion = CalcularAmortizacion(monto, mesesPlazo, periodoActual, taza);
            this.saldo = CalcularSaldo(monto, mesesPlazo, periodoActual, taza);
        }

        #endregion Cosntructores

        #region Propiedades

        public int Nro { get; set; }

        private double valor;
        public double Valor
        {
            get { return valor; }
        }

        private double amortizacion;
        public double Amortizacion
        {
            get { return amortizacion; }
        }

        public string InteresFormateado { get
            {
                return Interes.ToString("N");
            }
        }

        public string AmortizacionFormateada
        {
            get
            {
                return Amortizacion.ToString("N");
            }
        }

        public double Interes
        {
            get { return valor - amortizacion; }
        }

        private double totalAmortizado;
        public double TotalAmortizado
        {
            get { return totalAmortizado; }
        }

        private double saldo;
        public double Saldo
        {
            get { return saldo; }
        }

        #endregion Propiedades


        #region Metodos Privados

        private double CalcularCuota(double monto, int mesesPlazo, double taza)
        {
            /***** FORMULA DE CALCULO
             * PMT = -RATE * ( FV + PV * Math.pow(1+RATE,NPER)) / ((Math.pow(1+RATE,NPER)-1));
             */

            double t = taza / 1200;
            double b = Math.Pow((1 + t), mesesPlazo);
            return t * monto * b / (b - 1);
        }

        private double CalcularAmortizacion(double monto, int mesesPlazo, int periodo, double taza)
        {
            double totAmortAnterior = 0;
            if (periodo > 1)
                totAmortAnterior = CalcularTotalAmortizado(monto, mesesPlazo, periodo - 1, taza);

            return TotalAmortizado - totAmortAnterior;
        }

        private double CalcularTotalAmortizado(double monto, int n, int p, double taza)
        {
            double t = taza / 1200;
            double b = Math.Pow((1 + t), n);
            double c = Math.Pow((1 + t), p);
            return monto * ((c - 1) / (b - 1));
        }

        private double CalcularSaldo(double monto, int mesesPlazo, int periodo, double taza)
        {
            double t = taza / 1200;
            double b = Math.Pow((1 + t), mesesPlazo);
            double c = Math.Pow((1 + t), periodo);
            return monto * (1 - ((c - 1) / (b - 1)));
        }


        #endregion Metodos Privados

    }
}
