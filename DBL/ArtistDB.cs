using Models;

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
            try
            {
                A.ArtistID = int.Parse(row[0].ToString());
                A.StageName = row[1].ToString();
                A.GenreID = int.Parse(row[2].ToString());
                // null = invalid memory reference.
                // DBNull = invalid reference to DB entry. DBNull.Value != null (DBNull.Value isn't an invalid memory reference)
                if (row[3] != DBNull.Value)
                {
                    A.ProfileImage = (byte[])row[3];
                }
                else
                {
                    A.ProfileImage = Array.Empty<byte>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return A;
        }
        protected override string GetPrimaryKeyName()
        {
            return "artistId";
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
            parameters.Add("artistId", id);
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
                {"artistId", artist.ArtistID },
                {"stageName", artist.StageName },
                {"genreId", artist.GenreID },
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
            fillValues.Add("genreID", artist.GenreID);
            fillValues.Add ("profileImage" , artist.ProfileImage);

            return await base.UpdateAsync(fillValues, filterValues);
        }

        public async Task<Artist> GetArtistBySongAsync(int ArtistID)
        {
            string sql = @$"SELECT * FROM tuneapp.artusts";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("artistID", ArtistID.ToString());
            List<Artist> list = (List<Artist>)await SelectAllAsync(sql, p);
            Artist artist = null;
            foreach(var a in list)
            {
                artist = a;
            }
            return artist;
        }

    }
}
