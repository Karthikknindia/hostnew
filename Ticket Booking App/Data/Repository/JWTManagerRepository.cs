using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Text;
using Ticket_Booking_App.Models;
using System.Data;
using System.Drawing;
using System.Configuration;
using Dapper;

namespace Ticket_Booking_App.Data.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        //private readonly SqlConnection _con;
        private readonly IConfiguration iconfiguration;
        private readonly IDbConnection _con;
        private readonly ILoginRepository _loginRepository;
        public JWTManagerRepository(IConfiguration iconfiguration, ILoginRepository _loginRepository, IDbConnection _con)
        {
            this.iconfiguration = iconfiguration;
            this._loginRepository = _loginRepository;
            //_con = new SqlConnection(iconfiguration.GetConnectionString("TicketConnection"));
            this._con = _con;
        }
        public async Task<Tokens> Authenticate(Login login)
        {

            var data = (await _loginRepository.GetAllAsync()).Where(x => x.login_email == login.login_email && x.login_password == login.login_password).FirstOrDefault();



            if (data != null)
            {



                var secretKey = iconfiguration.GetValue<string>("JwtSettings:SecretKey");
                var issuer = iconfiguration.GetValue<string>("JwtSettings:Issuer");
                var audience = iconfiguration.GetValue<string>("JwtSettings:Audience");
                var accessTokenExpirationMinutes = iconfiguration.GetValue<int>("JwtSettings:AccessTokenExpirationMinutes");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = issuer,
                    Audience = audience,
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, login.login_id.ToString())
                    }),
                    Expires = DateTime.Now.AddMinutes(accessTokenExpirationMinutes),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };




                DateTimeOffset expirationDate = tokenDescriptor.Expires.Value;
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var sql = "sp_insert_token";

                var result = await _con.QueryAsync(sql, new
                {



                    @login_name = data.login_name,
                    @token = tokenString,
                    @token_status = "Active",
                    @user_status = "Logged in",
                    @token_createddate = DateTime.Now,
                    @expiration_date = expirationDate,


                }, commandType: CommandType.StoredProcedure);






                return new Tokens { Token = tokenString };


            }
            else
            {
                return new Tokens
                {
                    Token = null,

                };
            }

        }

        public async Task<Tokens> UpdateTokenStatusAsync(string token)
        {

            var sql = "sp_update_token_status";
            await _con.QueryAsync(sql, new
            {
                @token = token,
                @user_status = "Logged Out"

            }, commandType: CommandType.StoredProcedure);

            return new Tokens
            {
                Token = token,

            };
        }



    }
}
