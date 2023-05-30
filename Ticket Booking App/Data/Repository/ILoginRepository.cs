using Ticket_Booking_App.Models;

namespace Ticket_Booking_App.Data.Repository
{
 
    public interface ILoginRepository
    {

        Task<ResponseModel> UpdateAsync(int id);
        Task<IReadOnlyList<Login>> GetAllAsync();
        Task<ResponseModel> DeleteAsync(int id);
        Task<ResponseModel> AddAsync(Login model);
        Task<ResponseModel> GetByIdAsync(int id);

        T GetValue<T>(string key);
    }
}
