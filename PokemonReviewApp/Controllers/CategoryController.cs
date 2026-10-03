using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.interfaces;
using PokemonReviewApp.Mappers;
using PokemonReviewApp.Models.Dto;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryMapper _categoryMapper;
        private readonly PokemonMapper _pokemonMapper;
        public CategoryController(ICategoryRepository categoryRepository, CategoryMapper categoryMapper, PokemonMapper pokemonMapper)
        {
            _categoryRepository = categoryRepository;
            _categoryMapper = categoryMapper;
            _pokemonMapper = pokemonMapper;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();

            var categoriesDto = _categoryMapper.ToDtoList(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExist(id))
            {
                return NotFound("Not Found");
            }

            var category = _categoryRepository.GetCategory(id)!;
            var categoryDto = _categoryMapper.categoryDto(category);
            return Ok(categoryDto);
        }

        [HttpGet("{categoryId}/PokemonCategory")]
        public IActionResult GetPokemonByCategory(int categoryId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExist(categoryId))
            {
                return NotFound("Not found");
            }
            var pokemonByCategory = _categoryRepository.GetPokemonByCategory(categoryId);

            var pokemonDto = _pokemonMapper.ToDtoList(pokemonByCategory);
            return Ok(pokemonDto);
        }

    }
}
