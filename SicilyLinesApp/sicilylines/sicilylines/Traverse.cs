namespace ApiClient
{
    public class Traverse
    {
        private int id_liaison;
        private int id_traverse;
        private int id_bateau;
        private string datetraverse;
        private string heure;

        public Traverse(int id_liaison, int id_traverse, int id_bateau, string datetraverse, string heure)
        {
            this.Id_liaison = id_liaison;
            this.Id_traverse = id_traverse;
            this.Id_bateau = id_bateau;
            this.Datetraverse = datetraverse;
            this.Heure = heure;
        }

        public int Id_liaison { get => id_liaison; set => id_liaison = value; }
        public int Id_traverse { get => id_traverse; set => id_traverse = value; }
        public int Id_bateau { get => id_bateau; set => id_bateau = value; }
        public string Datetraverse { get => datetraverse; set => datetraverse = value; }
        public string Heure { get => heure; set => heure = value; }
    }
}
