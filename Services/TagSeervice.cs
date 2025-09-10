using Microsoft.AspNetCore.Mvc.RazorPages;
using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.Entities;

namespace SrSur.Services
{
    public class TagSeervice
    {
        private readonly ITagRepository _tagRepository;
        
        public TagSeervice(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Responce<bool>> CreateTag(string TagName, string slug, string descrption)
        {

            if (_tagRepository.GetByNameAsync(TagName) == null)
            {
                var newTag = new Tag() { TagName = TagName, Slug = slug, TagDescription = descrption };
                await _tagRepository.AddAsync(newTag);
                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "el nombre del tag ya existe" };
            }
        }

        public async Task<Responce<bool>> EditTag(long id, string? tagName, string? slug, string? descrption)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag != null)
            {
                tag!.TagName = (tag!.TagName != tagName!) ? tagName! : tag!.TagName;
                tag!.Slug = (tag!.Slug != slug!) ? slug! : tag!.Slug;
                tag!.TagDescription = (tag!.TagDescription != descrption!) ? descrption! : tag!.TagDescription;
                await _tagRepository.UpdateAsync(tag);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "no existe un tag con ese id" };
            }
        }

        public async Task<Responce<bool>> DeleteTag(long id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag != null)
            {
                await _tagRepository.DeleteAsync(id);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            else
            {
                return new Responce<bool>() { data = true, succeeded = true, message = "no existe un tag con ese id" };
            }
        }

        public async Task<Responce<List<Tag>>> GetTags(int page)
        {
            var tags = await _tagRepository.GetPaginatedAsync(page, 50);
            return new Responce<List<Tag>>() { data = tags, succeeded = true };
        }

        public async Task<Responce<List<Tag>>> SerchTag(string prefix, int page)
        {
            var tags = await _tagRepository.GetByPrefixAsync(prefix, page, 50);
            return new Responce<List<Tag>>() { data = tags, succeeded = true };
        }
    }
}
