using Contacts.Database;
using Contacts.Endpoints;
using Contacts.Entities;

namespace Contacts.Features.Contacts;

public static class CreateContact
{
    public record Request(string Name, string CountryCode, string PhoneNumber);
    public record Response(Guid Id, string Name, string CountryCode, string PhoneNumber);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("contacts", Handler).WithTags("Contacts");
        }
    }

    public static async Task<IResult> Handler(Request request, ContactsDbContext context)
    {
        var contact = new Contact{Name=request.Name, CountryCode=request.CountryCode, PhoneNumber = request.PhoneNumber};
        
        context.Contacts.Add(contact);
        
        await context.SaveChangesAsync();

        return Results.Ok(new Response(contact.Id, contact.Name, contact.CountryCode, contact.PhoneNumber));
    }
}