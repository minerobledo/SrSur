using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.Entities;
using SrSur.Repositories;

namespace SrSur.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Responce<bool>> CreateCategory(string name, long? parent, string slug, string descrption)
        {

            if (_categoryRepository.GetByNameAsync(name) == null)
            {
                var category = new Category() { Categoryname = name, Slug = slug, Categorydescription = descrption };
                category.Parentid = (parent != null)? parent.Value : null;
                await _categoryRepository.AddAsync(category);
                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "el nombre del tag ya existe" };
            }
        }

        public async Task<Responce<bool>> EditCategory(long id, string? name, string? slug, string? descrption)
        {
            var tag = await _categoryRepository.GetByIdAsync(id);
            if (tag != null)
            {
                tag!.Categoryname = (tag!.Categoryname != name!) ? name! : tag!.Categoryname;
                tag!.Slug = (tag!.Slug != slug!) ? slug! : tag!.Slug;
                tag!.Categorydescription = (tag!.Categorydescription != descrption!) ? descrption! : tag!.Categorydescription;
                await _categoryRepository.UpdateAsync(tag);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "no existe un tag con ese id" };
            }
        }

        public async Task<Responce<bool>> DeleteCategory(long id)
        {
            var tag = await _categoryRepository.GetByIdAsync(id);
            if (tag != null)
            {
                await _categoryRepository.DeleteAsync(id);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "no existe un tag con ese id" };
            }
        }

        public async Task<Responce<List<Category>>> GetCategory(int page)
        {
            var categories = await _categoryRepository.GetPaginatedAsync(page, 50);
            return new Responce<List<Category>>() { data = categories, succeeded = true };
        }

        public async Task<Responce<List<Category>>> SerchCategory(string prefix, int page)
        {
            var categories = await _categoryRepository.GetByPrefixAsync(prefix, page, 50);
            return new Responce<List<Category>>() { data = categories, succeeded = true };
        }
    }
}
