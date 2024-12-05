namespace Infrastructure.MediatR.Appoinment.Queries
{
    public class UserDto
    {
        public string? Name {  get; set; }
        public string? Gender {  get; set; }
        public DateOnly? Dob {  get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
