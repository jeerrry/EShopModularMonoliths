namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");
        RuleFor(x => x.Product.ImageFile)
            .NotEmpty()
            .WithMessage("Product image is required.");
        RuleFor(x => x.Product.Category)
            .NotEmpty()
            .WithMessage("Product category is required.");
        RuleFor(x => x.Product.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero.");
    }
}

public class CreateProductHandler(
    CatalogDbContext dbContext)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command.Product);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto productDto)
    {
        return Product.Create(Guid.NewGuid(), productDto.Name, productDto.Description, productDto.Category,
            productDto.Price, productDto.ImageFile);
    }
}