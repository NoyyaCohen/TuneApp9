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
            return "postID";
        }

        protected override string GetTableName()
        {
            return "feed_upload";
        }

        public async Task<List<Feed>> selectAllAsync()
        {
            return await base.SelectAllAsync();
        }
        public async Task<Feed> GetListenerByID(long id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["messageID"] = id;
            List<Feed> ArtistDTOs = await base.SelectAllAsync(parameters);
            if (ArtistDTOs != null && ArtistDTOs.Count == 1)
                return ArtistDTOs[0];
            else
                return null;
        }

        public async Task<Feed> InsertGetObjAsync(Feed feed, string password)
        {
            Dictionary<string, object> fillValues = new Dictionary<string, object>()
            {
                { "title", feed.Title } ,
                { "message", feed.Message },
                {"artistFeedID", feed.ArtistPosterID }
            };
            return (Feed)await base.InsertGetObjAsync(fillValues);
        }
        public async Task<List<Feed>> GetAllAsync()
        {
            return ((List<Feed>)await SelectAllAsync());
        }
        public async Task<int> DeleteAsync(Feed feed)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object> { { "messageID", feed.PostID } };
            return await base.DeleteAsync(filterValues);
        }
        //public async Task<int> UpdateAsync(Listener listener)
        //{
        //    Dictionary<string, object> fillValues = new Dictionary<string, object>();
        //    Dictionary<string, object> filterValues = new Dictionary<string, object>();
        //    fillValues.Add("fullName", listener.FullName);
        //    fillValues.Add("userName", listener.UserName);
        //    fillValues.Add("emailAdress", listener.EmailAdress);
        //    filterValues.Add("phoneNumber", listener.PhoneNumber);
        //    return await base.UpdateAsync(fillValues, filterValues);
        //}
    }
}

