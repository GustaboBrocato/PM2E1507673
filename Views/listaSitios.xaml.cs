using Microsoft.Maui.Controls;

namespace PM2E1507673.Views;

public partial class listaSitios : ContentPage
{
    private Controllers.DBSitioMaps controller;
    private List<Models.sitioMaps> sitios;

    public listaSitios()
	{
		InitializeComponent();
        controller = new Controllers.DBSitioMaps();
    }

    //Metodo que permite mostrar la lista mientras la pagina se esta mostrando o cargando
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Obtiene la lista de personas de la base de datos
        sitios = await controller.getListSitio();

        // Coloca la lista en el collection view
        listUbicacion.ItemsSource = sitios;
    }

    private void btnSalir_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnVerMaps_Clicked(object sender, EventArgs e)
    {

    }
}