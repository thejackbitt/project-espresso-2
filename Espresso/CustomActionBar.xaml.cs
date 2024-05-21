using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Animations;

namespace Espresso;

public partial class CustomActionBar : ContentView
{
    private bool _isLoaderVisible;

    public static readonly BindableProperty DynamicHeightProperty =
            BindableProperty.Create(
                nameof(DynamicHeight),
                typeof(double),
                typeof(CustomNavigationBar),
                50.0);
    public double DynamicHeight
    {
        get => (double)GetValue(DynamicHeightProperty);
        set
        {
            SetValue(DynamicHeightProperty, value);
            OnPropertyChanged(nameof(DynamicHeight));
        }
    }

    public bool IsLoaderVisible
    {
        get => _isLoaderVisible;
        private set
        {
            if (_isLoaderVisible != value)
            {
                _isLoaderVisible = value;
                OnPropertyChanged(nameof(IsLoaderVisible));
            }
        }
    }

    public ICommand ToggleLoaderCommand { get; }

    public CustomActionBar()
	{
		InitializeComponent();
        ToggleLoaderCommand = new Command(ToggleLoader);
        BindingContext = this;
	}
    public async void ToggleLoader()
    {
        if (!IsLoaderVisible)
        {
            var customNavBar = (this.Parent as StackLayout)?.FindByName<CustomNavigationBar>("customNavBar");
            customNavBar?.KillTop();
            IsLoaderVisible = true;
            loaderPanel.IsVisible = true;
            var translateAnimation = loaderPanel.TranslateTo(0, 0, 350, Easing.CubicInOut);
            var heightAnimation = loaderPanel.AnimateProperty(height => DynamicHeight = height, 50, 360, 200, Easing.CubicInOut);
            await Task.WhenAll(translateAnimation, heightAnimation);
        }
        else
        {
            IsLoaderVisible = false;
            loaderPanel.IsVisible = false;
            var translateAnimation = loaderPanel.TranslateTo(0, -400, 150, Easing.CubicInOut);
            var heightAnimation = loaderPanel.AnimateProperty(height => DynamicHeight = height, 360, 50, 350, Easing.CubicInOut);
            await Task.WhenAll(translateAnimation, heightAnimation);
        }
    }

    public async void KillBottom()
    {
        if (IsLoaderVisible)
        {
            IsLoaderVisible = false;
            loaderPanel.IsVisible = false;
            var translateAnimation = loaderPanel.TranslateTo(0, -400, 150, Easing.CubicInOut);
            var heightAnimation = loaderPanel.AnimateProperty(height => DynamicHeight = height, 360, 50, 350, Easing.CubicInOut);
            await Task.WhenAll(translateAnimation, heightAnimation);
        }
    }
}