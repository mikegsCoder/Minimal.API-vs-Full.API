using Core.Models.DbModels;

namespace Core.Contracts
{
    public interface IDbService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync(CancellationToken cancellationToken);

        Task<UserModel> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);

        Task<IEnumerable<TaskModel>> GetAllTasksAsync(CancellationToken cancellationToken);

        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync(CancellationToken cancellationToken);

        Task<IEnumerable<StatusModel>> GetAllStatusesAsync(CancellationToken cancellationToken);

        Task UpdateТаsкAsync(string taskId, string statusName, CancellationToken cancellation);

        Task CreateUserAsync(string username, string firstName, string lastName, CancellationToken cancellation);

        Task UpdateUserAsync(string username, string firstName, string lastName, CancellationToken cancellation);

        Task DeleteUserByUsernameAsync(string username, CancellationToken cancellation);
    }
}
