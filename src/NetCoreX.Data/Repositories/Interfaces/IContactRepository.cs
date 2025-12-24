using NetCoreX.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreX.Data.Repositories.Interfaces
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<List<Contact>> GetContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
    }
}
