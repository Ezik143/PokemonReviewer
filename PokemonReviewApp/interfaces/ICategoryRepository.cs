using PokemonReviewApp.Models.Dto;
using PokemonReviewApp.Models.Entities;

namespace PokemonReviewApp.interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category? GetCategory(int id);
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool CategoryExist(int id);
        bool CreateCategory(Category category);
        bool Save();
    }
}
