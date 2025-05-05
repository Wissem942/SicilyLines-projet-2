using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ApiClient
{
    public class TraverseDAO
    {

        // attributs de connexion statiques
        private static string provider = "localhost";

        private static string dataBase = "slines";

        private static string uid = "root";

        private static string mdp = "";



        private static ConnexionSql maConnexionSql;


        private static MySqlCommand Ocom;


        // Récupération de la liste des employés
        public static List<Traverse> getTraverse()
        {

            List<Traverse> lt = new List<Traverse>();

            try
            {
                // Pour se connecter à la base
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);

                // ouverture de la connexion
                maConnexionSql.openConnection();

                // exécution de la requete avec l'objer Command
                Ocom = maConnexionSql.reqExec("Select * from traverse");

                // lire les données ligne par ligne avec le reader

                MySqlDataReader reader = Ocom.ExecuteReader();

                Traverse u;



                // Tant qu'il existe une ligne dans la table
                while (reader.Read())
                {
                    // récupération de la ligne
                    int id_liaison = (int)reader.GetValue(0);
                    int id_traverse = (int)reader.GetValue(1);
                    int id_bateau = (int)reader.GetValue(2);
                    string datetraverse = (string)reader.GetValue(3);
                    string heure = (string)reader.GetValue(4);
                    
                    //Instanciation d'une Traverse
                    u = new Traverse(id_liaison, id_traverse, id_bateau, datetraverse, heure);

                    // Ajout de cet traverse à la liste 
                    lt.Add(u);


                }


                // fermeture du reader
                reader.Close();

                //fermeture de la connexion
                maConnexionSql.closeConnection();

                // Envoi de la liste au Manager
                return (lt);


            }

            catch (Exception emp)
            {

                throw (emp);

            }


        }


        public static List<Traverse> trouveTraverse()
        {
            List<Traverse> lt = new List<Traverse>();

            try
            {
                // Initialisation de la connexion
                maConnexionSql = ConnexionSql.getInstance(provider, dataBase, uid, mdp);
                maConnexionSql.openConnection();

                // Préparation de la requête
                string req = "SELECT * FROM traverse";
                Ocom = maConnexionSql.reqExec(req);

                // Exécution de la requête et lecture des résultats
                using (MySqlDataReader reader = Ocom.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // récupération de la ligne
                        int id_liaison = (int)reader.GetValue(0);
                        int id_traverse = (int)reader.GetValue(1);
                        int id_bateau = (int)reader.GetValue(2);
                        string datetraverse = (string)reader.GetValue(3);
                        string heure = (string)reader.GetValue(4);

                        // Création de l'objet Reservation
                        Traverse t = new Traverse(id_liaison, id_traverse, id_bateau, datetraverse, heure);

                        // Ajout à la liste
                        lt.Add(t);
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

            return lt;
        }      

    }
}

