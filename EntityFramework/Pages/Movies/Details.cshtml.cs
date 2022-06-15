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
    public class DetailsModel : PageModel
    {
        private readonly EntityFramework.Data.EntityFrameworkContext _context;
        private readonly ILogger _logger;

        public DetailsModel(EntityFramework.Data.EntityFrameworkContext context,ILogger<DetailsModel> logger)
        {
            _context = context;
            _logger = logger;
        }

      public Movie Movie { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Movie == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            else 
            {
                Movie = movie;
            }
            return Page();
        }
    }
}
