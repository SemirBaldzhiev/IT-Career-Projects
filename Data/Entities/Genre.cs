using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{

    //Here we create a genre model for the Database.
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GameGenreConnection> GameGenres { get; set; } = new HashSet<GameGenreConnection>();
    }
}
