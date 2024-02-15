

namespace PM2E1507673
{
    public partial class MainPage : ContentPage
    {
        Controllers.DBSitioMaps controller;
        FileResult photo;

        public MainPage()
        {
            InitializeComponent();
            controller = new Controllers.DBSitioMaps();
            InitializePage();
        }

        public MainPage(Controllers.DBSitioMaps dbPath)
        {
            InitializeComponent();
            controller = dbPath;
            InitializePage();
        }

        private async void InitializePage()
        {
            try
            {
                // Check if location permission is granted
                var locationPermissionStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (locationPermissionStatus == PermissionStatus.Granted)
                {
                    // Get the last known location
                    var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Default,
                        Timeout = TimeSpan.FromSeconds(10) // Specify a timeout if needed
                    });

                    if (location != null)
                    {
                        // Update the labels with latitude and longitude
                        labelLatitude.Text = $"{location.Latitude}";
                        labelLongitude.Text = $"{location.Longitude}";
                    }
                    else
                    {
                        // Handle case where location is null
                        // (Could not determine the location)
                        
                        await DisplayAlert("Alerta", "El GPS se encuentra desactivado, por favor activar el GPS!", "Ok");
                        await DisplayGpsNotEnabledAlert();
                    }
                }
                else
                {
                    // Handle case where location permission is not granted
                }
            }
            catch (FeatureNotEnabledException)
            {
                // Handle exceptions
                await DisplayAlert("Alerta", "El GPS se encuentra desactivado, por favor activar el GPS!", "Ok");
                
            }
       
        }

        private async Task DisplayGpsNotEnabledAlert()
        {
            var result = await DisplayAlert("GPS Not Enabled", "Please enable GPS to use this app.", "Go to Settings", "Cancel");
            

            if (result)
            {
                // Open the device settings to enable GPS
                await Launcher.OpenAsync(new Uri("ms-settings:privacy-location"));
            }
        }

        public string? GetImg64()
        {
            if (photo != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream stream = photo.OpenReadAsync().Result;
                    stream.CopyTo(ms);
                    byte[] data = ms.ToArray();

                    String Base64 = Convert.ToBase64String(data);

                    return Base64;
                }
            }
            return null;
        }



        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            string latitud = labelLatitude.Text;
            string longitud = labelLongitude.Text;
            string descripcion = entryDescripcion.Text;

            if(photo != null)
            {
                if(string.IsNullOrEmpty(latitud) || string.IsNullOrEmpty(longitud))
                {
                    await DisplayAlert("Error", "No hay datos de longitud y latitud", "OK");
                    return;
                }
                else if (string.IsNullOrEmpty(descripcion))
                {
                        await DisplayAlert("Error", "Porfavor ingrese una descripción", "OK");
                        return;
                }
            }else
            {
                await DisplayAlert("Error", "Porfavor tome una fotografía", "OK");
                return;
            }

            var sitio = new Models.sitioMaps
            {
                latitud = double.Parse(labelLatitude.Text),
                longitud = double.Parse(labelLongitude.Text),
                descripcion = entryDescripcion.Text,
                imagen = GetImg64()
            };

            try
            {
                if (controller != null)
                {
                    if (await controller.InsertMapaSitio(sitio) > 0)
                    {
                        await DisplayAlert("Aviso", "Registro Ingresado con Exito!", "OK");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Ocurrio un Error", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrio un Error: {ex.Message}", "OK");
            }


        }

        private void btnListaSitios_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.listaSitios());
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnTomarFoto_Clicked(object sender, EventArgs e)
        {
            photo = await MediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                string photoPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using Stream sourcephoto = await photo.OpenReadAsync();
                using FileStream streamlocal = File.OpenWrite(photoPath);

                imgSitio.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result); //Para verla dentro de archivo

                await sourcephoto.CopyToAsync(streamlocal); //Para Guardarla local
            }
        }
    }

}
