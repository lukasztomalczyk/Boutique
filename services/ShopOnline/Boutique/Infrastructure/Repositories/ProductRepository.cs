using Boutique.Domain;
using Boutique.Infrastructure.Attributes;
using Boutique.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

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

        public string Load(string Id)
        {
            var rawProducts = _sqlConnection.ExecuteQuery($"SELECT * FROM Products WHERE Id = '{Id}'");

            var product = new Products(
                                (string)rawProducts.Id,
                                (string)rawProducts.Name,
                                (string)rawProducts.Color);

            return product.Id;
        }

        public void Save(Products product)
        {
            throw new NotImplementedException();
        }
    }
}
