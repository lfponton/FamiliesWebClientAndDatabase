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
    public class ChildrenWebService : IChildrenService
    {
        private readonly HttpClient client;
        public IList<Child> Children { get; private set; }

        public ChildrenWebService()
        {
            client = new HttpClient();
            Children = new List<Child>();
            GetChildrenAsync(null);
        }

        public async Task<IList<Child>> GetChildrenAsync(int? familyId)
        {
            HttpResponseMessage response =
                await client.GetAsync($"https://localhost:5001/Children?familyId={familyId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            string result = await response.Content.ReadAsStringAsync();
            IList<Child> children = JsonSerializer.Deserialize<List<Child>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Children = children;
            return children;
        }

        public async Task AddChildAsync(int familyId, Child child)
        {
            Children.Add(child);
            Child newChild = new()
            {
                FirstName = child.FirstName,
                LastName = child.LastName,
                Age = child.Age,
                EyeColor = child.EyeColor,
                HairColor = child.HairColor,
                Height = child.Height,
                Sex = child.Sex,
                Interests = child.Interests,
                Pets = child.Pets,
                Weight = child.Weight
            };
            string childAsJson = JsonSerializer.Serialize(newChild);
            HttpContent content = new StringContent(
                childAsJson,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await client.PostAsync(
                $"https://localhost:5001/Children?familyId={familyId}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task RemoveChildAsync(int familyId, int? id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"https://localhost:5001/Children/{id}?familyId={familyId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task UpdateChildAsync(int? familyId, Child child)
        {
            string childAsJson = JsonSerializer.Serialize(child);
            HttpContent content = new StringContent(childAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PatchAsync(
                $"https://localhost:5001/Children/{child.Id}?familyId={familyId}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task<Child> GetChildAsync(int familyId, int? id)
        {
            IList<Child> children = await GetChildrenAsync(familyId);
            return children.FirstOrDefault(c => c.Id == id);
        }
    }
}