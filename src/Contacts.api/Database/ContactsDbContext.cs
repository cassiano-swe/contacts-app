using Microsoft.EntityFrameworkCore;
using Contacts.Entities;

namespace Contacts.Database;

public class ContactsDbContext: DbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options): base(options)
    {
    }

    public DbSet<Contact> Contacts{get; init;}
}