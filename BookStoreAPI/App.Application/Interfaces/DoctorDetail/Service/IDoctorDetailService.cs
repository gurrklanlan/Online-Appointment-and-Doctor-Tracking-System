using App.Application.Features.DoctorDetail;
using App.Application.Features.DoctorDetail.Create;
using App.Application.Features.DoctorDetail.Update;

namespace App.Application.Interfaces.DoctorDetail.Service
{
    public interface IDoctorDetailService
    {
        Task<ServiceResult<List<DoctorDetailDto>>> GetAllDoctorDetailsAsync();
        Task<ServiceResult<DoctorDetailDto>> GetDoctorDetailByIdAsync(int doctorDetailId);
        Task<ServiceResult<CreateDoctorDetailRequest>> CreateDoctorDetailAsync(CreateDoctorDetailRequest doctorDetail);
        Task<ServiceResult<UpdateDoctorDetailRequest>> UpdateDoctorDetailAsync(UpdateDoctorDetailRequest doctorDetail);
        Task<ServiceResult<DoctorDetailDto>> DeleteDoctorDetailAsync(int doctorDetailId);
        Task<ServiceResult<DoctorDetailDto>> GetDoctorDetailBySpecialtyAsync(string specialty);




    }
}
