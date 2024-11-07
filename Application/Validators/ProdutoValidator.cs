using FluentValidation;
using ProdutoAPI.Models.Entities;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        RuleFor(produto => produto.Nome)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do produto não pode exceder 100 caracteres.");

        RuleFor(produto => produto.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.")
            .NotNull().WithMessage("O preço é obrigatório.");

        RuleFor(produto => produto.Nome)
            .Must(nome => NomeUnico(nome))
            .WithMessage("Já existe um produto com este nome.");
    }

    private bool NomeUnico(string nome)
    {
        return true;  
    }
}