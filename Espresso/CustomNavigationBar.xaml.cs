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

        public ICommand ToggleMenuCommand { get; }

        public CustomNavigationBar()
        {
            InitializeComponent();
            ToggleMenuCommand = new Command(ToggleMenu);
            BindingContext = this;
        }

        private async void ToggleMenu()
        {
            if (!IsMenuVisible)
            {
                IsMenuVisible = true;
                menuPanel.IsVisible = true;
                var translateAnimation = menuPanel.TranslateTo(0, 0, 250, Easing.CubicInOut);
                var heightAnimation = menuPanel.AnimateProperty(height => menuPanel.HeightRequest = height, -200, 200, 250, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, heightAnimation);
            }
            else
            {
                var translateAnimation = menuPanel.TranslateTo(0, -200, 250, Easing.CubicInOut);
                var heightAnimation = menuPanel.AnimateProperty(height => menuPanel.HeightRequest = height, 200, -200, 250, Easing.CubicInOut);
                await Task.WhenAll(translateAnimation, heightAnimation);
                IsMenuVisible = false;
                menuPanel.IsVisible = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
