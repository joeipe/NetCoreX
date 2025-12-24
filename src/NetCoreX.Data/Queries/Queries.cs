using NetCoreX.ViewModel;
using SharedKernel.Interfaces;

namespace NetCoreX.Data.Queries
{
    public class Queries
    {
        public record GetContactsQuery() : IQuery<List<ContactVM>> { }
        public record GetContactByIdQuery(int Id) : IQuery<ContactVM> { }
    }
}