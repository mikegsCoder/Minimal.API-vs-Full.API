using Core.Contracts;
using Core.Models.DbModels;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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
    }
}
