namespace api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public int age { get; set; }
        public string weight { get; set; }
    }
}