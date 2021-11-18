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
    public class PetsWebService : IPetsService
    {
        private readonly HttpClient client;
        public IList<Pet> Pets { get; private set; }

        public PetsWebService()
        {
            client = new HttpClient();
            Pets = new List<Pet>();
            GetFamilyPetsAsync(null);
        }

        public async Task<IList<Pet>> GetFamilyPetsAsync(int? familyId)
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:5001/Pets?familyId={familyId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            string result = await response.Content.ReadAsStringAsync();
            IList<Pet> pets = JsonSerializer.Deserialize<List<Pet>>(result, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Pets = pets;
            return pets;
        }

        public async Task AddPetAsync(int? familyId, Pet pet)
        {
            Pets.Add(pet);
            Pet newPet = new()
            {
                Name = pet.Name,
                Species = pet.Species,
                Age = pet.Age,
            };
            string petAsJson = JsonSerializer.Serialize(newPet);
            HttpContent content = new StringContent(
                petAsJson,
                Encoding.UTF8,
                "application/json"
            );
            HttpResponseMessage response = await client.PostAsync(
                $"https://localhost:5001/Pets?familyId={familyId}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task RemovePetAsync(int familyId, int? id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"https://localhost:5001/Pets/{id}?familyId={familyId}");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task UpdatePetAsync(int? familyId, Pet pet)
        {
            string petAsJson = JsonSerializer.Serialize(pet);
            HttpContent content = new StringContent(petAsJson,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PatchAsync(
                $"https://localhost:5001/Pets/{pet.Id}?familyId={familyId}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task<Pet> GetPetAsync(int familyId, int? id)
        {
            IList<Pet> pets = await GetFamilyPetsAsync(familyId);
            return pets.FirstOrDefault(p => p.Id == id);
        }
    }
}