using Contacts.Database;
using Contacts.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Features.Contacts;

public static class GetContacts
{
    public record Response(Guid Id, Guid UserId, string Name, string Email, string PhoneNumber, DateTime CreatedAt);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/contacts", Handler).WithTags("Contacts");
        }
    }

    public static async Task<IResult> Handler(ContactsDbContext context)
    {
        var contacts = await context.Contacts.AsNoTracking().ToListAsync();
        
        var responses = contacts.Select(contact => new Response(contact.Id
            , contact.UserId
            , contact.Name
            , contact.Email
            , contact.PhoneNumber
            , contact.CreatedAt));

        return TypedResults.Ok(responses);
    }
}