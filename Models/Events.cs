namespace Models
{
    public class Events
    {
        private int eventID;
        private DateTime date;
        private string artistName;
        private string place;
        private byte[] image;

        public Events() { }

        public Events(int eventID, DateTime date, string artistName, string place, byte[] image)
        {
            this.EventID = eventID;
            this.Date = date;
            this.ArtistName = artistName;
            this.Place = place;
            this.Image = image;
        }

        public int EventID { get => eventID; set => eventID = value; }
        public DateTime Date { get => date; set => date = value; }
        public string ArtistName { get => artistName; set => artistName = value; }
        public string Place { get => place; set => place = value; }
        public byte[] Image { get => image; set => image = value; }
    }
}
