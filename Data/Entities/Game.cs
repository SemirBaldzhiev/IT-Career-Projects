using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    //Here we create a game model for the Database.
    public class Game
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public double Rating { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [MaxLength(50)]
        public string Platform { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        [NotMapped]
        public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();

        public ICollection<GameGenreConnection> GameGenres { get; set; } = new HashSet<GameGenreConnection>();

    }
}
