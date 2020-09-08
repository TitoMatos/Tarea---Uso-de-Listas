using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Prestamos
{
    public partial class DetalleCuotaPage : ContentPage
    {
        public DetalleCuotaPage(Cuota cuota)
        {
            InitializeComponent();

            NumeroCuota.Text = cuota.Nro.ToString();
            InteresCuota.Text = cuota.InteresFormateado;
            AmortizacionCuota.Text = cuota.AmortizacionFormateada;
            TotalCuota.Text = (cuota.Interes + cuota.Amortizacion).ToString("N");
        }
    }
}
