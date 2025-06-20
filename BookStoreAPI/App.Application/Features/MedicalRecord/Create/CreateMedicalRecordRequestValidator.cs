using FluentValidation;

namespace App.Application.Features.MedicalRecord.Create
{
    public class CreateMedicalRecordRequestValidator:AbstractValidator<CreateMedicalRecordRequest>
    {
        public CreateMedicalRecordRequestValidator()
        {
            RuleFor(x=>x.Diagnosis)
                .NotEmpty()
                .WithMessage("Tanı boş olmamalı");

            RuleFor(x => x.Treatment)
                .NotEmpty()
                .WithMessage("Tedavi boş olmamalı");

            RuleFor(x=>x.AppointmentId)
                .NotEmpty()
                .WithMessage("Randevu ID boş olmamalı");

           


        }
    }
}
