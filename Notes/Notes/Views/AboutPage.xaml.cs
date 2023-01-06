using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Notes.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            /*Button button = new Button
            {
                Text = "Navigate!",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            button.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new YourProgress());
            };

            Content = button; */
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            // Launch the specified URL in the system browser.
            await Navigation.PushAsync(new YourProgress());
        }

        async void OnOtherButtonClicked(object sender, EventArgs e)
        {
            // Launch the specified URL in the system browser.
            await Navigation.PushAsync(new ClockPage());
        }
    }
}