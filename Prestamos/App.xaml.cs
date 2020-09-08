using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prestamos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var navegacion = new NavigationPage(new MainPage());
            MainPage = navegacion;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
