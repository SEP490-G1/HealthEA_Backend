namespace Domain.Enum;

public enum EventStatusConstants
{
    Upcoming = 1,    // Sự kiện sắp tới
    Pending = 2,     // Sự kiện đang chờ xử lý
    Recurring = 3,   // Sự kiện định kỳ
    Past = 4,        // Sự kiện đã diễn ra
    Cancelled = 5  // si kien huy
}
