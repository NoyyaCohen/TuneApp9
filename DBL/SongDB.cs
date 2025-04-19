using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class SongDB : BaseDB<Song>
    {
        protected async override Task<List<Song>> CreateListModelAsync(List<object[]> rows)
        {
            List<Song> songsList = new List<Song>();
            foreach (object[] item in rows)
            {
                Song c;
                c = (Song)await CreateModelAsync(item);
                songsList.Add(c);
            }
            return songsList;

        }

        protected async override Task<Song> CreateModelAsync(object[] row)
        {
            Song S = new Song();
            S.SongID = int.Parse(row[0].ToString());
            S.SongName = row[1].ToString();
            //if (row[2] != null)
            //{
            //    S.SongFile = (byte[])row[2];
            //}
            //else
            //{
            //    S.SongFile = new byte[0];
            //}
            S.ArtistID = int.Parse(row[2].ToString());
            S.SongFilePath = row[3].ToString();
            return S;
        }

        //public async Task<Song> SelectByPkAsync(int id)
        //{
        //    Dictionary<string, object> filters = new Dictionary<string, object>()
        //    {
        //        { "songID", id}
        //    }; 
        //    List<Song> Song1 = await base.SelectAllAsync();
        //    return Song1.FirstOrDefault();
        //}

        protected override string GetPrimaryKeyName()
        {
            return "songID";
        }

        protected override string GetTableName()
        {
            return "songs";
        }

        public async Task<List<Song>> selectAllAsync()
        {
            return await base.SelectAllAsync();
        }

        public async Task<Song> GetSongByID(long id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["songID"] = id;
            List<Song> songDTOs = await base.SelectAllAsync(parameters);
            if (songDTOs != null && songDTOs.Count == 1)
                return songDTOs[0];
            else
                return null;
        }

        public async Task<Song> InsertGetObjAsync(Song song)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "songName", song.SongName },
                { "songFilePath", song.SongFilePath } ,
                { "artistID", song.ArtistID }
            };
            return (Song)await base.InsertGetObjAsync(fillValues);
        }

        //public async Task<int> UpdateAsync(Song song)
        //{
        //    Dictionary<string, object> fillValues = new Dictionary<string, object>();
        //    Dictionary<string, object> filterValues = new Dictionary<string, object>();
        //    fillValues.Add("Name", song.SongName);
        //    fillValues.Add("File", song.SongFile);
        //    fillValues.Add("ArtistID", song.SongID);
        //    filterValues.Add("songID", song.SongID.ToString());
        //    return await base.UpdateAsync(fillValues, filterValues);
        //}


        public async Task<int> DeleteAsync(Song song)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object> { { "songID", song.SongID } };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<List<Song>> GetAllAsync()
        {
            return ((List<Song>)await SelectAllAsync());
        }

        public async Task<List<Song>> GetSongByArtistAsync(int ArtistID)
        {
            string sql = @$"SELECT * FROM tuneapp.songs";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("artistID", ArtistID.ToString());
            List<Song> list = (List<Song>)await SelectAllAsync(sql, p);

            return list;

        }


        public async Task<List<Song>> GetAllSongByArtisIDtAsync(int ArtistID)
        {
            string sql = @$"Select
                                songs.songId,
                                songs.songName,
                                songs.artistId
                            From
                                artists Inner Join
                                songs On songs.artistId = artists.artistId
                            Where
                                artists.artistId = @artistID;";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("artistID", ArtistID.ToString());
            List<Song> list = await SelectAllAsync(sql, p);

            return list;

        }

        public async Task<List<Song>> GetSongByGenre(int userID)
        {
            string sql = $@"Select
                            songs.*


                        From
                            songs Inner Join
                            artists On songs.artistId = artists.artistId
                        Where
                            artists.genreId IN (Select
                            user_genre_preferences.genreId
                        From
                            user_genre_preferences
                        Where
                            user_genre_preferences.userId = @p)";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("p", userID);
            List<Song> list = (List<Song>)await SelectAllAsync(sql, p);

            return list;


        }

    }
}
