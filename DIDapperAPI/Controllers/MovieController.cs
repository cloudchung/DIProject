using Dapper;
using DIDapperAPI.Model;
using DIDapperAPI.service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DIDapperAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieservice;
        private readonly ILogger _logger;
        public MovieController(
            MovieService movieService , ILogger<MovieController> logger) 
        {
            _movieservice = movieService;
            _logger= logger;
        }
        [HttpGet(Name = "Movie")]
        public IEnumerable<Movie> GetList()
        {
            return _movieservice.GetList();
        }
        //查詢資料Detail
        [HttpGet]
        [Route("Get/{id}")]
        public Movie? Get(int id)
        {
            var result = _movieservice.Get(id);
            if (result is null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return result;
        }
        //新增資料
        [HttpPost]
        [Route("Create")]
        //[HttpPost(Name = "Movie/Create")]
        public IActionResult Create([FromBody] Movie parameter)
        //public int Create(Movie parameter)
        {
            var result = _movieservice.Create(parameter);
            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        //修改資料
        [HttpPut]
        //[HttpPut(Name = "Movie/Update")]
        [Route("Update/{id}")]
        //public bool Update(int id,Movie parameter) 
        public IActionResult Update([FromRoute] int id, [FromBody] Movie parameter)
        {
            var targetCard = _movieservice.Get(id);
            if (targetCard is null)
            {
                return NotFound();
            }

            var isUpdateSuccess = _movieservice.Update(id, parameter);
            if (isUpdateSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }
        //刪除資料
        [HttpDelete]
        //[HttpDelete(Name = "Movie/Delete")]
        [Route("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _movieservice.Delete(id);
            return Ok();
        }
    }
}
