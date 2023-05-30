using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Data.Repository
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly IConfiguration configuration;
        //private readonly SqlConnection _con;
        private readonly IDbConnection _con;
        public ErrorRepository(IConfiguration configuration, IDbConnection con)
        {
            this.configuration = configuration;
            _con = con;
            //_con = new SqlConnection(configuration.GetConnectionString("TicketConnection"));
        }

        public async Task<Error> AddError(Error model)
        {
            var sql = "sp_insert_error";

            var result = await _con.QueryAsync(sql, new
            {

                
            @Message=model.Message,
            @StackTrace=model.StackTrace,
            @Timestamp=model.Timestamp,


        }, commandType: CommandType.StoredProcedure);

            return model;
        }
    }
}
