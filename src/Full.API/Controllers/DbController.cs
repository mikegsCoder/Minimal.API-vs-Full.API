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

    [HttpGet("categories")]
    public async Task<IActionResult> AllCategories(CancellationToken cancellationToken)
    {
        var result = await this._db.GetAllCategoriesAsync(cancellationToken);
        return this.Ok(result);
    }
    // http://localhost:5000/db/categories
    //[
    //    {
    //        "id": "43e674b8-ae0e-48a0-a231-e1bc5cf85e9c",
    //        "name": "Other",
    //        "tasks": []
    //    },
    //    {
    //        "id": "4a9861e0-7884-4d4c-9080-b48de9c883ac",
    //        "name": "Family",
    //        "tasks": []
    //    },
    //    {
    //        "id": "669fe84c-19ce-41f9-ba64-2c844d2b851b",
    //        "name": "Personal",
    //        "tasks": [
    //            {
    //                "id": "23bf9c00-056c-4c72-9fa7-396c28da66c7",
    //                "category": "Personal",
    //                "status": "In Progress",
    //                "user": "peter123",
    //                "description": "Create MinimalAPI demo project."
    //            }
    //        ]
    //    },
    //    {
    //        "id": "9c24c7ac-b86f-4a30-a0fd-bb3ef6e76308",
    //        "name": "Job",
    //        "tasks": []
    //    }
    //]

    [HttpGet("statuses")]
    public async Task<IActionResult> AllStatuses(CancellationToken cancellationToken)
    {
        var result = await this._db.GetAllStatusesAsync(cancellationToken);
        return this.Ok(result);
    }
    // http://localhost:5000/db/statuses
    //[
    //    {
    //        "id": "0a7bead2-d075-4100-b803-05498f07347b",
    //        "name": "In Progress",
    //        "tasks": [
    //            {
    //                "id": "23bf9c00-056c-4c72-9fa7-396c28da66c7",
    //                "category": "Personal",
    //                "status": "In Progress",
    //                "user": "peter123",
    //                "description": "Create MinimalAPI demo project."
    //            }
    //        ]
    //    },
    //    {
    //        "id": "2b381501-15b1-488b-96e2-950869d68d79",
    //        "name": "Finished",
    //        "tasks": []
    //    },
    //    {
    //        "id": "d920dc1b-fab5-40cc-a387-81f7059da658",
    //        "name": "Canceled",
    //        "tasks": []
    //    },
    //    {
    //        "id": "f83e8ace-ea2c-48f0-846d-1f86c7eb127d",
    //        "name": "Awaiting",
    //        "tasks": []
    //    }
    //]

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] UserBody? user, [FromQuery] string? username, [FromQuery] string? firstName, [FromQuery] string? lastName, CancellationToken cancellationToken)
    {
        if (user != null)
        {
            await this._db.CreateUserAsync(user.Username, user.FirstName, user.LastName, cancellationToken);
        }
        else if (username != null && firstName != null && lastName != null)
        {
            await this._db.CreateUserAsync(username, firstName, lastName, cancellationToken);
        }

        return this.Created();
    }
    // POST with Postman to:
    // localhost:5000/db/users?username=george123&firstName=George&lastName=Smith
    // Response sattus: 204 No Content
    // http://localhost:5000/db/users
    //[
    //    {
    //        "id": "1b8b644f-5dea-45da-9ab3-0797dedb304e",
    //        "tasks": [],
    //        "username": "george123",
    //        "firstName": "George",
    //        "lastName": "Smith"
    //    },
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
    // POST with Postman to:
    // localhost:5000/db/users
    // Headers:
    //     Content-Type: application/json
    // Body:
    // {
    //     "username" : "bobby123",
    //     "firstName" : "Bobby",
    //     "lastName" : "Talor"
    // }
    // Response sattus: 204 No Content
    // http://localhost:5000/db/users
    //[
    //    {
    //        "id": "1b8b644f-5dea-45da-9ab3-0797dedb304e",
    //        "tasks": [],
    //        "username": "george123",
    //        "firstName": "George",
    //        "lastName": "Smith"
    //    },
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
    //    },
    //    {
    //    "id": "da60395b-8a0c-45a4-8688-ec5ea6601841",
    //        "tasks": [],
    //        "username": "bobby123",
    //        "firstName": "Bobby",
    //        "lastName": "Talor"
    //    }
    //]

    [HttpPut("users")]
    public async Task<IActionResult> UpdateUser([FromBody] UserBody user, CancellationToken cancellationToken)
    {
        await this._db.UpdateUserAsync(user.Username, user.FirstName, user.LastName, cancellationToken);
        return this.Ok();
    }
    // PUT with Postman to localhost:5000/db/users
    // Headers:
    //     Content-Type: application/json
    // Body:
    // {
    //     "username" : "NewUsername321",
    //     "firstName" : "Bobby",
    //     "lastName" : "Talor"
    // }
    // Response sattus: 200 Ok
    //
    // http://localhost:5000/db/users?username=NewUsername321
    //{
    //    "id": "da60395b-8a0c-45a4-8688-ec5ea6601841",
    //    "tasks": [],
    //    "username": "NewUsername321",
    //    "firstName": "Bobby",
    //    "lastName": "Talor"
    //}
}
