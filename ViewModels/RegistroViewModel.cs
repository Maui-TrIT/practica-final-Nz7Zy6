using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ShopApp.Models.Backend.Login;
using ShopApp.Models.Backend.Registro;
using ShopApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public partial class RegistroViewModel : ViewModelGlobal
    {
        private readonly IConnectivity _connectivity;
        private readonly RegistroService _registroService;
        private readonly INavegacionService _navegacionService;

        [ObservableProperty]
        private RegistrarRequest registro;

        [ObservableProperty]
        private string mensajeError;

        public RegistroViewModel(IConnectivity connectivity, RegistroService registroService, INavegacionService navegacionService)
        {
            try
            {
                _connectivity = connectivity;
                _registroService = registroService;
                _navegacionService = navegacionService;
                _connectivity.ConnectivityChanged += _connectivity_ConnectivityChanged;

                Registro = new RegistrarRequest();
                MensajeError = "";

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en RegistroViewModel.cs >> RegistroViewModel: " + ex.Message);
            }
        }

        private void _connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            RegistroMethodCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(StatusConnection))]
        private async Task RegistroMethod()
        {
            bool bTieneCaracteresEspeciales = false;

            try
            {
                if (IsBusy) { return; }
                IsBusy = true;

                if (string.IsNullOrEmpty(Registro.Nombre) || string.IsNullOrEmpty(Registro.Email) || string.IsNullOrEmpty(Registro.UserName) || string.IsNullOrEmpty(Registro.Password))
                {
                    await Shell.Current.DisplayAlert("Atención", "¡Revisa los campos obligatorios!", "Aceptar");
                    return;
                }

                for (int i = 0; i < Registro.Password.Length; i++)
                {
                    if (!char.IsLetterOrDigit(Registro.Password[i]) && !char.IsWhiteSpace(Registro.Password[i]))
                    {
                        bTieneCaracteresEspeciales = true;
                        break;
                    }
                }


                if (Registro.Password.Length < 12 || !Registro.Password.Any(char.IsUpper) || !Registro.Password.Any(char.IsLower) || !Registro.Password.Any(char.IsDigit) || !bTieneCaracteresEspeciales)
                {
                    await Shell.Current.DisplayAlert("Atención", "La password debe contener al menos 12 caracteres, una mayúscula, una minúscula, un dígito y algún caracter especial", "Aceptar");
                    return;
                }


                var resultado = await _registroService.Registro(Registro);
                if (resultado) Application.Current.MainPage = new AppShell();
                else await Shell.Current.DisplayAlert("Atención", "No se ha podido registrar el usuario, inténtalo de nuevo más tarde", "Aceptar");

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en RegistroViewModel.cs >> RegistroMethod: " + ex.Message);
            }
            finally { IsBusy = false; }
        }


        private bool StatusConnection()
        {
            return _connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
        }

        [RelayCommand]
        private async Task BackLoginMethod()
        {
            try
            {
                await _navegacionService.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en RegistroViewModel.cs >> BackLoginMethod: " + ex.Message);
            }

        }
    }
}
