using IntegrationWithKafka.Models;

namespace IntegrationWithKafka.Repositories
{
    public interface IPaymentRepository
    {
        Task InsertTeam(Payments payments);
        Task UpdateTeam(Payments payments);
        Task DeleteTeam(string id);
        Task<IEnumerable<Payments>> GetAllPayments();
        Task<Payments> getPaymentById(string id);
    }
}
