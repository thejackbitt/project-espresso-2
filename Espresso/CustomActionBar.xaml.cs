using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Animations;

namespace Espresso;

public partial class CustomActionBar : ContentView, INotifyPropertyChanged
{
    private bool _isLoaderVisible;

    public static readonly BindableProperty DynamicHeightProperty_bottom =
            BindableProperty.Create(
                nameof(DynamicHeight_bottom),
                typeof(double),
                typeof(CustomActionBar),
                50.0);

    public double DynamicHeight_bottom
    {
        get => (double)GetValue(DynamicHeightProperty_bottom);
        set
        {
            SetValue(DynamicHeightProperty_bottom, value);
            OnPropertyChanged(nameof(DynamicHeight_bottom));
        }
    }

    public static readonly BindableProperty DynamicInputTransparencyProperty =
        BindableProperty.Create(
            nameof(DynamicInputTransparency),
            typeof(bool),
            typeof(CustomActionBar),
            true);

    public bool DynamicInputTransparency
    {
        get => (bool)GetValue(DynamicInputTransparencyProperty);
        set
        {
            SetValue(DynamicInputTransparencyProperty, value);
            OnPropertyChanged(nameof(DynamicInputTransparency));
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

    public ICommand KillBottomCommand { get; }

    public CustomActionBar()
    {
        InitializeComponent();
        ToggleLoaderCommand = new Command(ToggleLoader);
        KillBottomCommand = new Command(KillBottom);
        BindingContext = this;
    }

    public async void ToggleLoader()
    {
        if (!IsLoaderVisible)
        {
            DynamicInputTransparency = false;
            IsLoaderVisible = true;
            loaderPanel.IsVisible = true;
            var translateAnimation = loaderPanel.TranslateTo(0, 25, 350, Easing.CubicInOut);
            var heightAnimation = loaderPanel.AnimateProperty(height => DynamicHeight_bottom = height, 50, 800, 200, Easing.CubicInOut);
            // System.Diagnostics.Debug.WriteLine($"Height is now {DynamicHeight_bottom}");
            await Task.WhenAll(translateAnimation, heightAnimation);
        }
        else
        {
            DynamicInputTransparency = true;
            await CloseLoader();
        }
    }

    public async void KillBottom()
    {
        if (IsLoaderVisible)
        {
            DynamicInputTransparency = true;
            await CloseLoader();
        }
    }

    private async Task CloseLoader()
    {
        var translateAnimation = loaderPanel.TranslateTo(0, 200, 150, Easing.CubicInOut);
        var heightAnimation = loaderPanel.AnimateProperty(height => DynamicHeight_bottom = height, 800, 50, 350, Easing.CubicInOut);
        // System.Diagnostics.Debug.WriteLine($"Height is now {DynamicHeight_bottom}");
        await Task.WhenAll(translateAnimation, heightAnimation);
        IsLoaderVisible = false;
        loaderPanel.IsVisible = false;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}