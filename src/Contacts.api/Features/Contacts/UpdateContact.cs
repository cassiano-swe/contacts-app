using Contacts.Database;
using Contacts.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Contacts.Features.Contacts;

public static class UpdateContact
{
    public record Request(string Name, string CountryCode, string PhoneNumber);
    public record Response(Guid Id, string Name, string CountryCode, string PhoneNumber);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("contacts/{id}", Handler).WithTags("Contacts");
        }
    }

    public static async Task<Results<Ok<Response>, NotFound>> Handler(int id, Request request, ContactsDbContext context)
    {
        var contact = await context.Contacts.FindAsync(id);
        
        if (contact is null)
        {
            return TypedResults.NotFound();
        }

        contact.Name = request.Name;
        contact.CountryCode = request.CountryCode;
        contact.PhoneNumber = request.PhoneNumber;

        await context.SaveChangesAsync();

        return TypedResults.Ok(new Response(contact.Id, contact.Name, contact.CountryCode, contact.PhoneNumber));
    }
}