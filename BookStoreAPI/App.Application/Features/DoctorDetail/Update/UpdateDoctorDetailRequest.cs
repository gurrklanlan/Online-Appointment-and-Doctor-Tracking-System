namespace App.Application.Features.DoctorDetail.Update
{
    public class UpdateDoctorDetailRequest
    {
        public int Id { get; set; }
        public string? Specialization { get; set; }
        public int? YearsOfExperience { get; set; }
        public int? UserId { get; set; }
    }
}
