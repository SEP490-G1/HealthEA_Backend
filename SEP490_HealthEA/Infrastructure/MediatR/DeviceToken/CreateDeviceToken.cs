using Domain.Models.Entities;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.DeviceToken;

public class RegisterDeviceTokenCommand : IRequest<string>
{
    public Guid UserId { get; set; }
    public string DeviceToken { get; set; }
    public string DeviceType { get; set; }
}
public class RegisterDeviceTokenHandler : IRequestHandler<RegisterDeviceTokenCommand, string>
{
    private readonly SqlDBContext _context;

    public RegisterDeviceTokenHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(RegisterDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var existingToken = await _context.DeviceTokens
            .FirstOrDefaultAsync(dt => dt.DeviceToken == request.DeviceToken, cancellationToken);

        if (existingToken == null)
        {
            var newToken = new DeviceTokenRequest
            {
                UserId = request.UserId,
                DeviceToken = request.DeviceToken,
                DeviceType = request.DeviceType,
            };
            _context.DeviceTokens.Add(newToken);
        } else
        {
            existingToken.UserId = request.UserId;
            existingToken.DeviceToken = request.DeviceToken;
            _context.Update(existingToken);
        }
        await _context.SaveChangesAsync(cancellationToken);

        return "Device token registered successfully.";
    }
}
