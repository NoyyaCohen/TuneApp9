using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Events
    {
        private int eventID;
        private DateTime date;
        private string artistName;
        private string place;
        private string image;

        public Events() { }

        public Events(int eventID, DateTime date, string artistName, string place, string image)
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
        public string Image { get => image; set => image = value; }
    }
}

