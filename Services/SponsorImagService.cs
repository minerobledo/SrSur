using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.Imput.SponsorImage;
using SrSur.Models.Entities;

namespace SrSur.Services
{
    public class SponsorImagService
    {
        private readonly ISponsorImageRepocitory _imageRepocitory;
        private readonly ICloudflareRepocitory _cloudflareRepocitory;

        public SponsorImagService(ISponsorImageRepocitory imageRepocitory, ICloudflareRepocitory cloudflareRepocitory)
        {
            _imageRepocitory = imageRepocitory;
            _cloudflareRepocitory = cloudflareRepocitory;
        }

        public async Task<Responce<bool>> CreateSponsorImage(CreateSponsorImageDto dto)
        {
            var cloudResponce = await _cloudflareRepocitory.UploadImage(dto.FormFile);
            if (cloudResponce.Success)
            {
                var image = new SponsorImage
                {
                    SponsorID = dto.SponsorID,
                    CloudflareId = cloudResponce.Result.Id,
                    FileName = cloudResponce.Result.Filename,
                    Variants = cloudResponce.Result.Variants,
                };
                await _imageRepocitory.CreateAsync(image);
                return new Responce<bool> { data = true, succeeded = true };
            }
            return new Responce<bool> { data = false, succeeded = true };
        }
        public async Task<Responce<bool>> DeleteSponsorImage(long id)
        {
            var image = await _imageRepocitory.GetAsync(id);
            if (image != null)
            {
                var cloudResponce = await _cloudflareRepocitory.DeleteImageAsync(image.CloudflareId);
                if (cloudResponce == true)
                {
                    await _imageRepocitory.DeleteAsync(id);
                    return new Responce<bool> { succeeded = true, data = true };
                }
            }
            return new Responce<bool> { succeeded =true, data = false };
        }
        public async Task<Responce<SponsorImage>> UpdateSponsorImage(EditSponsorImageDto dto)
        {
            var image = await _imageRepocitory.GetAsync(dto.imageId);
            if (image != null)
            {
                var deleteResponce = await _cloudflareRepocitory.DeleteImageAsync(image.CloudflareId);
                if (deleteResponce == true)
                {
                    await _imageRepocitory.DeleteAsync(image.Id);
                    var cloudResponce = await _cloudflareRepocitory.UploadImage(dto.FormFile);
                    if(cloudResponce != null)
                    {
                        image .CloudflareId= cloudResponce.Result.Id;
                        image.FileName = cloudResponce.Result.Filename;
                        image.Variants = cloudResponce.Result.Variants;
                        await _imageRepocitory.UpdateSponsor(image);
                        image = await _imageRepocitory.GetAsync(dto.imageId);
                        return new Responce<SponsorImage> { succeeded = true, data = image };
                    }
                    return new Responce<SponsorImage> { succeeded = true, data = null, message = "no se puede suvir una nueva foto en cloudflare" };
                }
                return new Responce<SponsorImage> { succeeded = true, data = null ,message = "no se puede eliminar la foto en cloudflare"};
            }
            return new Responce<SponsorImage> { succeeded = true, data = null, message = "id inexistente" };
        }

        public async Task<Responce<List<SponsorImage>>> GetAllSponsorImage(int page)
        {
            var image = await _imageRepocitory.GetAllPagitatedAsync(page,50);
            
            return new Responce<List<SponsorImage>> { succeeded = true, data = image};
        }
        public async Task<Responce<List<SponsorImage>>> GetRandomSponsorImage(int count)
        {
            var image = await _imageRepocitory.GetRandomAsync(count);

            return new Responce<List<SponsorImage>> { succeeded = true, data = image };
        }
    }
}
