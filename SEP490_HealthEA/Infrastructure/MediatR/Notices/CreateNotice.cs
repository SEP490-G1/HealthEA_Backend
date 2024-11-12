﻿using Domain.Models.Entities;
using Google;
using Infrastructure.SQLServer;
using MediatR;

namespace Infrastructure.MediatR.Notices;

public class CreateNoticeCommand : IRequest<string>
{
    public Guid UserId { get; set; }
    public Guid RecipientId { get; set; }
    public string Message { get; set; }
}
public class CreateNoticeCommandHandler : IRequestHandler<CreateNoticeCommand, string>
{
    private readonly SqlDBContext _context;
    private readonly IMediator _mediator;

    public CreateNoticeCommandHandler(SqlDBContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<string> Handle(CreateNoticeCommand request, CancellationToken cancellationToken)
    {
        var notice = new Notice
        {
            NoticeId = Guid.NewGuid(),
            UserId = request.UserId,
            RecipientId = request.RecipientId,
            Message = request.Message,
        };

        _context.Notices.Add(notice);
        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new NoticeCreatedEvent(notice), cancellationToken);

        return "Notice created and notification sent!";
    }

}