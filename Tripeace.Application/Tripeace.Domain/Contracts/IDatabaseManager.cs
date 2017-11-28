using System.Threading.Tasks;

namespace Tripeace.Domain.Contracts
{
    public interface IDatabaseManager
    {
        Task SeedRoles();
    }
}
