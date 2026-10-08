using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly ApplicationDbContext _context;
        public PokemonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(int categoryId, Pokemon pokemon)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);
            _context.Add(pokemon);
            return Save();
        }

        public bool UpdatePokemon(Pokemon pokemon)
        {
            _context.Update(pokemon);
            return Save();
        }

        public Pokemon? GetPokemonById(int id)
        {
            var pokemon = _context.Pokemons
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (pokemon == null)
            {
                return null;
            }

            return pokemon;
        }

        public Pokemon? GetPokemonByName(string name)
        {
            var pokemon = _context.Pokemons
                .Where(p => p.Name == name)
                .FirstOrDefault();
            if (pokemon == null)
            {
                return null;
            }
            return pokemon;
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var average = _context.Reviews
                        .Where(r => r.Pokemon.Id == pokeId)
                        .Select(r => (decimal)r.Rating)
                        .Average();

            if (average <= 0)
            {
                return 0;
            }

            return average;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.ToList();
        }

        public bool PokemonExist(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
