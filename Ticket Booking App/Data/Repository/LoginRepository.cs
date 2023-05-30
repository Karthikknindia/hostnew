using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Data.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration configuration;
        //private readonly SqlConnection _con;
        private readonly IDbConnection _con;
        public LoginRepository(IConfiguration configuration, IDbConnection _con)
        {
            this.configuration = configuration;
            this._con = _con;
            //_con = new SqlConnection(configuration.GetConnectionString("TicketConnection"));
        }
      
      

        public async Task<ResponseModel> UpdateAsync(int id)
        {
            var sql = "sp_update_login_status";

            {

                var result = await _con.QueryAsync(sql, new

                {
                    @login_id = id,
                    
                }, 
                commandType: CommandType.StoredProcedure) ;
               
                return new ResponseModel
                {
                    SuccessMessage = "Login status updated"
                };
            }
        }

       public async Task<IReadOnlyList<Login>> GetAllAsync()
        {
            var sql = "sp_get_all_logins";
            {
                
                var result = await _con.QueryAsync<Login>(sql);
                return (IReadOnlyList<Login>)result.ToList();
            }

        }


        public async Task<ResponseModel> DeleteAsync(int login_id)
        {
            var sql = "sp_update_login_status_offline";

            {

                var result = await _con.QueryAsync(sql, new

                {
                    @login_id = login_id,

                },
               commandType: CommandType.StoredProcedure);

                return new ResponseModel
                {
                    SuccessMessage = "updated to offline"
                };
            }
        }

        public async Task<ResponseModel> AddAsync(Login model)
        {
            var sql = "sp_insert_login";
       
                var result = await _con.QueryAsync(sql, new
                {

                    @login_name= model.login_name,
                    @login_email = model.login_email,
                    @login_password = model.login_password,
                    @login_createdate = DateTime.Now,
                    @login_status=model.login_status,
                    @login_usertype=model.login_usertype,


                }, commandType: CommandType.StoredProcedure);




            return new ResponseModel
            {
               Data=result,
               SuccessMessage = "Registered Successfully"
            };

        }

        public async Task<ResponseModel> GetByIdAsync(int id)
        {
            var sql = "sp_login_by_id";

            var result = await _con.QueryAsync(sql, new

            {
                @login_id = id,

            },
              commandType: CommandType.StoredProcedure);

            return new ResponseModel
            {
                SuccessMessage = "get by id"
            };

        }

        public T GetValue<T>(string key)
        {
            var value = configuration.GetValue<T>(key);
            
            return value;
        }
    }
}
