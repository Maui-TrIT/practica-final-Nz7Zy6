
using CommunityToolkit.Mvvm.Input;
using ShopApp.Services;
using ShopApp.Views;
using System.Diagnostics;

namespace ShopApp.ViewModels
{
    public partial class SettingsViewModel : ViewModelGlobal
    {

        private readonly INavegacionService _navegacionService;

        [RelayCommand]
        async Task SalirSesion()
        {
            try
            {
                Preferences.Set("accesstoken", string.Empty);
                var uri = $"//{nameof(AboutPage)}";
                await _navegacionService.GoToAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en SettingsViewModel >> SalirSesion: " + ex.Message);
            }
        }

        public SettingsViewModel(INavegacionService navegacionService)
        {
            _navegacionService = navegacionService;
        }
    }

}