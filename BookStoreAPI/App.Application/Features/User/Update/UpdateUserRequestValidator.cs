using FluentValidation;

namespace App.Application.Features.User.Update
{
    public class UpdateUserRequestValidator: AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Kullanıcı ID boş olmamalı");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Ad ve soyad boş olmamalı")
                .Length(3, 100)
                .WithMessage("Ad ve soyad 3 ile 100 karakter arasında olmalıdır");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-posta boş olmamalı")
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi girin");

            RuleFor(x=>x.Role)
                .NotEmpty()
                .WithMessage("Rol boş olmamalı")
                .Must(role => role == "Admin" || role == "Doctor" || role == "Patient")
                .WithMessage("Rol 'Admin', 'Doctor' veya 'Patient' olmalıdır");
        }
    }
}
