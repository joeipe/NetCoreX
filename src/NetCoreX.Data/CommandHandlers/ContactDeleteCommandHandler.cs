using AutoMapper;
using Microsoft.Extensions.Logging;
using NetCoreX.Data.Repositories.Interfaces;
using SharedKernel.Interfaces;
using static NetCoreX.Data.Commands.Commands;

namespace NetCoreX.Data.CommandHandlers
{
    public class ContactDeleteCommandHandler : ICommandHandler<ContactDeleteCommand>
    {
        private readonly ILogger<ContactDeleteCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public ContactDeleteCommandHandler(
            ILogger<ContactDeleteCommandHandler> logger,
            IMapper mapper,
            IContactRepository contactRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task Handle(ContactDeleteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Handler}.{Action} start for contact ID: {ContactId}", 
                nameof(ContactDeleteCommandHandler), nameof(Handle), request.Id);

            var data = await _contactRepository.FindAsync(request.Id);
            if (data != null)
            {
                _logger.LogWarning("Deleting contact ID: {ContactId} - Contact Name: {FirstName} {LastName}", 
                    data.Id, data.FirstName, data.LastName);
                    
                _contactRepository.Delete(data);
                await _contactRepository.SaveAsync();
                
                _logger.LogInformation("Successfully deleted contact ID: {ContactId}", request.Id);
            }
            else
            {
                _logger.LogWarning("Attempted to delete non-existent contact ID: {ContactId}", request.Id);
            }
        }
    }
}