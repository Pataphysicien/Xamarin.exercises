using System;

using Xamarin.Forms;
using Connectivity.Plugin;
using Connectivity.Plugin.Abstractions;

namespace NetStatus
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = 
                (CrossConnectivity.Current.IsConnected ? (ContentPage)new NetworkViewPage() : (ContentPage)new NoNetworkPage());
                

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Type currentPage = this.MainPage.GetType();
            if (e.IsConnected && currentPage != typeof(NetworkViewPage))
                this.MainPage = new NetworkViewPage();
            else if (!e.IsConnected && currentPage != typeof(NoNetworkPage))
                this.MainPage = new NoNetworkPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

