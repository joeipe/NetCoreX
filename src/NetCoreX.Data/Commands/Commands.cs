using NetCoreX.ViewModel;
using SharedKernel.Interfaces;

namespace NetCoreX.Data.Commands
{
    public class Commands
    {
        public record ContactSaveCommand(ContactVM Contact) : ICommand { }
        public record ContactDeleteCommand(int Id) : ICommand { }
    }
}