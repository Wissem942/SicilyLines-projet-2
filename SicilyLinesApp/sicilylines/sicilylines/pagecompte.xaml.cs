using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace sicilylines;

public partial class pagecompte : ContentPage
{
    Client c = null;
    public pagecompte(Client monclientauthentifier)
    {
        c = monclientauthentifier;

        InitializeComponent();
        NomEntry.Text = monclientauthentifier.Nom;
        PseudoEntry.Text = monclientauthentifier.Pseudo;
        TelEntry.Text = monclientauthentifier.Tel;
        PrenomEntry.Text = monclientauthentifier.Prenom;
        AdresseEntry.Text = monclientauthentifier.Adresse;
        CpEntry.Text = monclientauthentifier.Cp;

    }


    private async Task<bool> UpdateClientAsync(Client client, string newAddress, string newPhone)
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                client.Adresse = newAddress;
                client.Tel = newPhone;
                string url = $"http://localhost:5097/clientitems?num={newPhone}&adr={newAddress}";
                string jsonContent = JsonConvert.SerializeObject(client);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync(url, content);
                return response.IsSuccessStatusCode;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la mise à jour du client : {ex.Message}");
            return false;
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(AdresseEntry.Text) && !string.IsNullOrEmpty(TelEntry.Text))
            {
                bool success = await UpdateClientAsync(c, AdresseEntry.Text, TelEntry.Text);

                if (success)
                {
                    await DisplayAlert("Succès", "Vos informations ont été mises à jour", "OK");
                    // Mettre à jour l'objet client local
                    c.Adresse = AdresseEntry.Text;
                    c.Tel = TelEntry.Text;
                    c.Pseudo = PseudoEntry.Text;
                }
                else
                {
                    await DisplayAlert("Erreur", "Impossible de mettre à jour vos informations", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erreur", "Coordonnées manquantes", "Retour");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erreur", $"Une erreur s'est produite : {ex.Message}", "OK");
        }


    }
}


