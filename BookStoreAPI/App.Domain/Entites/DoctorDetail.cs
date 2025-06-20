namespace App.Domain.Entites
{
    public class DoctorDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Specialty { get; set; } = null!;
        public string ClinicAddress { get; set; } = null!;

        // Navigation
        public User User { get; set; } = null!;
    }

}
