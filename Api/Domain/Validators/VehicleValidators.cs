using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoTrackMinimalApi.Domain.Entity;
using FluentValidation;

namespace AutoTrackMinimalApi.Domain.Validators
{
    public class VehicleValidators : AbstractValidator<Vehicle>
    {
        public VehicleValidators()
        {
            RuleFor(vehicle => vehicle.Name)
            .NotEmpty().WithMessage("Nome nao pode ser vazio")
            .NotNull().WithMessage("Nome nao pode ser nulo")
            .MaximumLength(150).WithMessage("Tamanho de caracteres excedido");

            RuleFor(vehicle => vehicle.Model)
            .NotEmpty().WithMessage("Modelo nao pode ser vazio")
            .NotNull().WithMessage("Modelo nao pode ser nulo")
            .MaximumLength(50).WithMessage("Tamanho de caracteres excedido");

            RuleFor(Vehicle => Vehicle.YearFabrication)
            .NotEmpty().WithMessage("Ano nao pode ser vazio")
            .NotNull().WithMessage("Ano nao pode ser nulo")
            .InclusiveBetween(1900, DateTime.Now.Year).WithMessage("Ano nao pode ser menor que 1900 ou maior que o ano atual");
        }
    }
}