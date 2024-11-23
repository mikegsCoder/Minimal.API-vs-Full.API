﻿using Core.Models.DbModels;
using Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IDbService
    {
        Task<IEnumerable<UserModel>> GetAllUsersAsync(CancellationToken cancellationToken);
    }
}
