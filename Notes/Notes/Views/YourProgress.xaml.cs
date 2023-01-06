using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

public static class StringExt
{
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }
}

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YourProgress : ContentPage
    {
        public YourProgress()
        {
            InitializeComponent();
        }
        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.NewValue < 10)
                valueLabel.Text = ((args.NewValue).ToString("F3")).Truncate(1);
            else if (args.NewValue < 100)
            {
                valueLabel.Text = ((args.NewValue).ToString("F3")).Truncate(2);
            }
            else
            {
                valueLabel.Text = ((args.NewValue).ToString("F4")).Truncate(3);
            }

        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await DisplayAlert("Clicked!",
                "The button labeled '" + button.Text + "' has been clicked",
                "Please fuck off!");
        }

    }
}