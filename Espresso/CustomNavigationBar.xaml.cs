using System;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Animations;

namespace Espresso
{
    public partial class CustomNavigationBar : ContentView, INotifyPropertyChanged
    {
        private bool _isMenuVisible;
        private bool _isProfileVisible;

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

        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            private set
            {
                if (_isMenuVisible != value)
                {
                    _isMenuVisible = value;
                    OnPropertyChanged(nameof(IsMenuVisible));
                }
            }
        }

        public bool IsProfileVisible
        {
            get => _isProfileVisible;
            private set
            {
                if (_isProfileVisible != value)
                {
                    _isProfileVisible = value;
                    OnPropertyChanged(nameof(IsProfileVisible));
                }
            }
        }

        public ICommand ToggleMenuCommand { get; }
        public ICommand ToggleProfileCommand { get; }

        public CustomNavigationBar()
        {
            InitializeComponent();
            ToggleMenuCommand = new Command(ToggleMenu);
            ToggleProfileCommand = new Command(ToggleProfile);
            BindingContext = this;
        }

        private async void ToggleMenu()
        {
            if (!IsMenuVisible)
            {
                IsProfileVisible = false;
                profilePanel.IsVisible = false;
                IsMenuVisible = true;
                menuPanel.IsVisible = true;
                var translateAnimation = menuPanel.TranslateTo(0, 0, 250, Easing.CubicInOut);
                var rotateIcon = borger.RotateTo(90, 250, Easing.CubicInOut);
                var heightAnimation = menuPanel.AnimateProperty(height => DynamicHeight = height, 50, 240, 250, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, rotateIcon, heightAnimation);
            }
            else
            {
                var translateAnimation = menuPanel.TranslateTo(0, -200, 250, Easing.CubicInOut);
                var rotateIcon = borger.RotateTo(0, 250, Easing.CubicInOut);
                var heightAnimation = menuPanel.AnimateProperty(height => DynamicHeight = height, 240, 50, 250, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, rotateIcon, heightAnimation);
                IsMenuVisible = false;
                menuPanel.IsVisible = false;
            }
        }

        private async void ToggleProfile()
        {
            if (!IsProfileVisible)
            {
                var rotateIcon = borger.RotateTo(0, 250, Easing.CubicInOut);
                IsMenuVisible = false;
                menuPanel.IsVisible = false;
                IsProfileVisible = true;
                profilePanel.IsVisible = true;
                var translateAnimation = profilePanel.TranslateTo(0, 0, 350, Easing.CubicInOut);
                var heightAnimation = profilePanel.AnimateProperty(height => DynamicHeight = height, 50, 360, 200, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, rotateIcon, heightAnimation);
            }
            else
            {
                var translateAnimation = profilePanel.TranslateTo(0, -400, 150, Easing.CubicInOut);
                var heightAnimation = profilePanel.AnimateProperty(height => DynamicHeight = height, 360, 50, 350, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, heightAnimation);
                IsProfileVisible = false;
                profilePanel.IsVisible = false;
            }
        }

        private async void RedirectLink(object sender, EventArgs e)
        {
            var button = sender as Button;
            var url = button?.CommandParameter as string;

            if (!string.IsNullOrEmpty(url))
            {
                await Launcher.OpenAsync(new Uri(url));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
