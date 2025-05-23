using Contacts.Database;
using Contacts.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Contacts.Features.Contacts;

public static class UpdateContact
{
    public record Request(string Name, string Email, string PhoneNumber);
    public record Response(Guid Id, Guid UserId, string Name, string Email, string PhoneNumber, DateTime CreatedAt);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("api/contacts/{id}", Handler).WithTags("Contacts");
        }
    }

    public static async Task<Results<Ok<Response>, NotFound>> Handler(Guid id, Request request, ContactsDbContext context)
    {
        var contact = await context.Contacts.FindAsync(id);
        
        if (contact is null)
        {
            return TypedResults.NotFound();
        }

        contact.Name = request.Name;
        contact.Email = request.Email;
        contact.PhoneNumber = request.PhoneNumber;

        await context.SaveChangesAsync();

        return TypedResults.Ok(new Response(contact.Id
            , contact.UserId
            , contact.Name
            , contact.Email
            , contact.PhoneNumber
            , contact.CreatedAt));
    }
}