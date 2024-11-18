using Google.Apis.Auth.OAuth2;
using Infrastructure.SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

public class FirebaseSettings
{
    public string ProjectId { get; set; }
}

public class FirebaseNotificationService
{
    private readonly HttpClient _httpClient;
    private readonly FirebaseSettings _firebaseSettings;
    private readonly SqlDBContext _context;

    public FirebaseNotificationService(HttpClient httpClient, IOptions<FirebaseSettings> firebaseSettings, SqlDBContext context)
    {
        _httpClient = httpClient;
        _firebaseSettings = firebaseSettings.Value;
        _context = context;
    }

    //public async Task SendNotificationAsync(string deviceToken, string title, string body)
    //{
    //    var accessToken = await GetAccessTokenAsync();

    //    var message = new
    //    {
    //        message = new
    //        {
    //            token = deviceToken,
    //            notification = new
    //            {
    //                title = title,
    //                body = body
    //            }
    //        }
    //    };

    //    var jsonMessage = JsonSerializer.Serialize(message);
    //    var requestContent = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

    //    _httpClient.DefaultRequestHeaders.Clear();
    //    _httpClient.DefaultRequestHeaders.Authorization =
    //        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

    //    var url = $"https://fcm.googleapis.com/v1/projects/sep490-c3c0a/messages:send";
    //    var response = await _httpClient.PostAsync(url, requestContent);

    //    if (!response.IsSuccessStatusCode)
    //    {
    //        var responseBody = await response.Content.ReadAsStringAsync();

    //        if (responseBody.Contains("\"errorCode\": \"UNREGISTERED\""))
    //        {
    //            Console.WriteLine("Device token is no longer valid. Removing from database...");
    //            await InvalidateDeviceToken(deviceToken); 
    //        }
    //        else
    //        {
    //            throw new Exception($"Failed to send notification: {responseBody}");
    //        }
    //    }
    //}

    //private async Task InvalidateDeviceToken(string deviceToken)
    //{
    //    var tokenEntity = await _context.DeviceTokens.FirstOrDefaultAsync(dt => dt.DeviceToken == deviceToken);
    //    if (tokenEntity != null)
    //    {
    //        _context.DeviceTokens.Remove(tokenEntity);
    //        await _context.SaveChangesAsync();
    //    }
    //}
    private static readonly SemaphoreSlim _notificationLock = new SemaphoreSlim(1, 1);

    public async Task SendNotificationAsync(string deviceToken, string title, string body)
    {
        if (string.IsNullOrEmpty(deviceToken))
        {
            Console.WriteLine("Device token is null or empty. Skipping notification.");
            return;
        }

        try
        {
            await _notificationLock.WaitAsync();

            var accessToken = await GetAccessTokenAsync();

            var message = new
            {
                message = new
                {
                    token = deviceToken,
                    notification = new
                    {
                        title = title,
                        body = body
                    }
                }
            };

            var jsonMessage = JsonSerializer.Serialize(message);
            var requestContent = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var url = $"https://fcm.googleapis.com/v1/projects/sep490-c3c0a/messages:send";
            var response = await _httpClient.PostAsync(url, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody.Contains("\"errorCode\": \"UNREGISTERED\""))
                {
                    Console.WriteLine("Device token is no longer valid. Removing from database...");
                    await InvalidateDeviceToken(deviceToken);
                }
                else
                {
                    throw new Exception($"Failed to send notification: {responseBody}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending notification: {ex.Message}");
        }
        finally
        {
            _notificationLock.Release();
        }
    }

    private async Task InvalidateDeviceToken(string deviceToken)
    {
        var tokenEntity = await _context.DeviceTokens.FirstOrDefaultAsync(dt => dt.DeviceToken == deviceToken);
        if (tokenEntity != null)
        {
            _context.DeviceTokens.Remove(tokenEntity);
            await _context.SaveChangesAsync();
        }
    }

    private async Task<string> GetAccessTokenAsync()
    {
        GoogleCredential credential;
        using (var stream = new FileStream("firebaseServiceAccount.json", FileMode.Open, FileAccess.Read))
        {
            credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromStream(stream)
                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
        }

        var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        return token;
    }
}
