using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.Imput.Category;
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
        public async Task<Responce<bool>> CreateCategory(string name, long? parent, string? slug, string descrption)
        {
            var cat = await _categoryRepository.GetByNameAsync(name);
            if (cat == null)
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

        public async Task<Responce<bool>> EditCategory(EditCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.Id);
            if (category != null)
            {
                category!.Categoryname =  dto.CategoryName! ?? category!.Categoryname;
                category!.Slug = dto.Slug! ?? category!.Slug;
                category!.Categorydescription = dto.CategoryDescription! ?? category!.Categorydescription;
                category!.Createdate = DateTime.SpecifyKind(category.Createdate, DateTimeKind.Utc);
                category.Lasteditdate = DateTime.UtcNow; 

                await _categoryRepository.UpdateAsync(category);

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
