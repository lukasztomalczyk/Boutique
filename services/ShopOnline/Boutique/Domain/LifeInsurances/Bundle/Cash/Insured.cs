using System;
using System.Net.Sockets;

namespace Boutique.Domain.LifeInsurances.Bundle.Cash
{
    public class Insured
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string IdCard { get; set; }
        public string Nationality { get; set; }
        public DateTime Birthday { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
    }
}