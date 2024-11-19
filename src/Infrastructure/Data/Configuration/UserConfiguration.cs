using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(SeedUsers());
        }

        private IEnumerable<User> SeedUsers()
        {
            var peter = new User
            {
                Id = "90b21bc9-9062-4142-b3f9-774e6797e080",
                Username = "peter123",
                FirstName = "Peter",
                LatName = "Petrov",
            };

            return new List<User>(){ peter };
        }
    }
}

