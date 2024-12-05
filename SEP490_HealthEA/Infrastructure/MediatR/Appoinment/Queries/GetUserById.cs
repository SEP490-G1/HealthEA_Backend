using Infrastructure.MediatR.Common;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Appoinment.Queries;

public class GetUserById : IRequest<UserDto>
{
    public Guid UserId { get; set; }
}
public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto>
{
    private readonly SqlDBContext _context;

    public GetUserByIdHandler(SqlDBContext context)
    {
        _context = context;
    }
    public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var users = await _context.Users.Where(x => x.UserId == request.UserId).Select(u => new UserDto
        {
            Name = u.FirstName + " " + u.LastName,
            Email = u.Email,
            Gender = u.Gender == true ? "Nam" : u.Gender == false ? "Nữ" : null,
            Dob = u.Dob,
            Phone = u.Phone
        }).FirstOrDefaultAsync(cancellationToken);
        return users;
    }
}

