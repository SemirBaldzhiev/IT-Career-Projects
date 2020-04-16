using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class GameRepository : CrudRepository<Game>
    {
        public GameRepository(GameDbContext dbContext) 
            : base(dbContext, dbContext.Games)
        {
        }
    }
}
