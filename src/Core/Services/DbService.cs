using Core.Contracts;
using Core.Models.DbModels;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class DbService : IDbService
    {
        private readonly ApiDbContext _db;

        public DbService(ApiDbContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await this._db.Users
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LatName,
                    Tasks = u.Tasks
                        .Select(t => new TaskModel
                        {
                            Id = t.Id,
                            Category = t.Category.Name,
                            Status = t.Status.Name,
                            User = t.User.Username,
                            Description = t.Description,
                        })
                        .ToArray(),
                })
                .ToArrayAsync();

            return users;
        }

        public async Task<UserModel> GetUserByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var user = await this._db.Users
                .Where(x => x.Username == username)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    FirstName = u.FirstName,
                    LastName = u.LatName,
                    Tasks = u.Tasks
                        .Select(t => new TaskModel
                        {
                            Id = t.Id,
                            Category = t.Category.Name,
                            Status = t.Status.Name,
                            User = t.User.Username,
                            Description = t.Description,
                        })
                        .ToArray(),
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync(CancellationToken cancellationToken)
        {
            var tasks = await this._db.Tasks
                .Select(t => new TaskModel
                {
                    Id = t.Id,
                    Category = t.Category.Name,
                    Status = t.Status.Name,
                    User = t.User.Username,
                    Description = t.Description,
                })
                .ToArrayAsync();

            return tasks;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await this._db.Categories
                .Select(c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Tasks = c.Tasks
                        .Select(t => new TaskModel
                        {
                            Id = t.Id,
                            Category = t.Category.Name,
                            Status = t.Status.Name,
                            User = t.User.Username,
                            Description = t.Description,
                        })
                        .ToArray(),
                })
                .ToArrayAsync();

            return categories;
        }

        public async Task<IEnumerable<StatusModel>> GetAllStatusesAsync(CancellationToken cancellationToken)
        {
            var statuses = await this._db.Statuses
                .Select(c => new StatusModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Tasks = c.Tasks
                        .Select(t => new TaskModel
                        {
                            Id = t.Id,
                            Category = t.Category.Name,
                            Status = t.Status.Name,
                            User = t.User.Username,
                            Description = t.Description,
                        })
                        .ToArray(),
                })
                .ToArrayAsync();

            return statuses;
        }

        public async Task CreateUserAsync(string username, string firstName, string lastName, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = username,
                FirstName = firstName,
                LatName = lastName,
            };

            await this._db.Users.AddAsync(user, cancellationToken);
            await this._db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUserAsync(string username, string firstName, string lastName, CancellationToken cancellation)
        {
            var user = await this._db.Users
                .Where(x => x.FirstName == firstName && x.LatName == lastName)
                .FirstOrDefaultAsync();

            user.Username = username;

            this._db.Users.Update(user);
            await this._db.SaveChangesAsync();
        }

        public async Task DeleteUserByUsernameAsync(string username, CancellationToken cancellation)
        {
            var user = await this._db.Users
               .Where(x => x.Username == username)
               .FirstOrDefaultAsync();

            this._db.Users.Remove(user);
            await this._db.SaveChangesAsync();
        }

        public async Task UpdateТаsкAsync(string taskId, string statusName, CancellationToken cancellation)
        {
            var task = await this._db.Tasks
                .Where(x => x.Id == taskId)
                .FirstOrDefaultAsync();

            var newStatus = await this._db.Statuses
                .Where(x => x.Name == statusName)
                .FirstOrDefaultAsync();

            task.Status = newStatus;

            this._db.Update(task);
            await this._db.SaveChangesAsync();
        }
    }
}
