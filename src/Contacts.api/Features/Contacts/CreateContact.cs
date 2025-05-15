using Contacts.Database;
using Contacts.Endpoints;
using Contacts.Entities;

namespace Contacts.Features.Contacts;

public static class CreateContact
{
    public record Request(Guid UserId, string Name, string Email, string PhoneNumber);
    public record Response(Guid Id, Guid UserId, string Name, string Email, string PhoneNumber, DateTime CreatedAt);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("contacts", Handler).WithTags("Contacts");
        }
    }

    public static async Task<IResult> Handler(Request request, ContactsDbContext context)
    {
        var contact = new Contact { 
            UserId = request.UserId
            , Name = request.Name
            , Email = request.Email
            , PhoneNumber = request.PhoneNumber
            , CreatedAt = DateTime.UtcNow 
            };

        context.Contacts.Add(contact);

        await context.SaveChangesAsync();

        return Results.Ok(
            new Response(contact.Id
            , contact.UserId
            , contact.Name
            , contact.Email
            , contact.PhoneNumber
            , contact.CreatedAt));
    }
}