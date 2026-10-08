using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon? GetPokemonById(int id);
        Pokemon? GetPokemonByName(string name);
        decimal GetPokemonRating(int pokeId);
        bool PokemonExist(int pokeId);
        bool CreatePokemon(int categoryId, Pokemon pokemon);
        bool UpdatePokemon(Pokemon pokemon);
        bool Save();
    }
}
