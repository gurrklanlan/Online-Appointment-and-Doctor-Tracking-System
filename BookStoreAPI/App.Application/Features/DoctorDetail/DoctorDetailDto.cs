namespace App.Application.Features.DoctorDetail
{
    public class DoctorDetailDto
    {
        public int Id { get; set; }
        public string Specialization { get; set; } = null!;
        public int YearsOfExperience { get; set; }
        public int UserId { get; set; }
    }
}
