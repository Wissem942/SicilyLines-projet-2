namespace ApiClient
{
    public class Client
    {
        private int id_client;
        private string nom;
        private string prenom;
        private string pseudo;
        private string password;
        private string adresse;
        private string cp;
        private string tel;





        // getter & setter
        public int Id_client { get => id_client; set => id_client = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Pseudo { get => pseudo; set => pseudo = value; }
        public string Password { get => password; set => password = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Cp { get => cp; set => cp = value; }
        public string Tel { get => tel; set => tel = value; }


        // Constructeur de la classe Client
        public Client(int unId_client, string unNom, string unPrenom, string unPseudo, string unPassword, string uneAdresse, string unCp, string unTel)
        {
            this.id_client = unId_client;
            this.nom = unNom;
            this.prenom = unPrenom;
            this.pseudo = unPseudo;
            this.password = unPassword;
            this.adresse = uneAdresse;
            this.cp = unCp;
            this.tel = unTel;
        }

        // Constructeur vide


        public Client()
        {
 
        }

      
        
    }
}
