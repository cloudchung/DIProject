using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using EntityFramework.Model;

namespace EntityFramework
{
    public class IndexModel : PageModel
    {
        private readonly EntityFramework.Data.EntityFrameworkContext _context;
        private readonly ILogger _logger;
        public IndexModel(EntityFramework.Data.EntityFrameworkContext context,ILogger<IndexModel> logger)
        {
            _context = context;
            _logger= logger;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Movie != null)
            {
                Movie = await _context.Movie.ToListAsync();
            }
        }
    }
}
