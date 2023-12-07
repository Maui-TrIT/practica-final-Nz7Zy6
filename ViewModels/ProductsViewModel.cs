using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.DataAccess;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ShopApp.ViewModels
{
    public partial class ProductsViewModel : ViewModelGlobal
    {
        private readonly INavegacionService navegacionService;

        [ObservableProperty]
        ObservableCollection<Product> products;

        [ObservableProperty]
        Product productoSeleccionado;

        [ObservableProperty]
        bool isRefreshing;

        public ProductsViewModel(INavegacionService navegacionService)
        {
            this.navegacionService = navegacionService;
            CargarListaProductos();
            PropertyChanged += ProductsViewModel_PropertyChanged;

        }

        [RelayCommand]
        private async Task Refresh()
        {
            try
            {
                CargarListaProductos();
                await Task.Delay(3000);

                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en ProductsViewModel.cs >> Refresh: " + ex.Message);
            }
        }

        private async void ProductsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == nameof(ProductoSeleccionado))
                {
                    var uri = $"{nameof(ProductDetailPage)}?id={ProductoSeleccionado.Id}";
                    await navegacionService.GoToAsync(uri);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en ProductsViewModel.cs >> ProductsViewModel_PropertyChanged: " + ex.Message);
            }

        }

        private void CargarListaProductos()
        {
            try
            {
                var database = new ShopDbContext();
                Products = new ObservableCollection<Product>(database.Products);
                database.Dispose();

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en ProductsViewModel.cs >> CargarListaProductos: " + ex.Message);
            }
        }
    }
}
