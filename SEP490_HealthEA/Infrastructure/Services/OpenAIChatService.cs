using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class OpenAIChatService : IOpenAIChatService
	{
		private readonly IConfiguration configuration;
		private string ApiKey => configuration["OpenAI:ApiKey"]!;

		public OpenAIChatService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public async Task<string> GetResponseAsync(List<string> instructions)
		{
			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
			var content = instructions.Select(i => new
			{
				type = "text",
				text = i
			}).ToList();
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
			string jsonRequestBody = JsonConvert.SerializeObject(requestBody);
			var postContent = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", postContent);
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
}
