using AutoMapper;
using Microsoft.Extensions.Logging;
using NetCoreX.Data.Repositories.Interfaces;
using NetCoreX.ViewModel;
using SharedKernel.Interfaces;
using static NetCoreX.Data.Queries.Queries;

namespace NetCoreX.Data.QueryHandlers
{
    public class GetContactsQueryHandler : IQueryHandler<GetContactsQuery, List<ContactVM>>
    {
        private readonly ILogger<GetContactsQueryHandler> _logger;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetContactsQueryHandler(
            ILogger<GetContactsQueryHandler> logger,
            IContactRepository contactRepository,
            IMapper mapper)
        {
            _logger = logger;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<List<ContactVM>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Handler}.{Action} start", nameof(GetContactsQueryHandler), nameof(Handle));

            var data = await _contactRepository.GetContactsAsync();

            var vm = _mapper.Map<List<ContactVM>>(data);
            return vm;
        }
    }
}