
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShopApp.Services;
using ShopApp.Views;
using System.Diagnostics;

namespace ShopApp.ViewModels
{
    public partial class LoginViewModel : ViewModelGlobal
    {
        private readonly IConnectivity _connectivity;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        private readonly SecurityService _securityService;
        private readonly INavegacionService _navegacionService;

        public LoginViewModel(IConnectivity connectivity, SecurityService securityService, INavegacionService navegacionService)
        {
            _connectivity = connectivity;
            _securityService = securityService;
            _navegacionService = navegacionService;
            _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;
        }

        [RelayCommand(CanExecute = nameof(StatusConnection))]
        private async Task LoginMethod()
        {
            try
            {
                var resultado = await _securityService.Login(Email, Password);
                if (resultado) Application.Current.MainPage = new AppShell();
                else await Shell.Current.DisplayAlert("Mensaje", "Ingresó credenciales erróneas", "Aceptar");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en LoginViewModel.cs >> LoginMethod: " + ex.Message);
            }
        }

        [RelayCommand]
        private async Task RegistroMethod()
        {
            try
            {
                var uri = $"{nameof(RegistroPage)}";
                await _navegacionService.GoToAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en LoginViewModel.cs >> RegistroMethod: " + ex.Message);
            }

        }


        private void _connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            LoginMethodCommand.NotifyCanExecuteChanged();
        }



        private bool StatusConnection()
        {
            return _connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
        }

    }
}