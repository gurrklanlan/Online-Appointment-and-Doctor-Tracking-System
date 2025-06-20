namespace App.Application.Features.DoctorDetail.Create
{
    public class CreateDoctorDetailRequest
    {
        public string Specialization { get; set; } = null!;
        public int YearsOfExperience { get; set; }
        public int UserId { get; set; }
    }
}
