using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.UserManagement.Commands.CreateUser
{
    public class CreateUser : IRequest<User>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUser, User>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password, 
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                Dob = request.Dob,
                Gender = request.Gender,
                //Status = Status.Active
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
