using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Data.Repository
{
    public interface ITheaterRepository
    {
        Task<ResponseModel> UpdateAsync(Theater model);
        Task<IReadOnlyList<Theater>> GetAllAsync();
        Task<ResponseModel> DeleteAsync(int id);
        Task<ResponseModel> AddAsync(Theater model);
        Task<ResponseModel> GetByIdAsync(int id);
    }
}
