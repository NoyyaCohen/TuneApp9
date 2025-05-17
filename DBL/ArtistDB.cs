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
                A.FollowersNumber = int.Parse(row[2].ToString());
                A.GenreID = int.Parse(row[3].ToString());
                // null = invalid memory reference.
                // DBNull = invalid reference to DB entry. DBNull.Value != null (DBNull.Value isn't an invalid memory reference)
                if (row[4] != DBNull.Value)
                {
                    A.ProfileImage = (byte[])row[4];
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
                {"profileImage" , artist.ProfileImage},
                {"followersNumber", 0 }
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

        //public async Task<byte[]> ArtistPhoto(int id)
        //{
        //    Dictionary<string, object> p = new Dictionary<string, object>();
        //    p.Add("artistID", id);
        //    List<Artist> lst = (List<Artist>)await SelectAllAsync(p);
        //    if (lst.Count == 1 && lst[0].ProfileImage != null)
        //    {
        //        return lst[0].ProfileImage;
        //    }
        //    else
        //        return Convert.FromBase64String(
        //            "CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB3aWR0aD0iMjAwIiBoZWlnaHQ9IjIwMCIgdmlld0JveD0iMCAwIDIwMCAyMDAiPgogIDwhLS0gQmFja2dyb3VuZCBjaXJjbGUgLS0+CiAgPGNpcmNsZSBjeD0iMTAwIiBjeT0iMTAwIiByPSI5NSIgZmlsbD0iIzFFMUUxRSIgc3Ryb2tlPSIjRkY0NTAwIiBzdHJva2Utd2lkdGg9IjIiLz4KICAKICA8IS0tIEFydGlzdCBzaWxob3VldHRlIC0gc2ltcGxlIHBlcnNvbiB3aXRoIGhlYWRwaG9uZXMgLS0+CiAgPGcgZmlsbD0iI0ZGNDUwMCI+CiAgICA8IS0tIEhlYWQgLS0+CiAgICA8Y2lyY2xlIGN4PSIxMDAiIGN5PSI4MCIgcj0iMzUiLz4KICAgIAogICAgPCEtLSBIZWFkcGhvbmVzIC0tPgogICAgPHBhdGggZD0iTTYwIDgwIEM2MCA2MCwgNzAgNDUsIDEwMCA0NSBDMTMwIDQ1LCAxNDAgNjAsIDE0MCA4MCIgc3Ryb2tlPSIjMUUxRTFFIiBzdHJva2Utd2lkdGg9IjUiIGZpbGw9Im5vbmUiLz4KICAgIDxyZWN0IHg9IjU1IiB5PSI3NSIgd2lkdGg9IjEwIiBoZWlnaHQ9IjI1IiByeD0iNSIgcnk9IjUiLz4KICAgIDxyZWN0IHg9IjEzNSIgeT0iNzUiIHdpZHRoPSIxMCIgaGVpZ2h0PSIyNSIgcng9IjUiIHJ5PSI1Ii8+CiAgICAKICAgIDwhLS0gQm9keSAtLT4KICAgIDxwYXRoIGQ9Ik03MCAxMjAgQzcwIDEwNSwgODUgMTA1LCAxMDAgMTA1IEMxMTUgMTA1LCAxMzAgMTA1LCAxMzAgMTIwIEwxMzAgMTU1IEMxMzAgMTY1LCAxMTUgMTY1LCAxMDAgMTY1IEM4NSAxNjUsIDcwIDE2NSwgNzAgMTU1IFoiLz4KICA8L2c+Cjwvc3ZnPgo=");
        //}


    }
}
