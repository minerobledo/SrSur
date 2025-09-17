using Microsoft.AspNetCore.Mvc.RazorPages;
using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.Imput.Tag;
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

        public async Task<Responce<bool>> CreateTag(TagDto t)
        {
            var tag = await _tagRepository.GetByNameAsync(t.TagName);
            if (tag == null)
            {
                var newTag = new Tag() { Tagname = t.TagName, Slug = t.Slug, Tagdescription = t.TagDescription};
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
                tag!.Tagname = (tag!.Tagname != tagName!) ? tagName! : tag!.Tagname;
                tag!.Slug = (tag!.Slug != slug!) ? slug! : tag!.Slug;
                tag!.Tagdescription = (tag!.Tagdescription != descrption!) ? descrption! : tag!.Tagdescription;
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
