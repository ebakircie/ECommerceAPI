using ECommerceAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceAPI.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<CreateProductVM>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
                .NotNull()
                    .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                    .WithMessage("Ürün adı 2 ile 150 karakter olmak zorundadır.")
                .MinimumLength(2)
                    .WithMessage("Ürün adı 2 ile 150 karakter olmak zorundadır.");

            RuleFor(x => x.Stock)
                .NotEmpty()
                    .WithMessage("Stok bilgisi boş geçilemez.")
                .NotNull()
                    .WithMessage("Stok bilgisi boş geçilemez.")
                .Must(x => x >= 0)
                    .WithMessage("Stok bilgisi negatif olamaz!");

            RuleFor(x => x.Price)
                .NotEmpty()
                    .WithMessage("Fiyat bilgisi boş geçilemez.")
                .NotNull()
                    .WithMessage("Fiyat bilgisi boş geçilemez.")
                .Must(x => x >= 0)
                    .WithMessage("Fiyat bilgisi negatif olamaz!");
        }
    }
}
