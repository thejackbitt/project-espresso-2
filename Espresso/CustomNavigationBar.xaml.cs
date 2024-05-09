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

        public static readonly BindableProperty DynamicHeightProperty = BindableProperty.Create(nameof(DynamicHeight), typeof(double), typeof(CustomNavigationBar), default(double));

        public double DynamicHeight
        {
            get => (double)GetValue(DynamicHeightProperty);
            set => SetValue(DynamicHeightProperty, value);
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
                var heightAnimation = menuPanel.AnimateProperty(height => menuPanel.HeightRequest = height, -200, 200, 250, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, heightAnimation);
            }
            else
            {
                var translateAnimation = menuPanel.TranslateTo(0, -200, 250, Easing.CubicInOut);
                var rotateIcon = borger.RotateTo(0, 250, Easing.CubicInOut);
                var heightAnimation = menuPanel.AnimateProperty(height => menuPanel.HeightRequest = height, 200, -200, 250, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, heightAnimation);
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
                var heightAnimation = profilePanel.AnimateProperty(height => profilePanel.HeightRequest = height, -200, 340, 150, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, heightAnimation);
            }
            else
            {
                var translateAnimation = profilePanel.TranslateTo(0, -400, 150, Easing.CubicInOut);
                var heightAnimation = profilePanel.AnimateProperty(height => profilePanel.HeightRequest = height, 200, -200, 350, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, heightAnimation);
                IsProfileVisible = false;
                profilePanel.IsVisible = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
