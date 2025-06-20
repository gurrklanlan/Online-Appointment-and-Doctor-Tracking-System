using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace App.Application.Features.MedicalRecord.Update
{
    public class UpdateMedicalRecorRequestValidator:AbstractValidator<UpdateMedicalRecordRequest>
    {
        public UpdateMedicalRecorRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Tıbbi kayıt ID boş olmamalı");
            RuleFor(x => x.Diagnosis)
                .NotEmpty()
                .WithMessage("Tanı boş olmamalı");
            RuleFor(x => x.Treatment)
                .NotEmpty()
                .WithMessage("Tedavi boş olmamalı");
            RuleFor(x => x.AppointmentId)
                .NotEmpty()
                .WithMessage("Randevu ID boş olmamalı");
        }
    }
}
