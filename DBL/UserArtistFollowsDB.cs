using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class UserArtistFollowsDB : BaseDB<UserArtistFollow>
    {
        protected async override Task<List<UserArtistFollow>> CreateListModelAsync(List<object[]> rows)
        {
            List<UserArtistFollow> followList = new List<UserArtistFollow>();
            foreach (object[] item in rows)
            {
                UserArtistFollow follow;
                follow = (UserArtistFollow)await CreateModelAsync(item);
                followList.Add(follow);
            }
            return followList;
        }

        protected async override Task<UserArtistFollow> CreateModelAsync(object[] row)
        {
            UserArtistFollow follow = new UserArtistFollow();
            follow.UserID = int.Parse(row[0].ToString());
            follow.ArtistID = int.Parse(row[1].ToString());
            return follow;
        }

        protected override string GetPrimaryKeyName()
        {
            return "id";
        }

        protected override string GetTableName()
        {
            return "user_artist_follows";
        }

        // Insert a new follow relationship
        public async Task<bool> InsertFollowAsync(int userID, int artistID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"userID", userID },
                {"artistID", artistID }
            };

            int result = await base.InsertAsync(parameters);
            return result > 0;
        }

        // Delete a follow relationship
        public async Task<int> DeleteFollowAsync(int userID, int artistID)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object> {                 
                {"userID", userID },
                {"artistID", artistID } 
            };
            return await base.DeleteAsync(filterValues);
        }


        // Get all users following a specific artist
        public async Task<List<int>> GetArtistsIdsByUsersIdAsync(int userID)
        {
            string sql = @"SELECT * FROM user_artist_follows WHERE userID = @userID";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"userID", userID.ToString() }
            };

            List<UserArtistFollow> list = (List<UserArtistFollow>)await SelectAllAsync(sql, parameters);

            List<int> lst = new List<int>();
            foreach (var v in list)
                lst.Add(v.ArtistID);

            return lst;
        }
    }
}