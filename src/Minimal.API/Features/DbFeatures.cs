using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Core.Models;
using System.Reflection;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Minimal.API.Features
{
    public static class DbFeatures
    {
        public static void AddDbFeatures(this IEndpointRouteBuilder app)
        {
            var infoGroup = app.MapGroup("db");

            infoGroup.MapGet("users", async ([FromQuery] string ? username, [FromServices] IDbService db, CancellationToken cancellationToken) =>
            {
                object result;

                if (username != null)
                {
                    result = await db.GetUserByUsernameAsync(username, cancellationToken);
                }
                else
                {
                    result = await db.GetAllUsersAsync(cancellationToken);
                }

                return result;
            });
            // http://localhost:5000/db/users
            //[
            //   {
            //        "id": "90b21bc9-9062-4142-b3f9-774e6797e080",
            //        "username": "peter123",
            //        "firstName": "Peter",
            //        "lastName": "Petrov",
            //        "tasks": [
            //            {
            //                "id": "23bf9c00-056c-4c72-9fa7-396c28da66c7",
            //                "category": "Personal",
            //                "status": "In Progress",
            //                "user": "peter123",
            //                "description": "Create MinimalAPI demo project."
            //            }
            //        ]
            //    }
            //]
            //
            // http://localhost:5000/db/users?username=peter123
            //{
            //    "id": "90b21bc9-9062-4142-b3f9-774e6797e080",
            //    "username": "peter123",
            //    "firstName": "Peter",
            //    "lastName": "Petrov",
            //    "tasks": [
            //        {
            //            "id": "23bf9c00-056c-4c72-9fa7-396c28da66c7",
            //            "category": "Personal",
            //            "status": "In Progress",
            //            "user": "peter123",
            //            "description": "Create MinimalAPI demo project."
            //        }
            //    ]
            //}

            infoGroup.MapGet("tasks", async ([FromServices] IDbService db, CancellationToken cancellationToken) => await db.GetAllTasksAsync(cancellationToken));
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

            infoGroup.MapGet("categories", async ([FromServices] IDbService db, CancellationToken cancellationToken) => await db.GetAllCategoriesAsync(cancellationToken));
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

            infoGroup.MapGet("statuses", async ([FromServices] IDbService db, CancellationToken cancellationToken) => await db.GetAllStatusesAsync(cancellationToken));
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

            infoGroup.MapPost("users", async ([FromBody] UserBody? user, [FromQuery] string? username, [FromQuery] string? firstName, [FromQuery] string? lastName, [FromServices] IDbService db, CancellationToken cancellationToken) =>
            {
                if (user != null)
                {
                    await db.CreateUserAsync(user.Username, user.FirstName, user.LastName, cancellationToken);
                }
                else if (username != null && firstName != null && lastName != null)
                {
                    await db.CreateUserAsync(username, firstName, lastName, cancellationToken);
                }

                return Results.Created();
            });
            // POST with Postman to:
            // localhost:5000/db/users?username=george321&firstName=George&lastName=Jackson
            // Response sattus: 201 Created
            // http://localhost:5000/db/users
            //[
            //    {
            //        "id": "2899c551-b2e6-4844-9801-68c896320713",
            //        "tasks": [],
            //        "username": "george321",
            //        "firstName": "George",
            //        "lastName": "Jackson"
            //    },
            //    {
            //                "id": "90b21bc9-9062-4142-b3f9-774e6797e080",
            //        "tasks": [
            //            {
            //                    "id": "23bf9c00-056c-4c72-9fa7-396c28da66c7",
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
            // Response sattus: 201 Created
            // http://localhost:5000/db/users
            //[
            //    {
            //        "id": "2899c551-b2e6-4844-9801-68c896320713",
            //        "tasks": [],
            //        "username": "george321",
            //        "firstName": "George",
            //        "lastName": "Jackson"
            //    },
            //    {
            //                "id": "49665db1-63c5-4f9a-b652-445c4c5b0508",
            //        "tasks": [],
            //        "username": "bobby123",
            //        "firstName": "Bobby",
            //        "lastName": "Talor"
            //    },
            //    {
            //                "id": "90b21bc9-9062-4142-b3f9-774e6797e080",
            //        "tasks": [
            //            {
            //                    "id": "23bf9c00-056c-4c72-9fa7-396c28da66c7",
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

            infoGroup.MapPut("users", async ([FromBody] UserBody user, [FromServices] IDbService db, CancellationToken cancellationToken) =>
            {
                await db.UpdateUserAsync(user.Username, user.FirstName, user.LastName, cancellationToken);

                return Results.Ok();
            });
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
            //    "id": "49665db1-63c5-4f9a-b652-445c4c5b0508",
            //    "tasks": [],
            //    "username": "NewUsername321",
            //    "firstName": "Bobby",
            //    "lastName": "Talor"
            //}
        }
    }
}
