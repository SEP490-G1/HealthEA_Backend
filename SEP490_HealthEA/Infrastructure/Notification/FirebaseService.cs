namespace Infrastructure.Notification;

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class FirebaseService
{
    private readonly HttpClient _httpClient;
    private readonly string _firebaseServerKey;

    public FirebaseService(HttpClient httpClient, string firebaseServerKey)
    {
        _httpClient = httpClient;
        _firebaseServerKey = firebaseServerKey;
    }

    public async Task SendNotificationAsync(string deviceToken, string title, string body)
    {
        var message = new FirebaseMessage
        {
            To = deviceToken,
            Notification = new Notification
            {
                Title = title,
                Body = body
            }
        };

        var jsonMessage = JsonSerializer.Serialize(message);
        var requestContent = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

        // Đặt tiêu đề (header) với `Authorization` chứa server key của Firebase
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={_firebaseServerKey}");

        var response = await _httpClient.PostAsync("https://fcm.googleapis.com/fcm/send", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to send notification: {responseBody}");
        }
    }
}

