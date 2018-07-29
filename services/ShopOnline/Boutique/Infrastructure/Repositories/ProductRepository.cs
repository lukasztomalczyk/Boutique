using Boutique.Domain;
using Boutique.Infrastructure.Attributes;
using Boutique.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Boutique.Domain.Products;

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
