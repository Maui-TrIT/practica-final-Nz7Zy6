using System.Diagnostics;

namespace ShopApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            var uri = new Uri("https://vaxidrez.com");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Excepción en AppShell.cs >> MenuItem_Clicked: " + ex.Message);
        }
    }
}
