using MiniValidation;
using NetCoreX.ViewModel;

namespace NetCoreX.API.EndpointFilters
{
    public class ValidationAnnotationFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var vm = context.GetArgument<ContactVM>(2);

            if (!MiniValidator.TryValidate(vm, out var validationErrors))
            {
                return TypedResults.ValidationProblem(validationErrors);
            }

            return await next.Invoke(context);
        }
    }
}