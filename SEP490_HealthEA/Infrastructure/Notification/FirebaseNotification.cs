namespace Infrastructure.Notification;

public class FirebaseNotification
{
    public string Title { get; set; }
    public string Body { get; set; }
}

public class FirebaseMessage
{
    public string To { get; set; } // Device Token của người nhận
    public Notification Notification { get; set; }
}

public class Notification
{
    public string Title { get; set; }
    public string Body { get; set; }
}

