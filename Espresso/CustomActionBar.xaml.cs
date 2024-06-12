using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Animations;
using System.Security.Cryptography.X509Certificates;

namespace Espresso
{

    public partial class CustomActionBar : ContentView, INotifyPropertyChanged
    {

       public bool isOpen = false;
       public ICommand ActionBarCommand { get; set; }

       public CustomActionBar()
        {
            InitializeComponent();
            ActionBarCommand = new Command(OpenActionBar);
            BindingContext = this;
            this.HeightRequest = 100;
        }

        async void OpenActionBar()
        {
            if (!this.isOpen)
            {
                isOpen = true;
                this.ScaleYTo(3, 500, Easing.CubicInOut);
                await this.TranslateTo(0, -100, 500, Easing.CubicInOut);
            }
            else
            {
                this.ScaleYTo(1, 500, Easing.CubicInOut);
                await this.TranslateTo(0, 0, 500, Easing.CubicInOut);
                isOpen = false;
            }
        }

    }
}