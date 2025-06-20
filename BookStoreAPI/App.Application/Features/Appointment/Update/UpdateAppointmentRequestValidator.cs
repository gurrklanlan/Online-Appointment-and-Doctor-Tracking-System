using FluentValidation;

namespace App.Application.Features.Appointment.Update
{
    public class UpdateAppointmentRequestValidator: AbstractValidator<UpdateAppointmentRequest>
    {
        public UpdateAppointmentRequestValidator()
        {
            RuleFor(x=>x.Id).NotEmpty()
                .WithMessage("Randevu ID boş olmamalı");

            RuleFor(x => x.DoctorId).NotEmpty()
                .WithMessage("Doktor seçimi boş olmamalı");

            RuleFor(x => x.PatientId).NotEmpty()
                .WithMessage("Hasta seçimi boş olmamalı");

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Randevu tarihi geçmiş bir tarih olamaz. Lütfen gelecekteki bir tarih seçiniz.");

            RuleFor(x => x.AppointmentDate)
                .LessThan(DateTime.Now.AddYears(1))
                .WithMessage("Randevu tarihi 1 yıl sonrasından daha ileri bir tarih olamaz. Lütfen 1 yıl içinde bir tarih seçiniz.");

            RuleFor(x => x.Status)
                .Must(status => status == "Scheduled" || status == "Completed" || status == "Cancelled")
                .WithMessage("Randevu durumu 'Scheduled', 'Completed' veya 'Cancelled' olmalıdır.");

        }
    }
}
