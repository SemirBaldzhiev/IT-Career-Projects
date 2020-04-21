using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class GameRepository : CrudRepository<Game>
    {
        private readonly GameDbContext context;

        public GameRepository(GameDbContext dbContext) 
            : base(dbContext, dbContext.Games)
        {
            context = dbContext;
        }

        public IQueryable<Game> SearchByTittle(string title)
        {
            return context.Games.Where(g => g.Title.Contains(title));
        }
           
        public IQueryable<Game> SearchByManufacturer(string manufacturer)
        {
            return context.Games.Where(g => g.Manufacturer.Name.Contains(manufacturer));
        }
             
    }
}
