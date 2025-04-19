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
        private int artistID;
        private string songFilePath;
        

        public Song( string songName, int artistID, string songFilePath)
        {
            this.SongName = songName;
            this.ArtistID = artistID;
            this.SongFilePath = songFilePath;
            
        }

        public Song() { }

        public int SongID { get => songID; set => songID = value; }
        public string SongName { get => songName; set => songName = value; }
        public int ArtistID { get => artistID; set => artistID = value; }
        public string SongFilePath { get => songFilePath; set => songFilePath = value; }

        
    }
}
