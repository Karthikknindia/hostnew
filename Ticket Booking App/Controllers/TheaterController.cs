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
    public class TheaterController : Controller
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly IErrorRepository _error;

        public TheaterController(ITheaterRepository theaterRepository, IErrorRepository error)
        {
            this._theaterRepository = theaterRepository;
            this._error = error;
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("GetAlltheaters")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {

                var data = await _theaterRepository.GetAllAsync();
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
        [Route("AddTheater")]
        public async Task<IActionResult> Theater([FromBody] Theater theater)
        {
            try
            {


                if (theater != null)
                {
                    theater.theater_status = "Y";
                    var data = await _theaterRepository.AddAsync(theater);
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

        public async Task<IActionResult> Delete([FromBody] int theater_id)
        {
            try
            {

                var data = await _theaterRepository.DeleteAsync(theater_id);
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
        [Route("UpdateTheater")]
        public async Task<IActionResult> Update(Theater theater)
        {
            try
            {

                var data = await _theaterRepository.UpdateAsync(theater);
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
        public async Task<IActionResult> Get(int theater_id)
        {
            try
            {

                var theater = await _theaterRepository.GetByIdAsync(theater_id);
                if (theater == null)
                {
                    return NotFound();
                }
                return Ok(theater.Data);
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
