using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Maui.Controls;
using ApiClient;

namespace sicilylines
{
    public partial class pagereservation : ContentPage
    {
        public pagereservation(Client monClientAuthentifie)
        {
            InitializeComponent();
            ChargerReservations(monClientAuthentifie.Id_client);
        }

        private async void ChargerReservations(int clientId)
        {
            try
            {
                HttpClient client = new HttpClient();

                string urlReservations = $"http://localhost:5097/resaitems/{clientId}";
                string urlTraverses = "http://localhost:5097/traverseitems/";

                string jsonReservations = await client.GetStringAsync(urlReservations);
                string jsonTraverses = await client.GetStringAsync(urlTraverses);

                var reservations = JsonConvert.DeserializeObject<List<dynamic>>(jsonReservations);
                var traverses = JsonConvert.DeserializeObject<List<dynamic>>(jsonTraverses);

                List<dynamic> items = new List<dynamic>();

                foreach (var resa in reservations)
                {
                    var traverse = traverses.FirstOrDefault(t => (int)t.id_traverse == (int)resa.id_traverse);

                    if (traverse != null)
                    {
                        string dateStr = traverse.datetraverse.ToString();
                        DateTime date;

                        if (DateTime.TryParse(dateStr, out date))
                        {
                            string couleurFond = date < DateTime.Now ? "Grey" : "LightGreen";

                            items.Add(new
                            {
                                Id_reservation = resa.id_reservation,
                                Id_traverse = resa.id_traverse,
                                Id_liaison = traverse.id_liaison,
                                DateTraverse = date.ToString("dd/MM/yyyy"),
                                CouleurFond = couleurFond,
                                Libelle = (date < DateTime.Now)
                                    ? $"⛔ Traversée passée du {date:dd/MM/yyyy}"
                                    : $"⛵ Traversée prévue le {date:dd/MM/yyyy}"
                            });
                        }
                    }
                }

                if (items.Count == 0)
                {
                    messageAucuneReservation.IsVisible = true;
                    messageAucuneReservation.Text = "📭 Aucune réservation trouvée pour l'instant.";
                }
                else
                {
                    messageAucuneReservation.IsVisible = false;
                    listeReservations.ItemsSource = items;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("❌ Erreur", $"Une erreur est survenue : {ex.Message}", "OK");
            }
        }
    }
}
