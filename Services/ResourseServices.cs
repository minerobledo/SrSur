using AutoMapper;
using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.Imput.resourse;
using SrSur.Models.Entities;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SrSur.Services
{
    public class ResourseServices
    {
        private readonly IResourseRepocitory _resourse;
        private readonly ICloudflareRepocitory _cloudflareRepocitory;

        public ResourseServices(IResourseRepocitory resourse, ICloudflareRepocitory cloudflareRepocitory)
        {
            _resourse = resourse;
            _cloudflareRepocitory = cloudflareRepocitory;
        }

        public async Task<Responce<Resourse>> CreateResourse(CreateResourseDto dto)
        {
            var res = await _cloudflareRepocitory.UploadImage(dto.FormFile);
            var image = res.Result;
            if (res.Success == true)
            {
                var resourse = new Resourse
                {
                    Title = dto.Titel,
                    CloudflareId = image.Id,
                    FileName = image.Filename,
                    Uploaded = image.Uploaded,
                    Variants = image.Variants
                };

                await _resourse.CreateResorse(resourse);
                return new Responce<Resourse> { data= resourse, succeeded= true};

            }
            return new Responce<Resourse> { data = null, succeeded = true };
        }

        public async Task<bool> DeleteResorse(long Id)
        {
            var resourse = await _resourse.GetResorseByID(Id);
            if (resourse != null)
            {
                var a = await _cloudflareRepocitory.DeleteImageAsync(resourse.CloudflareId);
                if(a == true)
                {
                    await _resourse.DeleteResorse(Id);
                    return true;
                }
            }
            return false;
        }
        public async Task <Responce<bool>> EditResourse(EditResourseDto dto)
        {
            var resourse = await _resourse.GetResorseByID(dto.Id);
            if (resourse != null)
            {
                resourse.Title =dto.Titel;
                await _resourse.EditResourse(resourse);
                return new Responce<bool> { data = true, succeeded = true };
            }
            return new Responce<bool> { data = true, succeeded = true };
        }
        public async Task<Responce<List<Resourse>>> GetResourses(GetResourseDto dto)
        {

            var a = await _resourse.GetResourseList(dto.Titel, dto.Page, 50);
            return new Responce<List<Resourse>> { data = a, succeeded = true };

        }


    }
    


}
