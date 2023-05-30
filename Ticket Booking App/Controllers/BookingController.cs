using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Ticket_Booking_App.Data.Repository;
using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly IErrorRepository _error;
        public BookingController(IBookingRepository bookingRepository, IJWTManagerRepository jWTManager, IErrorRepository error)
        {
            this._bookingRepository = bookingRepository;
            this._jWTManager = jWTManager;
            this._error = error;
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("GetAllBooking")]
        public async Task<IActionResult> GetAllAsync()
        {


            try
            {
                var data = await _bookingRepository.GetAllAsync();
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
        [Route("AddBooking")]
        public async Task<IActionResult> Booking([FromBody] Booking booking)
        {
            try
            {


                if (booking != null)
                {
                    booking.booking_status = "success";
                    var data = await _bookingRepository.AddAsync(booking);
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
                //throw;
            }
        }

        [HttpPost]
        [Route("getbyname")]
        public async Task<IActionResult> GetByNameAsync(string username)
        {
            try
            {


                var data = await _bookingRepository.GetAllAsync();
                if (!string.IsNullOrEmpty(username))
                {
                    data = data.Where(x => x.booking_name.ToLower() == username.ToLower()).ToList();
                }
                return Ok(data);
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
