using Boutique.Domain.Interface;
using Boutique.Domain.Products;
using Cqrs.Attributes;
using SqlServices.Dapper;
using System.Data.SqlClient;

namespace Boutique.Infrastructure.Repositories
{
    [Repository]
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _sqlConnection;

        public ProductRepository(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public string Load(string id)
        {
            var rawProducts = _sqlConnection.ExecuteQuery($"SELECT Id FROM Products WHERE Id = '{id}'");

            var product = new Products(
                                (string)rawProducts.Id,
                                (string)rawProducts.Name,
                                (string)rawProducts.Color);

            return product.Id;
        }

        public void Save(Products product)
        {
            _sqlConnection.ExecuteQuery($"INSERT INTO Products (Id, Name, Color, Price)" +
                                                    $"VALUES ('{product.Id}', '{product.Name}', '{product.Color}', '{product.Price}');");
        }
    }
}
