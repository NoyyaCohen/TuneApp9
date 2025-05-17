using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserArtistFollow
    {
        public int UserID { get; set; }
        public int ArtistID { get; set; }

        // Default constructor
        public UserArtistFollow()
        {
        }

        // Constructor with parameters
        public UserArtistFollow(int userID, int artistID)
        {
            UserID = userID;
            ArtistID = artistID;
        }
    }
}