using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using NetCoreX.ViewModel;
using static NetCoreX.Data.Commands.Commands;
using static NetCoreX.Data.Queries.Queries;

namespace NetCoreX.API.EndpointsHandler
{
    public static class ContactsHandler
    {
        public static async Task<Ok<List<ContactVM>>> GetContactsAsync(
            IMediator mediator)
        {
            var query = new GetContactsQuery();
            var vm = await mediator.Send(query);

            return TypedResults.Ok(vm);
        }

        public static async Task<Results<NotFound, Ok<ContactVM>>> GetContactsByIdAsync(
            IMediator mediator,
            int id)
        {
            var query = new GetContactByIdQuery(id);
            var vm = await mediator.Send(query);

            return vm is not null ? TypedResults.Ok(vm) : TypedResults.NotFound();
        }

        public static async Task<NoContent> SaveContactAsync(
           IMediator mediator,
           ContactVM value)
        {
            var command = new ContactSaveCommand(value);
            await mediator.Send(command);

            return TypedResults.NoContent();
        }

        public static async Task<NoContent> DeleteContactAsync(
           IMediator mediator,
           int id)
        {
            var command = new ContactDeleteCommand(id);
            await mediator.Send(command);

            return TypedResults.NoContent();
        }
    }
}
