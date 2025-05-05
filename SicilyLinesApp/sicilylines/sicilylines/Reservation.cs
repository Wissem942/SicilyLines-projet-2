namespace sicilylines
{
    public class Reservation
    {
        private int id_reservation;
        private int id_liaison;
        private int id_traverse;
        private int id_client;
        private string libelle;

        public Reservation(int unId_reservation, int unId_liaison, int unId_traverse, int unId_client, string uneLibelle)
        {
            this.Id_reservation = unId_reservation;
            this.Id_liaison = unId_liaison;
            this.Id_traverse = unId_traverse;
            this.Id_client = unId_client;
            this.Libelle = uneLibelle;
        }

        public Reservation()
        {

        }

        public int Id_reservation { get => id_reservation; set => id_reservation = value; }
        public int Id_liaison { get => id_liaison; set => id_liaison = value; }
        public int Id_traverse { get => id_traverse; set => id_traverse = value; }
        public int Id_client { get => id_client; set => id_client = value; }
        public string Libelle { get => libelle; set => libelle = value; }
    }
}
