using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ApiClient
{
    public class ClientDAO
    {

        // attributs de connexion statiques
        private static string provider = "localhost";

        private static string dataBase = "slines";

        private static string uid = "root";

        private static string mdp = "";



        private static ConnexionSql maConnexionSql;


        private static MySqlCommand Ocom;




        // Mise à jour d'un client

        public static void updateClient(Client u, string num, string adr)
        {
            try
            {
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();
                Ocom = maConnexionSql.reqExec("update client set tel = '" + num + "', adresse = '" + adr + "' WHERE ID_CLIENT = " + u.Id_client); int i = Ocom.ExecuteNonQuery();
                maConnexionSql.closeConnection();
            }
            catch (Exception emp)
            {
                throw (emp);
            }
        }




        // Récupération de la liste des clients
        public static List<Client> getClient()
        {

            List<Client> lc = new List<Client>();

            try
            {
                // Pour se connecter à la base
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);

                // ouverture de la connexion
                maConnexionSql.openConnection();

                // exécution de la requete avec l'objer Command
                Ocom = maConnexionSql.reqExec("Select * from Client");

                // lire les données ligne par ligne avec le reader

                MySqlDataReader reader = Ocom.ExecuteReader();

                Client u;



                // Tant qu'il existe une ligne dans la table
                while (reader.Read())
                {
                    // récupération de la ligne
                    int id_client = (int)reader.GetValue(0);
                    string nom = (string)reader.GetValue(1);
                    string prenom = (string)reader.GetValue(2);
                    string login = (string)reader.GetValue(3);
                    string password = (string)reader.GetValue(4);
                    string adresse = (string)reader.GetValue(5);
                    string cp = (string)reader.GetValue(6);
                    string tel = (string)reader.GetValue(7);

                    //Instanciation d'un Client
                    u = new Client(id_client, nom, prenom, login, password, adresse, cp, tel);

                    // Ajout de cet employe à la liste 
                    lc.Add(u);


                }


                // fermeture du reader
                reader.Close();

                //fermeture de la connexion
                maConnexionSql.closeConnection();

                // Envoi de la liste au Manager
                return (lc);


            }

            catch (Exception emp)
            {

                throw (emp);

            }


        }


        public static Client trouvePseudo(string Pseudo)
        {
            try
            {


                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);


                maConnexionSql.openConnection();

                string req = "Select * from Client where pseudo = @Pseudo";
                Ocom = maConnexionSql.reqExec(req);
                Ocom.Parameters.AddWithValue("@Pseudo", Pseudo);
                MySqlDataReader reader = Ocom.ExecuteReader();
        

                Client u = null;



                // Tant qu'il existe une ligne dans la table
                while (reader.Read())
                {
                    // récupération de la ligne
                    int id_client = (int)reader.GetValue(0);
                    string nom = (string)reader.GetValue(1);
                    string prenom = (string)reader.GetValue(2);
                    string pseudo = (string)reader.GetValue(3);
                    string password = (string)reader.GetValue(4);
                    string adresse = (string)reader.GetValue(5);
                    string cp = (string)reader.GetValue(6);
                    string tel = (string)reader.GetValue(7);

                    //Instanciation d'un Client
                    u = new Client(id_client, nom, prenom, pseudo, password, adresse, cp, tel);

                }

                // fermeture du reader
                reader.Close();

                //fermeture de la connexion
                maConnexionSql.closeConnection();

                // Envoi de l'employé au Manager
                return (u);
               

            }

            catch (Exception ex)
            {

                throw (ex);
                
            }

        }


    }
}

