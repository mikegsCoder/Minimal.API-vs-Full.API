namespace Full.API.Controllers;

using Core.Contracts;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[Route("db")]
public class DbController(IDbService dbService) : ControllerBase
{
    private readonly IDbService _db = dbService ?? throw new ArgumentNullException(nameof(dbService));

    [HttpGet("users")]
    public async Task<IActionResult> UserByUsername([FromQuery] string ? username, CancellationToken cancellationToken)
    {
        object result;

        if (username != null)
        {
            result = await _db.GetUserByUsernameAsync(username, cancellationToken);
        }
        else
        {
            result = await _db.GetAllUsersAsync(cancellationToken);
        }

        return this.Ok(result);
    }
    // http://localhost:5000/db/users
    // Response sattus: 200 OK
    //[
    //    {
    //        "id": "90b21bc9-9062-4142-b3f9-774e6797e080",
    //        "tasks": [
    //            {
    //                "id": "23bf9c00-056c-4c72-9fa7-396c28da66c7",
    //                "category": "Personal",
    //                "status": "In Progress",
    //                "user": "peter123",
    //                "description": "Create MinimalAPI demo project."
    //            }
    //        ],
    //        "username": "peter123",
    //        "firstName": "Peter",
    //        "lastName": "Petrov"
    //    }
    //]
    //
    // http://localhost:5000/db/users?username=peter123
    // Response sattus: 200 OK
    //{
    //    "id": "90b21bc9-9062-4142-b3f9-774e6797e080",
    //    "tasks": [
    //        {
    //            "id": "23bf9c00-056c-4c72-9fa7-396c28da66c7",
    //            "category": "Personal",
    //            "status": "In Progress",
    //            "user": "peter123",
    //            "description": "Create MinimalAPI demo project."
    //        }
    //    ],
    //    "username": "peter123",
    //    "firstName": "Peter",
    //    "lastName": "Petrov"
    //}

    [HttpGet("tasks")]
    public async Task<IActionResult> AllTasks(CancellationToken cancellationToken)
    {
        var result = await this._db.GetAllTasksAsync(cancellationToken);
        return this.Ok(result);
    }
    // http://localhost:5000/db/tasks
    //[
    //    {
    //        "id": "23bf9c00-056c-4c72-9fa7-396c28da66c7",
    //        "category": "Personal",
    //        "status": "In Progress",
    //        "user": "peter123",
    //        "description": "Create MinimalAPI demo project."
    //    }
    //]
}
