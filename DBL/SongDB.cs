﻿using Models;
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
            S.ArtistID = int.Parse(row[2].ToString());
            S.SongFilePath = row[3].ToString();
            return S;
        }


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

        public async Task<int> DeleteAsync(Song song)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object> { { "songID", song.SongID } };
            return await base.DeleteAsync(filterValues);
        }

        public async Task<List<Song>> GetAllAsync()
        {
            return ((List<Song>)await SelectAllAsync());
        }
       
        public async Task<List<Song>> GetAllSongByArtisIDtAsync(int ArtistID)
        {
            string sql = @$"Select
                                songs.*
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


        public async Task<List<Song>> GetAllSongByGenreAsync(int userId)
        {
            string sql = @$"select
                                songs.songId,
                                songs.songName,
                                songs.artistId,
                                songs.songFilePath
                            from songs inner join artists
                                on songs.artistId = artists.artistId
                            where artists.genreId in
                            (select genreId from user_genre_preferences where userId = @userId);";
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("userId", userId);
            List<Song> list = await SelectAllAsync(sql, p);

            return list;
        }

        public async Task<List<Song>> GetAllSongsByFollowedArtists(int userId)
        {
            string sql = $@"select
                                songs.songId,
                                songs.songName,
                                songs.artistId,
                                songs.songFilePath
                            from songs inner join artists
                                on songs.artistId = artists.artistId
                            where artists.artistId in
                            (select artistID from user_artist_follows where userID = @userId);";

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("userId", userId);
            List<Song> list = await SelectAllAsync(sql, p);

            return list;
        }      

    }
}
