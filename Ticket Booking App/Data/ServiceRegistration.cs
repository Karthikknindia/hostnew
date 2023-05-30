using Ticket_Booking_App.Data.Repository;

namespace Ticket_Booking_App.Data
{
    public static class ServiceRegistration
    {

        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ILoginRepository, LoginRepository>();

            services.AddTransient<ITheaterRepository, TheaterRepository>();

            services.AddTransient<IMoviesRepository, MoviesRepository>();

            services.AddTransient<IBookingRepository, BookingRepository>();

            services.AddTransient<IJWTManagerRepository, JWTManagerRepository>();

            services.AddTransient<IErrorRepository, ErrorRepository>();

        }
    }
}
