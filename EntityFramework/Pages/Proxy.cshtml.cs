using EntityFramework.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EntityFramework.Pages
{
    public class ProxyModel : PageModel
    {
        private readonly ILogger _logger;
        public ProxyModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public IList<Movie> Movie { get; set; } = default!;
        //public async Task OnGetAsync()
        //{
        //    List<Movie> reservationList = new List<Movie>();
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var response = await httpClient.GetAsync("https://localhost:7057/Movie"))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            reservationList = JsonConvert.DeserializeObject<List<Movie>>(apiResponse);

        //        }
        //    }
        //}

        public async Task<IActionResult> OnGet()
        {
            List<Movie> reservationList = new List<Movie>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7057/Movie"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<Movie>>(apiResponse);

                }
            }
            return new JsonResult(reservationList);
        }
    }
}
