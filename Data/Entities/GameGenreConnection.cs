using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    //Here is the connection between game and genre.
    public class GameGenreConnection
    {
        public int GameId { get; set; }
        public Game Game { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }


    }
}
