using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.Models.Backend.Inmueble;
using ShopApp.Services;
using ShopApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public partial class BookmarkViewModel : ViewModelGlobal
    {
        [ObservableProperty]
        ObservableCollection<InmuebleResponse> inmuebles;

        [ObservableProperty]
        private InmuebleResponse inmuebleSeleccionado;

        private readonly INavegacionService _navegacionService;

        private readonly InmuebleService _inmuebleService;

        public Command GetInmueblesCommand { get; }

        public BookmarkViewModel(
            INavegacionService navegacionService,
            InmuebleService inmuebleService
            )
        {
            _navegacionService = navegacionService;
            _inmuebleService = inmuebleService;
            GetInmueblesCommand = new Command(async () => await LoadDataAsync());
            PropertyChanged += BookmarkViewModel_PropertyChanged;
        }

        private async void BookmarkViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == nameof(InmuebleSeleccionado))
                {
                    var uri = $"{nameof(InmuebleDetailPage)}?id={InmuebleSeleccionado.Id}";
                    await _navegacionService.GoToAsync(uri);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en BookmarkViewModel >> BookmarkViewModel_PropertyChanged: " + ex.Message);
            }
        }

        public async Task LoadDataAsync()
        {
            if (IsBusy)
                return;


            try
            {
                IsBusy = true;
                var listInmuebles = await _inmuebleService.GetBookmarks();
                Inmuebles = new ObservableCollection<InmuebleResponse>(listInmuebles);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en BookmarkViewModel >> LoadDataAsync: " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
