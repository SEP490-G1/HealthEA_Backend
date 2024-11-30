using Domain.Common.Exceptions;
using Infrastructure.MediatR.Common;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.MediatR.Notices;

public class GetListNoticeQuery : IRequest<PaginatedList<NoticeDto>>
{
    public Guid UserId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
public class GetListNoticeHandler : IRequestHandler<GetListNoticeQuery, PaginatedList<NoticeDto>>
{
    private readonly SqlDBContext _context;

    public GetListNoticeHandler(SqlDBContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<NoticeDto>> Handle(GetListNoticeQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = _context.Notices
            .Where(n => n.RecipientId == request.UserId)
            .Include(n => n.Users)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NoticeDto
            {
                NoticeId = n.NoticeId,
                Message = n.Message,
                SenderName = $"{n.Users.FirstName} {n.Users.LastName}",
                CreatedAt = n.CreatedAt,
                HasViewed = n.HasViewed,
            });

        var paginatedNotices = await PaginatedList<NoticeDto>.CreateAsync(
            query, request.PageNumber, request.PageSize
        );

        //if (paginatedNotices == null || !paginatedNotices.Items.Any())
        //{
        //    throw new Exception(ErrorCode.NOTICES_NOT_FOUND);
        //}

        return paginatedNotices;
    }
}