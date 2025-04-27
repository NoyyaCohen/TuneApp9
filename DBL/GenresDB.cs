using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class GenresDB : BaseDB<Genres>
    {
        protected async override Task<List<Genres>> CreateListModelAsync(List<object[]> rows)
        {
            List<Genres> GenresList = new List<Genres>();
            foreach (object[] item in rows)
            {
                Genres G;
                G = (Genres)await CreateModelAsync(item);
                GenresList.Add(G);
            }
            return GenresList;

        }

        protected async override Task<Genres> CreateModelAsync(object[] row)
        {
           Genres G = new Genres();
           G.Genre = int.Parse(row[0].ToString());
           G.GenreName = row[1].ToString();
           return G;
        }

        protected override string GetPrimaryKeyName()
        {
            return "GenresID";
        }

        protected override string GetTableName()
        {
            return "genres";
        }

        public async Task<List<Genres>> selectAllAsync()
        {
            return await base.SelectAllAsync();
        }
  

    }
}
