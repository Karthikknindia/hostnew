using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Data.Repository
{
    public interface IMoviesRepository
    {
        Task<ResponseModel> UpdateAsync(Movies model);
        Task<IReadOnlyList<Movies>> GetAllAsync();
        Task<ResponseModel> DeleteAsync(int id);
        Task<ResponseModel> AddAsync(Movies model);
        Task<ResponseModel> GetByIdAsync(int id);
    }
}
