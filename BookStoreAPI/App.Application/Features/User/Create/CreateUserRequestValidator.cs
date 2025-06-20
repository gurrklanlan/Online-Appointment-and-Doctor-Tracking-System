using FluentValidation;

namespace App.Application.Features.User.Create
{
    public class CreateUserRequestValidator:AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotNull()
                .WithMessage("Ad ve soyad boş olmamalı");

            RuleFor(x=>x.Email)
                .NotEmpty()
                .WithMessage("E-posta boş olmamalı")
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi girin");

            RuleFor(x=>x.Password)
                .NotEmpty()
                .WithMessage("Parola boş olmamalı")
                .MinimumLength(6)
                .WithMessage("Parola en az 6 karakter olmalı");

        }
    }
}
