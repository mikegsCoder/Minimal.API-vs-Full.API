namespace Core.Models.DbModels
{
    public class CategoryModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public IEnumerable<TaskModel> Tasks { get; set; } = null!;
    }
}
