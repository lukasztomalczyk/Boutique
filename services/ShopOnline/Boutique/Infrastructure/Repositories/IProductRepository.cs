namespace Boutique.Domain.Interface
{
    public interface IProductRepository
    {
        void Save(Domain.Products.Products product);
        string Load(string id);
    }
}
