using Boutique.Domain.Insure.Policy;

namespace Boutique.Infrastructure.Repositories
{
    public interface IInsuranceRepository
    {
        string Create(Insurance insure);
    }
}