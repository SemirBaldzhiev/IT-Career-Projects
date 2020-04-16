using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class GenreRepository : CrudRepository<Genre>
    {
        public GenreRepository(GameDbContext dbContext) 
            : base(dbContext, dbContext.Genres)
        {
        }
    }
}
