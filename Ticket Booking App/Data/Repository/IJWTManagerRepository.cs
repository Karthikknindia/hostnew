using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Data.Repository
{
    
        public interface IJWTManagerRepository
        {
        Task<Tokens> Authenticate(Login login);
        Task <Tokens> UpdateTokenStatusAsync(string token);

       
    }
    
}
