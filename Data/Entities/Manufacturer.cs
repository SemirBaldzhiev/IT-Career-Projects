using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{

    //Here we create a manufacturer model for the Database.
    public class Manufacturer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; } = new HashSet<Game>();

    }
}
