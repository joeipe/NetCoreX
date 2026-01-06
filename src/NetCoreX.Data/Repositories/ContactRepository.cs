using Microsoft.Extensions.Logging;
using NetCoreX.Data.Repositories.Interfaces;
using NetCoreX.Domain;

namespace NetCoreX.Data.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private readonly ILogger<ContactRepository> _logger;
        protected NetCoreXDbContext _dbContext;

        public ContactRepository(
            ILogger<ContactRepository> logger,
            NetCoreXDbContext dbContext)
            : base(dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<List<Contact>> GetContactsAsync()
        {
            _logger.LogInformation("{Repository}.{Action} start", nameof(ContactRepository), nameof(GetContactsAsync));

            var contacts = await GetAllAsync();

            return contacts.ToList();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            _logger.LogInformation("{Repository}.{Action} start", nameof(ContactRepository), nameof(GetContactByIdAsync));

            return await FindAsync(id);
        }
    }
}