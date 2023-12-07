using ShopApp.DataAccess;
using ShopApp.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Handlers
{
    public class ProductoBusquedaHandler : SearchHandler
    {

        ShopDbContext dbContext;

        public ProductoBusquedaHandler()
        {
            this.dbContext = new ShopDbContext();

        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newValue))
                {
                    ItemsSource = null;
                    return;
                }

                var resultados = dbContext.Products.Where(p => p.Nombre.ToLowerInvariant().Contains(newValue.ToLowerInvariant())).ToList();
                ItemsSource = resultados;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en ProductoBusquedaHandler.cs >> OnQueryChanged: " + ex.Message);
            }

        }



        protected async override void OnItemSelected(object item)
        {
            try
            {
                var product = item as Product;
                var uri = $"{nameof(ProductDetailPage)}?id={product.Id}";
                Shell.Current.CurrentItem = Shell.Current.CurrentItem.Items[0];
                await Shell.Current.GoToAsync(uri, false);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción en ProductoBusquedaHandler.cs >> OnItemSelected: " + ex.Message);
            }
}
    }
}
