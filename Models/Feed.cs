using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Models
{
    public class Feed : IComparable<Feed>
    {
        private int postID;
        private string title;
        private string message;
        private int artistPosterID;
        private DateTime postDate;

        public Feed(int postID, string title, string message, int artistPosterID, DateTime postDate)
        {
            this.PostID = postID;
            this.Title = title;
            this.Message = message;
            this.ArtistPosterID = artistPosterID;
            this.PostDate = postDate;
        }
        public Feed() { } 

        public int PostID { get => postID; set => postID = value; }
        public string Title { get => title; set => title = value; }
        public string Message { get => message; set => message = value; }
        public int ArtistPosterID { get => artistPosterID; set => artistPosterID = value; }
        public DateTime PostDate { get => postDate; set => postDate = value; }

        public int CompareTo(Feed other)
        {
            return this.postDate.CompareTo(other.postDate);
        }
    }
}
