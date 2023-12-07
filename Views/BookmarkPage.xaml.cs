using ShopApp.ViewModels;
using System.Diagnostics;

namespace ShopApp.Views;

public partial class BookmarkPage : ContentPage
{
    private BookmarkViewModel _viewModel;
    public BookmarkPage(BookmarkViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        try
        {
            _viewModel.GetInmueblesCommand.Execute(this);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Excepción en BookmarkPage.cs >> OnAppearing: " + ex.Message);
        }
    }
}