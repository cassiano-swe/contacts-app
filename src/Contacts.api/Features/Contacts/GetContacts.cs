using Contacts.Database;
using Contacts.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Features.Contacts;

public static class GetContacts
{
    public record Response(Guid Id, string Name, string CountryCode, string PhoneNumber);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("contacts", Handler).WithTags("Contacts");
        }
    }

    public static async Task<IResult> Handler(ContactsDbContext context)
    {
        var contacts = await context.Contacts.ToListAsync();
        
        var responses = contacts.Select(c => new Response(c.Id, c.Name, c.CountryCode, c.PhoneNumber));

        return TypedResults.Ok(responses);
    }
}