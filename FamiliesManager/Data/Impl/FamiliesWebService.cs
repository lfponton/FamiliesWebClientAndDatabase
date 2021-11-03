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
    public class FamiliesWebService : IFamiliesService
    {
        private readonly HttpClient client;
        public IList<Family> Families { get; private set; }

        public FamiliesWebService()
        {
            client = new HttpClient();
            Families = new List<Family>();
        }

        public async Task<IList<Family>> GetFamiliesAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:5001/Families");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            string result = await response.Content.ReadAsStringAsync();
            IList<Family> families = JsonSerializer.Deserialize<List<Family>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Families = families;
            return families;
        }

        public async Task AddFamilyAsync(Family family)
        {
            if (Families.Any())
            {
                int max = Families.Max(f => f.Id);
                family.Id = (++max);
            }
            else
            {
                family.Id = 1;
            }

            Families.Add(family);
            
            Family newFamily = new()
            {
                Id = family.Id,
                StreetName = family.StreetName,
                HouseNumber = family.HouseNumber,
            };
            string familyAsJson = JsonSerializer.Serialize(newFamily);
            HttpContent  content = new StringContent(
                familyAsJson,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await client.PostAsync("https://localhost:5001/Families", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task RemoveFamilyAsync(int? id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:5001/Families/{id}");
            if(!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task UpdateFamilyAsync(Family family)
        {
            string familyAsJson = JsonSerializer.Serialize(family);
            HttpContent content = new StringContent(familyAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PatchAsync($"https://localhost:5001/Families/{family.Id}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task<Family> GetFamilyByIdAsync(int? familyId)
        {
            IList<Family> families = await GetFamiliesAsync();
            return families.FirstOrDefault(f => f.Id == familyId);
        }
    }
}