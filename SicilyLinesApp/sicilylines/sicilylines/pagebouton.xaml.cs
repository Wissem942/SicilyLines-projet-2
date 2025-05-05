namespace sicilylines;

public partial class pagebouton : ContentPage
{
    Client monclientauthentifier;

    public pagebouton(Client clientauth)
    {
        InitializeComponent();
        monclientauthentifier = clientauth;
    }

    private async void BoutonProfil_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new pagecompte(monclientauthentifier));
    }

    private async void BoutonReservations_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new pagereservation(monclientauthentifier));
    }

   
        private async void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        // Effacez les informations de l'utilisateur connecté
        Client c = null;

        // Redirigez vers la page d'accueil
        await Navigation.PushAsync(new MainPage());

        // Optionnel : Affichez un message de confirmation
        await DisplayAlert("Déconnexion", "Vous avez été déconnecté.", "OK");
    }
}
