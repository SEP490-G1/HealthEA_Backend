namespace Infrastructure.Services;
public class Email
{
    public string SenderEmail { get; set; }
    public string SenderPassword { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<string> RecipientEmails { get; set; }

    public Email()
    {
        RecipientEmails = new List<string>();
    }
}
