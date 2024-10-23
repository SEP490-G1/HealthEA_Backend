using System;
using System.Collections.Generic;

namespace Domain.Models.Entities;

public partial class InvalidatedToken
{
    public string Id { get; set; } = null!;

    public DateTime? ExpriryTime { get; set; }
}
