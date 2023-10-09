namespace SweatFlex.Maui.Models
{
    public class User
    {
        public string Id { get; set; }

        public int Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public User? Coach { get; set; }
    }
}
