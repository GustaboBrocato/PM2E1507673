namespace PM2E1507673
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
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
                    var location = await Geolocation.GetLastKnownLocationAsync();

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
                        await DisplayGpsNotEnabledAlert();
                    }
                }
                else
                {
                    // Handle case where location permission is not granted
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
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

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {

        }

        private void btnListaSitios_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {

        }
    }

}
