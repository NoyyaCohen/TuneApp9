using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class FeedDB : BaseDB<Feed>
    {
        protected async override Task<List<Feed>> CreateListModelAsync(List<object[]> rows)
        {
            List<Feed> FeedsList = new List<Feed>();
            foreach (object[] item in rows)
            {
                Feed F;
                F = (Feed)await CreateModelAsync(item);
                FeedsList.Add(F);
            }
            //FeedsList.Sort();
            return FeedsList;
        }

        protected async override Task<Feed> CreateModelAsync(object[] row)
        {
            Feed F = new Feed();
            F.PostID = int.Parse(row[0].ToString());
            F.Title = row[1].ToString();
            F.Message = row[2].ToString();
            F.ArtistPosterID = int.Parse(row[3].ToString());
            F.PostDate = DateTime.Parse(row[4].ToString());
            return F;
        }
        protected override string GetPrimaryKeyName()
        {
            return "postId";
        }

        protected override string GetTableName()
        {
            return "artists_posts";
        }

        public async Task<List<Feed>> selectAllAsync()
        {
            return await base.SelectAllAsync();
        }
        public async Task<Feed> GetListenerByID(long id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["postId"] = id;
            List<Feed> ArtistDTOs = await base.SelectAllAsync(parameters);
            if (ArtistDTOs != null && ArtistDTOs.Count == 1)
                return ArtistDTOs[0];
            else
                return null;
        }

        public async Task<List<Feed>> GetFeedByFollowedArtistsAsync(int userId)
        {
            string sql = $@"select
                     artists_posts.PostId,
                     artists_posts.Title,
                     artists_posts.Message,
                     artists_posts.ArtistPosterID,
                     artists_posts.PostDate
                 from artists_posts inner join artists
                     on artists_posts.ArtistPosterID = artists.artistId
                 where artists.artistId in
                 (select artistID from user_artist_follows where userID = @userId)
                 order by artists_posts.PostDate desc";

            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("userId", userId);
            List<Feed> list = await SelectAllAsync(sql, p);
            return list;
        }

        public async Task<Feed> InsertGetObjAsync(Feed feed)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                {"title", feed.Title } ,
                { "message", feed.Message },
                {"artistPosterID", feed.ArtistPosterID },
                {"postDate" , feed.PostDate}
            };
            return (Feed)await base.InsertGetObjAsync(fillValues);
        }
        public async Task<List<Feed>> GetAllAsync()
        {
            return ((List<Feed>)await SelectAllAsync());
        }
        public async Task<int> DeleteAsync(Feed feed)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object> { { "postId", feed.PostID } };
            return await base.DeleteAsync(filterValues);
        }
    }
}

