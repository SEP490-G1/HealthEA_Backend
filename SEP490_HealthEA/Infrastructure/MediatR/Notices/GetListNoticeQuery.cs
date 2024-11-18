using Domain.Common.Exceptions;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Notices;

public class GetListNoticeQuery : IRequest<List<NoticeDto>>
{
    public Guid UserId { get; set; }
}
public class GetListNoticeHandler : IRequestHandler<GetListNoticeQuery, List<NoticeDto>>
{
    private readonly SqlDBContext _context;

    public GetListNoticeHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<List<NoticeDto>> Handle(GetListNoticeQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await _context.Users
       .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        if (user == null)
        {
            throw new Exception(ErrorCode.USER_NOT_FOUND);
        }

        var notices = await _context.Notices
            .Where(n => n.RecipientId == request.UserId)
            .Include(n => n.Users)
            .Select(n => new NoticeDto
            {
                NoticeId = n.NoticeId,
                Message = n.Message,
                RecipientName = $"{n.Users.FirstName} {n.Users.LastName}",
                CreatedAt = n.CreatedAt
            })
            .ToListAsync(cancellationToken);

        if (notices == null || !notices.Any())
        {
            throw new Exception(ErrorCode.NOTICES_NOT_FOUND);
        }

        return notices;
    }
}