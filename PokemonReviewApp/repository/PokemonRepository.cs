using PokemonReviewApp.Data;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPokemonRepository _pokemonRepository;
        public PokemonRepository(ApplicationDbContext context, IPokemonRepository pokemonRepository)
        {
            _context = context;
            _pokemonRepository = pokemonRepository;
        }

        public Pokemon GetPokemonById(int id)
        {
            var pokemon = _context.Pokemons
                .Where(p => p.Id == id)
                .FirstOrDefault();

            return pokemon;
        }

        public Pokemon GetPokemonByName(string name)
        {
            var pokemon = _context.Pokemons
                .Where(p => p.Name == name)
                .FirstOrDefault();

            return pokemon;
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);
            
            if(review.Count() <= 0)
            {
                return 0;
            }

            var toReturn = (decimal)review.Sum(r => r.Rating) / review.Count();
                 
            return toReturn; 
        }

        public ICollection<Pokemon> GetPokemons()
        {
            throw new NotImplementedException();
        }

        public bool PokemonExist(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId);
        }
    }
}
