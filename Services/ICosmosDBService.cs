namespace Authorization.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Authorization.Models;

    public interface ICosmosDbService
    {
        Task<IEnumerable<ApplicationUser>> GetUsersAsync(string query);
        Task<ApplicationUser> GetUserAsync(string id);
        Task AddUserAsync(ApplicationUser user);
        Task UpdateUserAsync(string id, ApplicationUser user);
        Task DeleteUserAsync(string id);
    }
}