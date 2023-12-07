using ShopApp.ViewModels;
using System.Diagnostics;

namespace ShopApp.Views;

public partial class ResumenPage : ContentPage
{
    private ResumenViewModel _viewModel;

    public ResumenPage(ResumenViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        try
        {
            _viewModel.GetResumenCommand.Execute(this);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Excepción en ResumenPage.cs >> OnAppearing: " + ex.Message);
        }
    }
}