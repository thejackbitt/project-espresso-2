using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Espresso
{
    public partial class CustomNavigationBar : ContentView, INotifyPropertyChanged
    {
        private bool _isMenuVisible;
        public bool IsMenuVisible
        {
            get => _isMenuVisible;
            set
            {
                _isMenuVisible = value;
                OnPropertyChanged(nameof(IsMenuVisible));
            }
        }

        public ICommand ToggleMenuCommand { get; }

        public CustomNavigationBar()
        {
            InitializeComponent();
            ToggleMenuCommand = new Command(() => IsMenuVisible = !IsMenuVisible);
            BindingContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
