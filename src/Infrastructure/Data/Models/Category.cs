using System.ComponentModel.DataAnnotations;
using Infrastructure.Constants;

namespace Infrastructure.Data.Models
{
    public class Category
    {
        public Category()
        {
            Id = Guid.NewGuid().ToString();

            Tasks = new List<UserTask>();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(DatabaseConstants.Category_Name_MaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}
