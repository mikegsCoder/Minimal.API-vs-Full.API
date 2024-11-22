namespace Core.Models.Db
{
    public class UserModel : UserBody
    {
        public string Id { get; set; } = null!;

        public IEnumerable<TaskModel> Tasks { get; set; } = null!;
    }
}
