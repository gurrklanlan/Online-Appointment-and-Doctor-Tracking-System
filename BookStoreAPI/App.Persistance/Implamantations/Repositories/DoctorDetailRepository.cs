using App.Application.Interfaces.DoctorDetail;
using App.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace App.Persistance.Implamantations.Repositories
{
    public class DoctorDetailRepository(AppDbContext context) : GenericRepository<DoctorDetail>(context), IDoctorDetailRepository
    {
        public async Task<DoctorDetail?> GetDoctorDetailByClinicAddressAsync(int userId)
        {
           return await context.DoctorDetails.AsNoTracking()
                .FirstOrDefaultAsync(x=>x.UserId == userId && !string.IsNullOrEmpty(x.ClinicAddress));

        }

        public async Task<DoctorDetail?> GetDoctorDetailBySpecialtyAsync(string specialty)
        {
            if (string.IsNullOrWhiteSpace(specialty))
                return null;

            return await context.DoctorDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Specialty == specialty);

        }

    }
}
