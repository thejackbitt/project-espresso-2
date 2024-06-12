using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Animations;

namespace Espresso
{

    public partial class CustomActionBar : ContentView, INotifyPropertyChanged
    {
       public CustomActionBar()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}