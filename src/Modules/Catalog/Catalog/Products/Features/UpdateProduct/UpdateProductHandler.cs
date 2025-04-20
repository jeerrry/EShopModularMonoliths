namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id)
            .NotEmpty()
            .WithMessage("Product ID is required.");
        RuleFor(x => x.Product.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");
        RuleFor(x => x.Product.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero.");
    }
}

public class UpdateProductHandler(CatalogDbContext dbContext)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync([command.Product.Id], cancellationToken: cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }

        UpdateProductWithNewValues(product, command.Product);

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }

    private void UpdateProductWithNewValues(Product product, ProductDto commandProduct)
    {
        product.Update(
            commandProduct.Name,
            commandProduct.Description,
            commandProduct.Category,
            commandProduct.Price,
            commandProduct.ImageFile);
    }
}