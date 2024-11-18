using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Constants;

namespace Infrastructure.Data.Models
{
    public class UserTask
    {
        public UserTask()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        public Category Category { get; set; } = null!;

        [Required]
        public string CategoryId { get; set; } = null!;

        public Status Status { get; set; } = null!;

        [Required]
        public string StatusId { get; set; } = null!;

        public User User { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [MaxLength(DatabaseConstants.UserTask_Description_MaxLength)]
        public string Description { get; set; } = null!;
    }
}
