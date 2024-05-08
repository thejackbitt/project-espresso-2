using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace Espresso;

public partial class Base : ContentPage
{
    public Base()
    {
        InitializeComponent();
        Debug.WriteLine("Base page loaded");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        topBar.IsVisible = false;
        Debug.WriteLine("Base page appearing");
    }

    private async void OnMenuButtonClicked(object sender, EventArgs e)
    {
        if (!topBar.IsVisible)
        {
            topBar.IsVisible = true;
            topBar.TranslationY = -topBar.HeightRequest;
            await topBar.TranslateTo(0, 0, 250, Easing.Linear);
        }
        else
        {
            await topBar.TranslateTo(0, -topBar.HeightRequest, 250, Easing.Linear);
            topBar.IsVisible = false;
        }
    }
}
