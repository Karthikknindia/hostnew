using Dapper;
using System.Data;
using System.Data.SqlClient;
using Ticket_Booking_App.Models;
namespace Ticket_Booking_App.Data.Repository
{
    public class BookingRepository:IBookingRepository
    {
        private readonly IConfiguration configuration;
        private readonly IDbConnection _con;
        //private readonly SqlConnection _con;

        public BookingRepository(IConfiguration configuration, IDbConnection con)
        {
            this.configuration = configuration;
            _con = con;
            //_con = new SqlConnection(configuration.GetConnectionString("TicketConnection"));
        }

        public async Task<Booking> AddAsync(Booking model)
        {
            var sql = "sp_insert_booking";

            var result = await _con.QueryAsync(sql, new
            {
                @booking_name = model.booking_name,
                @booking_email=model.booking_email,
                @booking_seats=model.booking_seats,
                @booking_movie=model.booking_movie,
                @booking_date=model.booking_date,
                @booking_theater=model.booking_theater,
                @booking_showtime=model.booking_showtime,
                @booking_status=model.booking_status,
                @booking_createdate=model.booking_createdate,
                @booking_updatedate=model.booking_updatedate,
                @booking_poster=model.booking_poster,
                @booking_amount=model.booking_amount,



            }, commandType: CommandType.StoredProcedure);




            return model;
        }

       

        public async Task<IReadOnlyList<Booking>> GetAllAsync()
        {
            var sql = "sp_get_all_bookings";
            {

                var result = await _con.QueryAsync<Booking>(sql);
                return result.OrderByDescending(x => x.booking_id).ToList();
            }
        }







        //public async Task<ResponseModel> GetByIdAsync(int id)
        //{
        //    var sql = "sp_insert_booking_by_id";

        //    {

        //        var result = await _con.QuerySingleOrDefaultAsync<Booking>(sql);
        //        return new ResponseModel
        //        {
        //            Data = result
        //        };
        //    }
        //}



        //public Task<ResponseModel> UpdateAsync(Booking model)
        //{
        //    throw new NotImplementedException();
        //}



        //public async Task<ResponseModel> DeleteAsync(int id)
        //{

        //        var sql = "sp_delete_booking_by_id";

        //        var result = await _con.QuerySingleOrDefaultAsync<Booking>(sql);


        //        if (result != null && result.booking_createdate < DateTime.UtcNow)
        //        {
        //            result.booking_status = "expired";
        //        }

        //        return new ResponseModel
        //        {
        //            Data = result
        //        };


        //}
    }
}
