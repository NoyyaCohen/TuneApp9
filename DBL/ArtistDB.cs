using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class ArtistDB : BaseDB<Artist>
    {
        protected async override Task<List<Artist>> CreateListModelAsync(List<object[]> rows)
        {
            List<Artist> ArtistsList = new List<Artist>();
            foreach (object[] item in rows)
            {
                Artist a;
                a = (Artist)await CreateModelAsync(item);
                ArtistsList.Add(a);
            }
            return ArtistsList;
        }

        protected async override Task<Artist> CreateModelAsync(object[] row)
        {
            Artist A = new Artist();
            A.ArtistID = int.Parse(row[0].ToString());
            A.StageName = row[1].ToString();
            A.FollowersNumber = int.Parse(row[2].ToString());
            A.GenreID = int.Parse(row[3].ToString());
            if (row[4] != null)
            {
                A.ProfileImage = (byte[])row[4];
            }
            else
            {
                A.ProfileImage = new byte[0];
            }

            return A;
        }
        protected override string GetPrimaryKeyName()
        {
            return "artistID";
        }

        protected override string GetTableName()
        {
            return "artists";
        }

        public async Task<List<Artist>> selectAllAsync()
        {
            return await base.SelectAllAsync();
        }
        public async Task<Artist> GetArtistByID(int id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("artistID", id);
            //parameters["artistID"] = id;
            List<Artist> ArtistDTOs = await base.SelectAllAsync(parameters);
            if (ArtistDTOs != null && ArtistDTOs.Count == 1)
                return ArtistDTOs[0];
            else
                return null;
        }

        public async Task<Artist> InsertGetObjAsync(Artist artist)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "stageName", artist.StageName },
                {"followersNumber" , artist.FollowersNumber } ,
                {"genreID", artist.GenreID },
                {"profileImage" , artist.ProfileImage}
            };
            return (Artist)await base.InsertGetObjAsync(fillValues);
        }
        public async Task<List<Artist>> GetAllAsync()
        {
            return ((List<Artist>)await SelectAllAsync());
        }
        public async Task<int> DeleteAsync(Artist artist)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object> { { "artistID", artist.ArtistID } };
            return await base.DeleteAsync(filterValues);
        }
        public async Task<int> UpdateAsync(Artist artist)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>();
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            fillValues.Add("stageName", artist.StageName);
            fillValues.Add("followersNumber", artist.FollowersNumber);
            fillValues.Add("genreID", artist.GenreID);
            fillValues.Add ("profileImage" , artist.ProfileImage);

            return await base.UpdateAsync(fillValues, filterValues);
        }



    }
}
