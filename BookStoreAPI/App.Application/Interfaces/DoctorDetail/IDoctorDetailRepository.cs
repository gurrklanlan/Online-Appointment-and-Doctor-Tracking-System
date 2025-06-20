using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Entites;

namespace App.Application.Interfaces.DoctorDetail
{
    public interface IDoctorDetailRepository:IGenericRepository<App.Domain.Entites.DoctorDetail>
    {
        Task<App.Domain.Entites.DoctorDetail?> GetDoctorDetailBySpecialtyAsync(string specialty);
        Task<App.Domain.Entites.DoctorDetail?> GetDoctorDetailByClinicAddressAsync(int userId);
    }
}
