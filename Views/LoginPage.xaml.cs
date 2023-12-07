

using ShopApp.ViewModels;
using System.Diagnostics;

namespace ShopApp.Views;

public partial class LoginPage : ContentPage
{
    LoginViewModel _viewModel;
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            _viewModel.RegistroMethodCommand.Execute(null);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Excepción en LoginPage.xaml.cs >> TapGestureRecognizer_Tapped: " + ex.Message);
        }
    }
}