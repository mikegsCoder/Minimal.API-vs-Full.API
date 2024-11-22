namespace Core.Models.Db
{
    public class TaskModel
    {
        public string Id { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string User { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
