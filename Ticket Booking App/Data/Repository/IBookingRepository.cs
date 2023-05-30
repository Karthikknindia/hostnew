using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Data.Repository
{
    public interface IBookingRepository
    {
        //Task<ResponseModel> UpdateAsync(Booking model);
        Task<IReadOnlyList<Booking>> GetAllAsync();
        //Task<ResponseModel> DeleteAsync(int id);
        Task<Booking> AddAsync(Booking model);
        //Task<ResponseModel> GetByIdAsync(int id);
    }
}
