using Boutique.Domain.Insure.Policy;

namespace Boutique.Infrastructure.Repositories
{
    public interface IInsuranceRepository
    {
        string Create(Insure insure);
    }
}