using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using JsonException = Newtonsoft.Json.JsonException;
using System.Net.Http;
using System.Threading.Tasks;

namespace sicilylines
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        public bool authentifier()
        {

            using (WebClient web = new WebClient())
            {
                bool res = false;

                try
                {

                    string url = string.Format("http://localhost:5097/clientitems/");

                    var json = web.DownloadString(url);

                    var jsonData = JsonConvert.DeserializeObject<dynamic>(json);


                    foreach (var data in jsonData)
                    {
                        var login = data.pseudo;
                        var mdp = data.password;

                        if (login == PseudoEntry.Text && mdp == PasswordEntry.Text)
                        {

                            res = true;
                        }

                    }
                    return (res);

                }

                catch (JsonException execption)
                {

                    execption.GetBaseException();
                }

                return (res);

            }


        }

     

        public async Task<Client> ChercherPseudoAsync()
        {
            Client c = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "http://localhost:5097/clientitems/";
                    string json = await client.GetStringAsync(url);
                    var jsonData = JsonConvert.DeserializeObject<dynamic>(json);

                    // Supposons que vous voulez trouver le client correspondant au pseudo entré
                    string pseudoRecherche = PseudoEntry.Text;

                    foreach (var data in jsonData)
                    {
                        var pseudo = data.pseudo.ToString();

                        if (pseudo == pseudoRecherche)
                        {
                            var num = (int)data.id_client;
                            var nom = data.nom.ToString();
                            var prenom = data.prenom.ToString();
                            var mdp = data.password.ToString();
                            var adr = data.adresse.ToString();
                            var cp = data.cp.ToString();
                            var tel = data.tel.ToString();

                            c = new Client(num, nom, prenom, pseudo, mdp, adr, cp, tel);
                            break; // On a trouvé le bon client, on sort de la boucle
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log l'exception ou affichez-la
                Console.WriteLine($"Erreur lors de la recherche du client : {ex.Message}");
                // Vous pourriez aussi afficher une alerte ici
            }

            return c;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (authentifier())
                {

                    Client leclient = await ChercherPseudoAsync();


                    if (leclient != null)
                    {
                        await Navigation.PushAsync(new pagebouton(leclient));
                    }
                    else
                    {
                        await DisplayAlert("Erreur", "Problème de connexion a la BDD ou autre", "OK");
                    }

                }


                else
                {
                    await DisplayAlert("Erreur", "Login ou mot de passe incorrect", "OK");


                }

            }
            catch (JsonException execption)
            {

                execption.GetBaseException();
            }

        }
    }
}