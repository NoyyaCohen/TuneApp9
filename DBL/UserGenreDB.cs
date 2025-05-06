using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class UserGenreDB : BaseDB<UserGenre>
    {
        protected async override Task<List<UserGenre>> CreateListModelAsync(List<object[]> rows)
        {
            List<UserGenre> GenresList = new List<UserGenre>();
            foreach (object[] item in rows)
            {
                UserGenre G;
                G = (UserGenre)await CreateModelAsync(item);
                GenresList.Add(G);
            }
            return GenresList;
        }

        protected async override Task<UserGenre> CreateModelAsync(object[] row)
        {
            UserGenre G = new UserGenre();
            G.UserID = int.Parse(row[0].ToString());
            G.GenreID = int.Parse(row[0].ToString());
            G.User_genre_preferencesID = int.Parse(row[0].ToString());
            return G;
        }

        protected override string GetPrimaryKeyName()
        {
            return "user_genre_preferencesID";
        }

        protected override string GetTableName()
        {
            return "user_genre_preferences";
        }

        public async Task<List<UserGenre>> selectAllAsync()
        {
            return await base.SelectAllAsync();
        }

        public async Task<UserGenre> InsertGetObjAsync(int idu , int idg)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "userID", idu },
                { "genreID", idg }
                
            };
            return (UserGenre)await base.InsertGetObjAsync(fillValues);
        }

        public async Task<int> DeleteAsync(UserGenre userGenre)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object> { { "userId", userGenre.UserID }, { "genreId", userGenre.GenreID } };
            return await base.DeleteAsync(filterValues);
        }
    }
}
