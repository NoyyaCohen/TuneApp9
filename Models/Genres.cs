using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Genres
    {
        private int genre;
        private string genreName;

        public Genres(int genre, string genreName)
        {
            this.Genre = genre;
            this.GenreName = genreName;
        }

        public Genres() { }

        public int Genre { get => genre; set => genre = value; }
        public string GenreName { get => genreName; set => genreName = value; }
    }
}
