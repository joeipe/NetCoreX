using AutoMapper;
using Microsoft.Extensions.Logging;
using NetCoreX.Data.Repositories.Interfaces;
using NetCoreX.Domain;
using SharedKernel.Interfaces;
using static NetCoreX.Data.Commands.Commands;

namespace NetCoreX.Data.CommandHandlers
{
    public class ContactSaveCommandHandler : ICommandHandler<ContactSaveCommand>
    {
        private readonly ILogger<ContactSaveCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public ContactSaveCommandHandler(
            ILogger<ContactSaveCommandHandler> logger,
            IMapper mapper,
            IContactRepository contactRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task Handle(ContactSaveCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Handler}.{Action} start", nameof(ContactDeleteCommandHandler), nameof(Handle));

            Contact data;
            if (request.Contact.Id == 0)
            {
                data = _mapper.Map<Contact>(request.Contact);
                _contactRepository.Create(data);
            }
            else
            {
                data = await _contactRepository.GetContactByIdAsync(request.Contact.Id);
                _mapper.Map(request.Contact, data);
            }
            await _contactRepository.SaveAsync();
        }
    }
}