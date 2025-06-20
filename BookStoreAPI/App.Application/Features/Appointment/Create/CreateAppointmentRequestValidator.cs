using FluentValidation;

namespace App.Application.Features.Appointment.Create
{
    public class CreateAppointmentRequestValidator:AbstractValidator<CreateAppointmentRequest>
    {
        public CreateAppointmentRequestValidator()
        {
            RuleFor(x => x.DoctorId).NotEmpty()
                .WithMessage("Doktor seçimi boş olmamalı");

            RuleFor(x => x.PatientId).NotEmpty()
                .WithMessage("Hasta seçimi boş olmamalı");

            RuleFor(x => x.AppointmentDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("Randevu tarihi geçmiş bir tarih olamaz. Lütfen gelecekteki bir tarih seçiniz.");



        }
    }
}
