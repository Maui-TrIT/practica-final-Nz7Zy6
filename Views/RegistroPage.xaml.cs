using ShopApp.ViewModels;
using System.Diagnostics;

namespace ShopApp.Views;

public partial class RegistroPage : ContentPage
{
    RegistroViewModel _viewModel;

    public RegistroPage(RegistroViewModel viewModel)
    {
        try
        {
            _viewModel = viewModel;
            InitializeComponent();
            BindingContext = viewModel;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Excepción en RegistroPage.xaml.cs >> RegistroPage: " + ex.Message);
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            if (_viewModel != null) _viewModel.BackLoginMethodCommand.Execute(null);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Excepción en RegistroPage.xaml.cs >> TapGestureRecognizer_Tapped: " + ex.Message);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (entryPassword.IsPassword)
            {
                entryPassword.IsPassword = false;
                btnPassword.Source = "no_visible.png";
            }
            else
            {
                entryPassword.IsPassword = true;
                btnPassword.Source = "visible.png";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Excepción en RegistroPage.xaml.cs >> Button_Clicked: " + ex.Message);
        }
    }
}