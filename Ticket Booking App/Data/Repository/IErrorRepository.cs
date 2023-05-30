using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Data.Repository
{
    public interface IErrorRepository
    {
        Task<Error> AddError(Error model);
    }
}
