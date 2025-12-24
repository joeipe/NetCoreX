using NetCoreX.ViewModel;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreX.Data.Commands
{
    public class Commands
    {
        public record ContactSaveCommand(ContactVM Contact) : ICommand { }
        public record ContactDeleteCommand(int Id) : ICommand { }
    }
}
