using CommunityToolkit.Mvvm.ComponentModel;
using ShopApp.Models.Backend.Inmueble;
using ShopApp.Services;
using ShopApp.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShopApp.ViewModels
{
    public partial class InmuebleListViewModel : ViewModelGlobal, IQueryAttributable
    {
        [ObservableProperty]
        InmuebleResponse inmuebleSeleccionado;

        private readonly INavegacionService _navegacionService;

        [ObservableProperty]
        ObservableCollection<InmuebleResponse> inmuebles;

        private readonly InmuebleService _inmuebleService;

        public InmuebleListViewModel(INavegacionService navegacionService, InmuebleService inmuebleService)
        {
            _navegacionService = navegacionService;
            _inmuebleService = inmuebleService;
            PropertyChanged += InmuebleListViewModel_PropertyChanged;
        }

        private async void InmuebleListViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
                Debug.WriteLine("Excepción en InmuebleListViewModel.cs >> InmuebleListViewModel_PropertyChanged: " + ex.Message);
            }
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            try
            {
                var id = int.Parse(query["id"].ToString());
                await LoadDataAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en InmuebleListViewModel.cs >> ApplyQueryAttributes: " + ex.Message);
            }
        }

        public async Task LoadDataAsync(int categoryId)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var listInmuebles = await _inmuebleService.GetInmueblesByCategory(categoryId);
                Inmuebles = new ObservableCollection<InmuebleResponse>(listInmuebles);
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error", e.Message, "Aceptar");
            }
            finally
            {
                IsBusy = false;
            }



        }

    }

}