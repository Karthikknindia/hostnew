using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket_Booking_App.Data.Repository;
using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        private readonly ILoginRepository _loginRepository;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly IErrorRepository _error;

        public LoginController(ILoginRepository _loginRepository, IJWTManagerRepository jWTManager, IErrorRepository error)
        {
            this._loginRepository = _loginRepository;
            this._jWTManager = jWTManager;
            this._error = error;
        }

        [EnableCors("AllowOrigin")]
        [HttpPost]
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _loginRepository.GetAllAsync();
            return Ok(data);


        }
        [HttpPost]
        [Route("AddUser")]

        public async Task<IActionResult> Login([FromBody] Login login)
        {
            try
            {



                var userExists = (await _loginRepository.GetAllAsync()).Where(u => u.login_email == login.login_email).FirstOrDefault();
                if (userExists != null)
                {

                    return Ok(new
                    {
                        status = "409",
                        message = "User Already Exist",

                    });
                }
                else
                {

                    if (login != null)
                    {
                        login.login_usertype = "u";


                        var data = await _loginRepository.AddAsync(login);
                        var token = _jWTManager.Authenticate(login);
                        if (data != null)
                        {

                            var retrievedLogin = (await _loginRepository.GetAllAsync()).Where(x => x.login_email == login.login_email).FirstOrDefault();
                            return Ok(new
                            {
                                token = token,
                                status = "200",
                                message = "success",
                                data = new
                                {
                                    retrievedLogin
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
            }
            catch (Exception ex)
            {

                var error = new Error { Message = ex.Message, StackTrace = ex.StackTrace, Timestamp = DateTime.Now };


                await _error.AddError(error);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Login login)
        {
            try
            {


                var data = (await _loginRepository.GetAllAsync()).Where(x => x.login_email == login.login_email && x.login_password == login.login_password).FirstOrDefault();

                var token = _jWTManager.Authenticate(login);

                if (data != null)
                {






                    if (data.login_usertype == "a")
                    {




                        return Ok(new
                        {
                            token = token,
                            status = "210",
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
                            token = token,
                            status = "200",
                            message = "success",
                            data = new
                            {
                                data
                            }
                        });
                    }

                }

                else
                {
                    //return Unauthorized();

                    return Ok(new
                    {

                        status = "404",
                        message = "Not Found",
                        data = new
                        {
                            data
                        }
                    });
                }
            }
            catch (Exception ex)
            {

                var error = new Error { Message = ex.Message, StackTrace = ex.StackTrace, Timestamp = DateTime.Now };


                await _error.AddError(error);

                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
        }

        //[HttpDelete("{login_id}")]
        [HttpPost]
        [Route("logout")]

        public async Task<IActionResult> Delete([FromBody] int login_id)
        {
            //string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            //// Expire the token
            //await _jWTManager.UpdateTokenStatusAsync();
            try
            {

                var data = await _loginRepository.DeleteAsync(login_id);
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
        [Route("Expired")]
        public async Task<IActionResult> Token(string token)
        {
            //string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {

                await _jWTManager.UpdateTokenStatusAsync(token);

                return Ok(new
                {
                    token = token,
                    status = "200",
                    message = "Sucess",

                });
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
        public async Task<IActionResult> Update([FromBody] int login_id)
        {
            try
            {

                var data = await _loginRepository.UpdateAsync(login_id);
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
