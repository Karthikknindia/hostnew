using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ticket_Booking_App.Data.Repository;
using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {

        private readonly IMoviesRepository _moviesRepository;
        private readonly IErrorRepository _error;
        public MoviesController(IMoviesRepository _moviesRepository, IErrorRepository error)
        {
            this._moviesRepository = _moviesRepository;
            this._error = error;
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("GetAllMovies")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _moviesRepository.GetAllAsync();
            return Ok(data);
        }
        [HttpPost]
        [Route("AddMovies")]
        public async Task<IActionResult> Movies([FromBody] Movies movies)
        {
            try
            {


                if (movies != null)
                {
                    movies.movie_status = "Y";
                    var data = await _moviesRepository.AddAsync(movies);
                    if (data != null)
                    {
                        return Ok(new
                        {
                            status = "200",
                            message = "success",
                            data = new
                            {
                                data
                            }
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            status = "404",
                            message = "Not Found",

                        });
                    }


                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {

                var error = new Error { Message = ex.Message, StackTrace = ex.StackTrace, Timestamp = DateTime.Now };


                await _error.AddError(error);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }
        [HttpPost]
        [Route("Delete")]

        public async Task<IActionResult> Delete([FromBody] int movie_id)

        {
            try
            {

                var data = await _moviesRepository.DeleteAsync(movie_id);
                return Ok(data);
            }
            catch (Exception ex)
            {

                var error = new Error { Message = ex.Message, StackTrace = ex.StackTrace, Timestamp = DateTime.Now };


                await _error.AddError(error);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(Movies movies)
        {
            try
            {

                var data = await _moviesRepository.UpdateAsync(movies);
                return Ok(data);
            }
            catch (Exception ex)
            {

                var error = new Error { Message = ex.Message, StackTrace = ex.StackTrace, Timestamp = DateTime.Now };


                await _error.AddError(error);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<IActionResult> Get(int movie_id)
        {
            try
            {

                var movies = await _moviesRepository.GetByIdAsync(movie_id);
                if (movies == null)
                {
                    return NotFound();
                }
                return Ok(movies.Data);
            }
            catch (Exception ex)
            {

                var error = new Error { Message = ex.Message, StackTrace = ex.StackTrace, Timestamp = DateTime.Now };


                await _error.AddError(error);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }
    }
}
