using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileClient
{
    public partial class MainPage : ContentPage
    {
        Label label;

        public MainPage()
        {
            Label header = new Label
            {
                Text = "Переключатель",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Switch switcher = new Switch
            {
                IsToggled = true,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand

            };

            switcher.Toggled += switcher_Toggled;

            Slider slider = new Slider
            {
                Value = 0.0,
                Minimum = 0.0,
                Maximum = 1.0,
                ThumbColor = Color.Black,
                MinimumTrackColor = Color.Gray,
                MaximumTrackColor = Color.Red
            };

            slider.ValueChanged += button_Clicked;
            Button button = new Button();
            button.Clicked += button_Clicked;

            label = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            this.Content = new StackLayout { Children = { header, switcher, slider, button, label } };


        }

        private void button_Clicked(object sender, EventArgs e)
        {
            Client.but();
        }

        private void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            label.Text = $"Значение {e.Value}";
        }
    }
}