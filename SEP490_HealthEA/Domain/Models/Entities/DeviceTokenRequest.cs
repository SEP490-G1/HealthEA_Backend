namespace Domain.Models.Entities;

public class DeviceTokenRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string DeviceToken { get; set; }
    public string DeviceType { get; set; }
}
