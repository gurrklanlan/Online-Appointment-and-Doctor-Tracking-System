using FluentValidation;

namespace App.Application.Features.DoctorDetail.Update
{
    public class UpdateDoctorDetailRequestValidator:AbstractValidator<UpdateDoctorDetailRequest>
    {
        public UpdateDoctorDetailRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("Doktor Detay ID boş olmamalı");
            RuleFor(x => x.Specialization).NotEmpty()
                .WithMessage("Uzmanlık alanı boş olmamalı");
            RuleFor(x => x.YearsOfExperience)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Deneyim yılı 0 veya daha büyük olmalıdır");
            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage("Kullanıcı ID boş olmamalı");
        }
    }
    
}
