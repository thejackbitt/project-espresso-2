
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Espresso.ViewModel;
    public partial class MainViewModel : ObservableObject
    {

    public MainViewModel()
    {
        Items = new ObservableCollection<string>();
        Text = string.Empty;
    }

    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty] 
    string text;

    [RelayCommand]
    void Add()
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;

        Items.Add(Text);
        // add item
        Text = string.Empty;
    }

    [RelayCommand]
    void Delete(string s)
    {
        if(Items.Contains(s))
        {
            Items.Remove(s);
        }
    }

    }
