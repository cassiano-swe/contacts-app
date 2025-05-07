using Contacts.Database;
using Contacts.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Features.Contacts;

public static class GetContact
{
    public record Response(Guid Id, string Name, string CountryCode, string PhoneNumber);

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

        return TypedResults.Ok(new Response(contact.Id, contact.Name, contact.CountryCode, contact.PhoneNumber));
    }
}