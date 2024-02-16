using PM2E1507673.Extensions;

namespace PM2E1507673.Views;

public partial class editSitio : ContentPage
{
    private Models.sitioMaps SelectedSitio { get; }
    public editSitio(Models.sitioMaps selectedSitio)
	{
		InitializeComponent();
        SelectedSitio = selectedSitio;
        byte[] imageData = PhotoHelper.GetImageArrayFromBase64(SelectedSitio.imagen);

        imgSitio.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(imageData));
        labelLatitude.Text = (SelectedSitio.latitud).ToString();
        labelLongitude.Text = (SelectedSitio.longitud).ToString();
        entryDescripcion.Text = SelectedSitio.descripcion;
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnRegresar_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}