namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string Status { get; set; }
    }
}
