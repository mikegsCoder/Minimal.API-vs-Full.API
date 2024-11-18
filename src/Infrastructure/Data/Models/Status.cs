using System.ComponentModel.DataAnnotations;
using Infrastructure.Constants;

namespace Infrastructure.Data.Models
{
    public class Status
    {
        public Status()
        {
            Id = Guid.NewGuid().ToString();

            Tasks = new List<UserTask>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(DatabaseConstants.Status_Name_MaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}
