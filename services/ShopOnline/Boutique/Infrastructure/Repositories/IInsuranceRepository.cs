using Boutique.Domain.Insurances;

namespace Boutique.Infrastructure.Repositories
{
    public interface IInsuranceRepository
    {
        string Create(Insure insure);
    }
}