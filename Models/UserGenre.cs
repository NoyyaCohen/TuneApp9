using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserGenre
    {
        private int userID;
        private int genreID;
        private int user_genre_preferencesID;

        
        public UserGenre(int userID, int genreID, int user_genre_preferencesID)
        {
            this.UserID = userID;
            this.GenreID = genreID;
            this.User_genre_preferencesID = user_genre_preferencesID;
        }
        public UserGenre() { }
        public int UserID { get => userID; set => userID = value; }
        public int GenreID { get => genreID; set => genreID = value; }
        public int User_genre_preferencesID { get => user_genre_preferencesID; set => user_genre_preferencesID = value; }
    }
}
