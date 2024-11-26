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
        }
    }
}
