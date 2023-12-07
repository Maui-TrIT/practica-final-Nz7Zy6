using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public partial class ResumenViewModel : ViewModelGlobal
    {
        [ObservableProperty]
        int visitas = 0;

        [ObservableProperty]
        int clients = 0;

        [ObservableProperty]
        decimal total = 0;

        [ObservableProperty]
        int totalProducts = 0;

        private ShopOutDbContext _shopOutDbContext;
        public Command GetResumenCommand { get; }

        public ResumenViewModel(ShopOutDbContext shopOutDbContext)
        {
            try
            {
                _shopOutDbContext = shopOutDbContext;
                GetResumenCommand = new Command(async () => await LoadDataAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en ResumenViewModel >> ResumenViewModel: " + ex.Message);
            }
        }

        public async Task LoadDataAsync()
        {
            if (IsBusy)
                return;


            try
            {
                IsBusy = true;
                var db = new ShopDbContext();
                Visitas = _shopOutDbContext.Compras
                        .ToList()
                        .DistinctBy(s => s.ClientId)
                        .ToList()
                        .Count();


                Clients = db.Clients.Count();

                Total = _shopOutDbContext.Compras.ToList().Sum(s => s.Cantidad * s.Precio);
                TotalProducts = _shopOutDbContext.Compras.Sum(s => s.Cantidad);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en ResumenViewModel >> LoadDataAsync: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}

