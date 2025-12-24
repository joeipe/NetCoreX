using NetCoreX.API.EndpointFilters;
using NetCoreX.API.EndpointsHandler;

namespace NetCoreX.API.Configurations
{
    public static class EndpointRouteBuilderConfig
    {
        public static void RegisterContactsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var contactsEndpoints = endpoints.MapGroup("/contacts");

            contactsEndpoints.MapGet("", ContactsHandler.GetContactsAsync)
                .WithSummary("Get all contacts");

            contactsEndpoints.MapGet("/{id:int}", ContactsHandler.GetContactsByIdAsync)
                .WithSummary("Get a contact by providing an id");

            contactsEndpoints.MapPost("", ContactsHandler.SaveContactAsync)
                .AddEndpointFilter<ValidationAnnotationFilters>()
                .ProducesValidationProblem(400)
                .WithSummary("Create or update contact");

            contactsEndpoints.MapDelete("/{id:int}", ContactsHandler.DeleteContactAsync)
                .WithSummary("Delete a contact by providing Id");
        }
    }
}
