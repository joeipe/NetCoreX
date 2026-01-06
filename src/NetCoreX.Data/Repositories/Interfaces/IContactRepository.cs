using NetCoreX.Domain;

namespace NetCoreX.Data.Repositories.Interfaces
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<List<Contact>> GetContactsAsync();

        Task<Contact> GetContactByIdAsync(int id);
    }
}