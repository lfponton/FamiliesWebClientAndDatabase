using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace FamiliesManager.Data.Impl
{
    public class AdultsWebServices : IAdultsService
    {
        private readonly HttpClient client;
        public IList<Adult> Adults { get; private set; }

        public AdultsWebServices()
        {
            client = new HttpClient();
            Adults = new List<Adult>();
            GetAdultsAsync(null);
        }

        public async Task<IList<Adult>> GetAdultsAsync(int? familyId)
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:5001/Adults?familyId={familyId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            string result = await response.Content.ReadAsStringAsync();
            IList<Adult> adults = JsonSerializer.Deserialize<List<Adult>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Adults = adults;
            return adults;
        }

        public async Task AddAdultAsync(int familyId, Adult adult)
        {
            if (Adults.Any())
            {
                int max = Adults.Max(adult => adult.Id);
                adult.Id = (++max);
            }
            else
            {
                adult.Id = 1;
            }

            Adults.Add(adult);
            Adult newAdult = new()
            {
                Id = adult.Id,
                FirstName = adult.FirstName,
                LastName = adult.LastName,
                Age = adult.Age,
                EyeColor = adult.EyeColor,
                HairColor = adult.HairColor,
                Height = adult.Height,
                JobTitle = adult.JobTitle,
                Sex = adult.Sex,
                Weight = adult.Weight
            };
            string adultAsJson = JsonSerializer.Serialize(newAdult);
            HttpContent  content = new StringContent(
                adultAsJson,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await client.PostAsync(
                $"https://localhost:5001/Adults?familyId={familyId}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");        
        }

        public async Task RemoveAdultAsync(int familyId, int? id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"https://localhost:5001/Adults/{id}?familyId={familyId}");
            if(!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task UpdateAdultAsync(int? familyId, Adult adult)
        {
            string adultAsJson = JsonSerializer.Serialize(adult);
            HttpContent content = new StringContent(adultAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PatchAsync(
                $"https://localhost:5001/Adults/{adult.Id}?familyId={familyId}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task<Adult> GetAdultAsync(int familyId, int? id)
        {
            IList<Adult> adults = await GetAdultsAsync(familyId);
            return adults.FirstOrDefault(a => a.Id == id);
        }
        
    }
}