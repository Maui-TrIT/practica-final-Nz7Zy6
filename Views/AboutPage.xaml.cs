using System.Diagnostics;

namespace ShopApp.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        try
        {
            var accessToken = Preferences.Get("accesstoken", string.Empty);
            if (string.IsNullOrEmpty(accessToken))
            {
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Excepción en AboutPage >> OnAppearing: " + ex.Message);
        }

    }
}