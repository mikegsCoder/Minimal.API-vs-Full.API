﻿using Core.Contracts;
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
    }
}