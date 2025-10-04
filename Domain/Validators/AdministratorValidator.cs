using AutoTrackMinimalApi.Domain.Entity;
using FluentValidation;

namespace AutoTrackMinimalApi.Domain.Validators
{
    public class AdministratorValidator : AbstractValidator<Administrator>
    {
        public AdministratorValidator()
        {
            RuleFor(admin => admin.Email)
            .NotEmpty().WithMessage("Email não pode ser vazio")
            .NotNull().WithMessage("Email nao pode ser nulo")
            .EmailAddress().WithMessage("Email inválido")
            .Length(255).WithMessage("Tamanho de caracteres excedido");

            RuleFor(admin => admin.Password)
            .NotEmpty().WithMessage("Senha nao pode ser vazia")
            .NotNull().WithMessage("Senha nao pode ser nula")
            .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres")
            .MaximumLength(50).WithMessage("Senha deter ter no máximo 50 caracteres");

            RuleFor(admin => admin.Profile)
            .NotEmpty().WithMessage("Perfil nao pode ser vazio")
            .NotNull().WithMessage("Perfil nao pode ser nulo")
            .MaximumLength(10).WithMessage("Tamanho de caracteres excedido");
        }
    }
}