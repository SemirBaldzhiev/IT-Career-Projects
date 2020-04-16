using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingCatalog.Models
{
    public class PaginationViewModel
    {
        public int PagesCount { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }

        public PaginationViewModel()
        {
            Page = (Page <= 0) ? 1 : Page;
            ItemsPerPage = (ItemsPerPage <= 0) ? 12 : ItemsPerPage;
        }
    }
}
