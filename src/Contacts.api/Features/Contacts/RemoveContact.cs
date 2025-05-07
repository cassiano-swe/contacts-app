using Contacts.Database;
using Contacts.Endpoints;

namespace Contacts.Features.Contacts;

public static class RemoveContact
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("contacts/{id}", Handler).WithTags("Contacts");
        }
    }

    public static async Task<IResult> Handler(Guid id, ContactsDbContext context)
    {
        var contact = await context.Contacts.FindAsync(id);
        
        if (contact is null)
        {
            return TypedResults.NotFound();
        }

        context.Remove(contact);

        await context.SaveChangesAsync();

        return Results.NoContent();
    }
}