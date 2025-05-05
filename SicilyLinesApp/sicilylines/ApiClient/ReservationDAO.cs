using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ApiClient
{
    public class ReservationDAO
    {

        // attributs de connexion statiques
        private static string provider = "localhost";

        private static string dataBase = "slines";

        private static string uid = "root";

        private static string mdp = "";



        private static ConnexionSql maConnexionSql;


        private static MySqlCommand Ocom;


        // Récupération de la liste des employés
        public static List<Reservation> getReservation()
        {

            List<Reservation> lr = new List<Reservation>();

            try
            {
                // Pour se connecter à la base
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);

                // ouverture de la connexion
                maConnexionSql.openConnection();

                // exécution de la requete avec l'objer Command
                Ocom = maConnexionSql.reqExec("Select * from reservation");

                // lire les données ligne par ligne avec le reader

                MySqlDataReader reader = Ocom.ExecuteReader();

                Reservation u;



                // Tant qu'il existe une ligne dans la table
                while (reader.Read())
                {
                    // récupération de la ligne
                    int id_reservation = (int)reader.GetValue(0);
                    int id_liaison = (int)reader.GetValue(1);
                    int id_traverse = (int)reader.GetValue(2);
                    int id_client = (int)reader.GetValue(3);
                    string libelle = (string)reader.GetValue(4);
                    
                    //Instanciation d'un Client
                    u = new Reservation(id_reservation, id_liaison, id_traverse, id_client, libelle);

                    // Ajout de cet employe à la liste 
                    lr.Add(u);


                }


                // fermeture du reader
                reader.Close();

                //fermeture de la connexion
                maConnexionSql.closeConnection();

                // Envoi de la liste au Manager
                return (lr);


            }

            catch (Exception emp)
            {

                throw (emp);

            }


        }


        public static List<Reservation> trouveReservation(int clientId)
        {
            List<Reservation> lr = new List<Reservation>();

            try
            {
                // Initialisation de la connexion
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                // Préparation de la requête
                string req = "SELECT * FROM reservation WHERE id_client = @clientId";
                Ocom = maConnexionSql.reqExec(req);
                Ocom.Parameters.AddWithValue("@clientId", clientId);

                // Exécution de la requête et lecture des résultats
                using (MySqlDataReader reader = Ocom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_reservation = (int)reader.GetValue(0);
                        int id_liaison = (int)reader.GetValue(1);
                        int id_traverse = (int)reader.GetValue(2);
                        int id_client = (int)reader.GetValue(3);
                        string libelle = (string)reader.GetValue(4);

                        // Création de l'objet Reservation
                        Reservation r = new Reservation(id_reservation, id_liaison, id_traverse, id_client, libelle);

                        // Ajout à la liste
                        lr.Add(r);
                    }
                }

                // Fermeture de la connexion
                maConnexionSql.closeConnection();
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                throw;
            }

            return lr;
        }      

    }
}

