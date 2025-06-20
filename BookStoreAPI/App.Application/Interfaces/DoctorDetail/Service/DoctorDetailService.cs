using System.Net;
using App.Application.Features.DoctorDetail;
using App.Application.Features.DoctorDetail.Create;
using App.Application.Features.DoctorDetail.Update;
using App.Application.Interfaces.Caching;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace App.Application.Interfaces.DoctorDetail.Service
{
    public class DoctorDetailService : IDoctorDetailService
    {
        private readonly IDoctorDetailRepository _doctorDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly ILogger<App.Domain.Entites.DoctorDetail> _logger;
        public DoctorDetailService(IDoctorDetailRepository doctorDetailRepository, IUnitOfWork unitOfWork, IMapper mapper,
            ICacheService cacheService, ILogger<App.Domain.Entites.DoctorDetail>  logger)
        {
            _doctorDetailRepository = doctorDetailRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cacheService = cacheService;
            _logger = logger;
        }

        private const string DoctorDetailCacheKey = "doctorDetailCacheKey";
        public async Task<ServiceResult<CreateDoctorDetailRequest>> CreateDoctorDetailAsync(CreateDoctorDetailRequest doctorDetail)
        {
            try
            {
                var anyDoctorDetail = _doctorDetailRepository.GetAll()
             .Any(d => d.UserId == doctorDetail.UserId && d.Specialty == doctorDetail.Specialization);
                if (anyDoctorDetail)
                    return ServiceResult<CreateDoctorDetailRequest>.Fail("Doctor detail with the same user and specialty already exists.");
                var doctorDetailAsDto = _mapper.Map<App.Domain.Entites.DoctorDetail>(doctorDetail);
                await _doctorDetailRepository.AddAsync(doctorDetailAsDto);
                await _unitOfWork.SaveChangesAsync();
                return ServiceResult<CreateDoctorDetailRequest>.Success(doctorDetail);

            }
            catch (Exception)
            {

                _logger.LogError("Hataaaaa!!!!!");
                return ServiceResult<CreateDoctorDetailRequest>.Fail("hata");
            }
         


        }

        public async Task<ServiceResult<DoctorDetailDto>> DeleteDoctorDetailAsync(int doctorDetailId)
        {
            var cachedDelete =  _cacheService.RemoveAsync(doctorDetailId.ToString());
            if (cachedDelete != null)
                return ServiceResult<DoctorDetailDto>.Fail("Silmek istediğiniz detay bulunuamadı");

           var doc = await _doctorDetailRepository.GetByIdAsync(doctorDetailId);
            if (doc == null)
                return ServiceResult<DoctorDetailDto>.Fail("Doctor detail not found", HttpStatusCode.NotFound);
            await Task.Run(() => _doctorDetailRepository.DeleteAsync(doc));
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.RemoveAsync(DoctorDetailCacheKey.ToString());
            return ServiceResult<DoctorDetailDto>.Success(_mapper.Map<DoctorDetailDto>(doc));
        }

        public async Task<ServiceResult<List<DoctorDetailDto>>> GetAllDoctorDetailsAsync()
        {
            var doctorDetailsAsCached =await _cacheService.GetAsync<List<DoctorDetailDto>>(DoctorDetailCacheKey);
            if(doctorDetailsAsCached is not null)
                return ServiceResult<List<DoctorDetailDto>>.Success(doctorDetailsAsCached, HttpStatusCode.OK);

            var getDoctorDetails = _doctorDetailRepository.GetAll().ToList();
            if (getDoctorDetails == null || !getDoctorDetails.Any())
                return ServiceResult<List<DoctorDetailDto>>.Fail("No doctor details found", HttpStatusCode.NotFound);

            await _cacheService.AddAsync(DoctorDetailCacheKey, getDoctorDetails,TimeSpan.FromMinutes(10));
            var doctorDetailDtos = _mapper.Map<List<DoctorDetailDto>>(getDoctorDetails);
            return ServiceResult<List<DoctorDetailDto>>.Success(doctorDetailDtos);

        }

        public async Task<ServiceResult<DoctorDetailDto>> GetDoctorDetailByIdAsync(int doctorDetailId)
        {
            
            var doctorDetail = await _doctorDetailRepository.GetByIdAsync(doctorDetailId);
            if (doctorDetail == null)
                return ServiceResult<DoctorDetailDto>.Fail("Doctor detail not found", HttpStatusCode.NotFound);
            var doctorDetailDto = _mapper.Map<DoctorDetailDto>(doctorDetail);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<DoctorDetailDto>.Success(doctorDetailDto);

        }

        public async Task<ServiceResult<DoctorDetailDto>> GetDoctorDetailBySpecialtyAsync(string specialty)
        {
            var entity = await _doctorDetailRepository.GetDoctorDetailBySpecialtyAsync(specialty);

            if (entity is null)
                return ServiceResult<DoctorDetailDto>.Fail("Doktor bulunamadı.");

            var dto = _mapper.Map<DoctorDetailDto>(entity);
            return ServiceResult<DoctorDetailDto>.Success(dto);
        }

      

        public async Task<ServiceResult<UpdateDoctorDetailRequest>> UpdateDoctorDetailAsync(UpdateDoctorDetailRequest doctorDetail)
        {
            var doctorDetailAsDto = _mapper.Map<App.Domain.Entites.DoctorDetail>(doctorDetail);
            var existingDoctorDetail = await _doctorDetailRepository.GetByIdAsync(doctorDetailAsDto.Id);
            if (existingDoctorDetail == null)
                return ServiceResult<UpdateDoctorDetailRequest>.Fail("Doctor detail not found", HttpStatusCode.NotFound);
            existingDoctorDetail.UserId = doctorDetailAsDto.UserId;
            existingDoctorDetail.Specialty = doctorDetailAsDto.Specialty;
            existingDoctorDetail.ClinicAddress = doctorDetailAsDto.ClinicAddress;
            existingDoctorDetail.User = doctorDetailAsDto.User;

             
            _doctorDetailRepository.UpdateAsync(existingDoctorDetail);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<UpdateDoctorDetailRequest>.Success(doctorDetail);
        }
    }
}
