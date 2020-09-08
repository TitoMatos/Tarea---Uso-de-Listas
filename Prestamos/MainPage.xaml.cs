using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prestamos
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private double CalcularCuota(double monto, int mesesPlazo, double taza)
        {
            /***** FORMULA DE CALCULO
             * PMT = -RATE * ( FV + PV * Math.pow(1+RATE,NPER)) / ((Math.pow(1+RATE,NPER)-1));
             */

            double t = taza / 1200;
            double b = Math.Pow((1 + t), mesesPlazo);
            return t * monto * b / (b - 1);
        }

        void Calcular_Clicked(System.Object sender, System.EventArgs e)
        {
            var monto = Double.Parse(Monto.Text);
            var tiempo = int.Parse(Tiempo.Text);
            var tasa = Double.Parse(Tasa.Text);

            if (monto < 100)
            {
                DisplayAlert("Error", "Debe colocar un monto mayor que 100", "Ok");
                return;
            }

            if (tiempo < 0 || tiempo > 360)
            {
                DisplayAlert("Error", "Debe colocar un tiempo entre 1 y 360", "Ok");
                return;
            }

            if (tasa < 0 || tasa > 100)
            {
                DisplayAlert("Error", "Debe colocar una tasa entre 1 y 100", "Ok");
                return;
            }

            // Calcular monto de la cuota usando el metodo privado CalcularCuota
            var cuota = CalcularCuota(monto, tiempo, tasa);

            MontoCuota.Text = cuota.ToString("C");
            CalculoCuotaPrestamo.IsVisible = true;

            // Llenar la lista de cuotas
            List<Cuota> cuotas = new List<Cuota>();
            for (int i = 0; i < tiempo; i++)
            {
                cuotas.Add(new Cuota(i + 1, monto, tiempo, i , tasa));
            }

            // Asignar cuotas a la tabla (ListView)
            TablaAmortizacion.ItemsSource = cuotas;
        }

        async void TablaAmortizacion_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var cuotaSeleccionada = e.SelectedItem as Cuota;
            if (cuotaSeleccionada == null)
            {
                return;
            }

            await Navigation.PushAsync(new DetalleCuotaPage(cuotaSeleccionada));
        }
    }
}
