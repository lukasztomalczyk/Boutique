namespace Boutique.Domain.Products
{
    public interface IProductRepository
    {
        void Save(Domain.Products.Products product);
        string Load(string id);
    }
}
