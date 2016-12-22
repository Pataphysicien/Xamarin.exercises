using System;
using Xamarin.Forms;

namespace Phoneword.Core
{
    public class MainPage : ContentPage
    {
        private readonly Entry  _entry;
        private readonly Button _btTranslate;
        private readonly Button _btCall;

        public MainPage()
        {
            this._entry = new Entry { Text = "1-855-XAMARIN" };
            this._btTranslate = new Button { Text = "Translate" };
            this._btTranslate.Clicked += BtTranslate_Clicked;
            this._btCall = new Button() { Text = "Call" };
            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new Label  { Text = "Enter a Phoneword", },
                    _entry,
                    _btTranslate,
                    _btCall
                },
                Spacing = 15
            };
            this.Padding = new Thickness(
                left: 20,
                top: Device.OnPlatform(iOS: 40, Android: 20, WinPhone: 20),
                bottom: 20,
                right: 20);
        }

        private void BtTranslate_Clicked(object sender, EventArgs e)
        {
            string number;
            if (_btCall.IsEnabled = PhonewordTranslator.TryToNumber(_entry.Text, out number))
            {
                _entry.Text = number;
                _btCall.Text = "Call " + number;
            }
        }
    }
}
