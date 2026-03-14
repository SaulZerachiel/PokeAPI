using System.Net.Http.Json;
using APIProject.Models;

namespace APIProject.Services
{
    public class PokemonService
    {
        private readonly HttpClient _http;

        public PokemonService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Pokemon?> GetPokemon(string nom)
        {
            // Appel REST a l'API publique PokeAPI.
            var reponse = await _http.GetFromJsonAsync<PokemonApiReponse>(
                $"https://pokeapi.co/api/v2/pokemon/{nom.ToLower()}"
            );

            if (reponse == null) return null;

            // Mapping du format externe (DTO API) vers le modele interne Pokemon.
            return new Pokemon
            {
                Name = reponse.Name,
                Height = reponse.Height,
                Weight = reponse.Weight,
                ImageUrl = reponse.Sprites.Front_Default,
                Types = reponse.Types.Select(t => t.Type.Name).ToList()
            };
        }

        private class TypeEntry
        {
            public TypeInfo Type { get; set; } = new();
        }

        private class TypeInfo
        {
            public string Name { get; set; } = "";
        }
        private class PokemonApiReponse
        {
            // Ces classes internes representent uniquement la reponse JSON distante.
            public string Name { get; set; } = "";
            public int Height { get; set; }
            public int Weight { get; set; }
            public Sprites Sprites { get; set; } = new();
            public List<TypeEntry> Types { get; set; } = new();
        }

        private class Sprites
        {
            public string Front_Default { get; set; } = "";
        }
    }
}