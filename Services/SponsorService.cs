using AutoMapper;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using SrSur.Interface;
using SrSur.Models.DTOs;
using SrSur.Models.DTOs.Imput.Sponsor;
using SrSur.Models.Entities;
using SrSur.Repositories;

namespace SrSur.Services
{
    public class SponsorService
    {
        private readonly ICloudflareRepocitory _cloudflareRepocitory;
        private readonly ISponsorImageRepocitory _sponsorImageRepocitory;
        private readonly ISponcorRepository _sponcorRepository;
        private readonly IMapper _mapper;

        public SponsorService(ICloudflareRepocitory cloudflareRepocitory, ISponsorImageRepocitory sponsorImageRepocitory, ISponcorRepository sponcorRepository, IMapper mapper)
        {
            _cloudflareRepocitory = cloudflareRepocitory;
            _sponsorImageRepocitory = sponsorImageRepocitory;
            _sponcorRepository = sponcorRepository;
            _mapper = mapper;
        }

        public async Task<Responce<bool>> CreateSponsor(CreateSponsorDto dto)
        {
            var r = await _cloudflareRepocitory.UploadImage(dto.FormFile);
            if(r.Success == true)
            { 
                await _sponcorRepository.CreateSponsor(_mapper.Map<Sponsor>(dto));
                var sponsor = _sponcorRepository.GetSponsorByname(dto.Name);
                var result = r.Result;

                await _sponsorImageRepocitory.CreateAsync(new SponsorImage
                {
                    CloudflareId = result.Id,
                    FileName = result.Filename,
                    Variants = result.Variants,
                    SponsorID = sponsor.Id,
                });
                return new Responce<bool> { data = true,succeeded = true };
            }
            return new Responce<bool> {data = false,succeeded = false,message= "Claudflare no responde "};
        }
        public async Task<Responce<bool>> UpdateSponsor(UpdateSponsorDto dto)
        {
            var spon = await _sponcorRepository.GetSponsorById(dto.id);
            if (spon != null)
            {
                spon!.Name = (spon!.Name != dto.Name!) ? dto.Name! : spon!.Name;
                spon!.Amount = (spon!.Amount != dto.Amount!) ? dto.Amount! : spon!.Amount;
                
                await _sponcorRepository.UpdateSponsor(spon);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            return new Responce<bool>() { data = false, succeeded = true };
        }
        public async Task<Responce<bool>> DeleteSponsor(long id)
        {
            return new Responce<bool> { data = await _sponcorRepository.DeleteSponsor(id), succeeded = true };
        }
        public async Task<Responce<List<Sponsor>>> GetSponsors()
        {
            return new Responce<List<Sponsor>> { data = await _sponcorRepository.GetSponsors(), succeeded = true };
        }
        public async Task<Responce<bool>> ChangeSponsorPaid(long id)
        {
            var spon = await _sponcorRepository.GetSponsorById(id);
            if (spon != null)
            {
                if (spon.Paid == true)
                {
                    spon.Paid = false;
                    spon.LastPayment = DateTime.UtcNow;
                }
                else spon.Paid = true;

                await _sponcorRepository.UpdateSponsor(spon);

                return new Responce<bool>() { data = true, succeeded = true };
            }
            return new Responce<bool>() { data = false, succeeded = true };
        }

    }
}
