using Contacts.Database;
using Contacts.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Features.Contacts;

public static class GetContact
{
    public record Response(Guid Id, Guid UserId, string Name, string Email, string PhoneNumber, DateTime CreatedAt);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("contacts/{id}", Handler).WithTags("Contacts");
        }
    }

    public static async Task<Results<Ok<Response>, NotFound>> Handler(Guid id, ContactsDbContext context)
    {
        var contact = await context.Contacts.AsNoTracking().FirstOrDefaultAsync(c=> c.Id == id);
        
        if (contact is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new Response(contact.Id
            , contact.UserId
            , contact.Name
            , contact.Email
            , contact.PhoneNumber
            , contact.CreatedAt));
    }
}