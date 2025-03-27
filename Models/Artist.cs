using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Artist : Listener
    {
        private int artistID;   
        private string stageName;
        private int followersNumber;
        private int genreID;
        private byte[] profileImage;

        public Artist() { }

        public Artist(int artistID, string stageName, int followersNumber, int genreID, byte[] profileImage)
        {
            ArtistID = artistID;
            StageName = stageName;
            FollowersNumber = followersNumber;
            GenreID = genreID;
            ProfileImage = profileImage;

        }

        public int ArtistID { get => artistID; set => artistID = value; }
        public string StageName { get => StageName; set => StageName = value; }
        public int FollowersNumber { get => followersNumber; set => followersNumber = value; }
        public int GenreID { get => genreID; set => genreID = value; }
        public byte[] ProfileImage { get => profileImage; set => profileImage = value; }

    }
}
