using AutoMapper;
using Microsoft.Extensions.Logging;
using NetCoreX.Data.Repositories.Interfaces;
using SharedKernel.Interfaces;
using static NetCoreX.Data.Commands.Commands;

namespace NetCoreX.Data.CommandHandlers
{
    public class ContactDeleteCommandHandler : ICommandHandler<ContactDeleteCommand>
    {
        private readonly ILogger<ContactSaveCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public ContactDeleteCommandHandler(
            ILogger<ContactSaveCommandHandler> logger,
            IMapper mapper,
            IContactRepository contactRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task Handle(ContactDeleteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Handler}.{Action} start", nameof(ContactDeleteCommandHandler), nameof(Handle));

            var data = await _contactRepository.FindAsync(request.Id);
            if (data != null)
            {
                _contactRepository.Delete(data);
                await _contactRepository.SaveAsync();
            }
        }
    }
}