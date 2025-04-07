using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Song
    {
        private int songID;
        private string songName;
        private byte[] songFile ;
        private int artistID;
        private int duration;

        public Song(int songID, string songName, byte[] songFile, int artistID, int duration)
        {
            this.SongID = songID;
            this.SongName = songName;
            this.SongFile = songFile;
            this.ArtistID = artistID;
            this.Duration = duration;
        }

        public Song() { }

        public int SongID { get => songID; set => songID = value; }
        public string SongName { get => songName; set => songName = value; }
        public byte[] SongFile { get => songFile; set => songFile = value; }
        public int ArtistID { get => artistID; set => artistID = value; }

        public int Duration { get => duration; set => duration = value; }
    }
}
