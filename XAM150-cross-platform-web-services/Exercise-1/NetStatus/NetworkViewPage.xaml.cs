using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Connectivity.Plugin;
using Connectivity.Plugin.Abstractions;
using System.Linq;

namespace NetStatus
{
    public partial class NetworkViewPage : ContentPage
    {
        public NetworkViewPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();

            ConnectionDetails.Text = CrossConnectivity.Current
                .ConnectionTypes.First ().ToString ();

            CrossConnectivity.Current.ConnectivityChanged += UpdateNetworkInfo;
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();

            CrossConnectivity.Current.ConnectivityChanged -= UpdateNetworkInfo;
        }

        private void UpdateNetworkInfo (object sender, ConnectivityChangedEventArgs e)
        {
            if (CrossConnectivity.Current != null && CrossConnectivity.Current.ConnectionTypes != null) {
                var connectionType = CrossConnectivity.Current.ConnectionTypes.FirstOrDefault ();
                ConnectionDetails.Text = connectionType.ToString ();
            }
        }
    }
}

