using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class ManufacturerRepository : CrudRepository<Manufacturer>
    {
        public ManufacturerRepository(GameDbContext dbContext) 
            : base(dbContext, dbContext.Manufacturers)
        {
        }
    }
}
