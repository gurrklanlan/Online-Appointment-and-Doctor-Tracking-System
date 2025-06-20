using FluentValidation;

namespace App.Application.Features.DoctorDetail.Create
{
    public class CreateDoctorDetailRequestValidator:AbstractValidator<CreateDoctorDetailRequest>
    {
        public CreateDoctorDetailRequestValidator()        
       {
            RuleFor(x=>x.UserId).NotEmpty()
                .WithMessage("Kullanıcı ID boş olmamalı");

            RuleFor(x => x.Specialization).NotEmpty()
                .WithMessage("Uzmanlık alanı boş olmamalı");

            RuleFor(x => x.YearsOfExperience)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Deneyim yılı 0 veya daha büyük olmalıdır");
         
        }
    }
}
