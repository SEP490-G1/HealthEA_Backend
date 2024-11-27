using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class ImageScanService : IImageScanService
	{
		private readonly IConfiguration configuration;
		private string ApiKey => configuration["OpenAI:ApiKey"]!;

		public ImageScanService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		private string ConvertStreamToBase64(MemoryStream stream)
		{
			byte[] imageBytes = stream.ToArray();
			return Convert.ToBase64String(imageBytes);
		}

		private async Task<string> SendRequestToOpenAIAsync(string base64Image, string instruction)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
				var content = new List<object>()
				{
					new {
						type = "text",
						text = instruction,
					},
					new {
						type = "text",
						text = "Please return the JSON object as plain text, with no additional formatting or Markdown.",
					},
					new
					{
						type = "image_url",
						image_url = new
						{
							url = $"data:image/jpeg;base64,{base64Image}"
						}
					}
				};
				// Build the request body
				var requestBody = new
				{
					model = "gpt-4o",
					messages = new[]
					{
						new
						{
							role = "user",
							content = content.ToArray()
						}
					}
				};

				// Serialize the request body to JSON
				string jsonRequestBody = JsonConvert.SerializeObject(requestBody);

				// Create the content for the HTTP request
				var postContent = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

				// Send the request to OpenAI API
				HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", postContent);

				// Read and output the response
				if (response.IsSuccessStatusCode)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
					return responseObject.choices[0].message.content.ToString();
				}
				else
				{
					return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
				}
			}
		}

		public async Task<string> GetPrescriptionAsync(MemoryStream stream)
		{
			var base64Image = ConvertStreamToBase64(stream);
			var instruction = "Extract all the prescribed items in this prescription and return them in JSON format. It should be a list of items, with the item having name, amount, unit, and frequency.";
			return await SendRequestToOpenAIAsync(base64Image, instruction);
		}

		public async Task<string> GetBloodTestAsync(MemoryStream stream)
		{
			var base64Image = ConvertStreamToBase64(stream);
			var instruction = "Extract the blood test info in this prescription and return them in JSON format. It should be a list of items, with the item having name, value, unit, and result.";
			return await SendRequestToOpenAIAsync(base64Image, instruction);
		}

		public async Task<string> GetUrinalystAsync(MemoryStream stream)
		{
			var base64Image = ConvertStreamToBase64(stream);
			var instruction = "Extract the urinalyst info in this prescription and return them in JSON format. It should be a list of items, with the item having name, value, unit, and result.";
			return await SendRequestToOpenAIAsync(base64Image, instruction);
		}
	}
}
